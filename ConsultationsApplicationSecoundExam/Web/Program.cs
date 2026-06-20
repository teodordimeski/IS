using System.Threading.RateLimiting;
using Domain.Config;
using Domain.Dto;
using Domain.Models;
using EvolveDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository;
using Repository.Implementation;
using Repository.Interface;
using Service.Implementation;
using Service.Interface;
using Service.BackgroundServices;
using Web;
using Web.DbSeeder;
using Web.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
    options.UseLazyLoadingProxies();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IConsultationService, ConsultationService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<ConsultationMapper>();
builder.Services.AddScoped<AttendanceMapper>();
builder.Services.AddScoped<IInboundEventEntryService, InboundEventEntryService>();
builder.Services.AddScoped<IInboundEventEntryProcessor, InboundEventEntryProcessor>();
builder.Services.AddScoped<IEtlSyncService, EtlSyncService>();
builder.Services.AddScoped<IConsultationsRepository, ConsultationsRepository>();
builder.Services.AddHostedService<SyncConsultationsBackgroundService>();
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));
builder.Services.Configure<ApiKeySettings>(builder.Configuration.GetSection("ApiKeySettings"));
builder.Services.Configure<ConsultationsApiSettings>(builder.Configuration.GetSection("ConsultationsApiSettings"));
builder.Services.AddHostedService<InboundEventProcessorBackgroundService>();

builder.Services.AddHttpClient<IConsultationsApiClient<ExternalConsultationsDto>, ConsultationsApiClient>((sp, option) =>
{
    var settings = sp.GetService<IOptions<ConsultationsApiSettings>>().Value;
    option.BaseAddress = new Uri(settings.BaseAddress);
    option.DefaultRequestHeaders.Add("X-Api-Key", settings.ApiKey);
});

builder.Services.Configure<RateLimitSettings>(builder.Configuration.GetSection("RateLimitSettings"));

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;

    options.AddPolicy("external-api", context =>
    {
        var settings = context.RequestServices
            .GetRequiredService<IOptions<RateLimitSettings>>().Value;
        var apiKey = context.Request.Headers["X-Api-Key"].ToString();

        return RateLimitPartition.GetFixedWindowLimiter(apiKey, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = settings.PermitLimit,
            Window = TimeSpan.FromSeconds(settings.WindowInSeconds),
            QueueLimit = 0,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    });
});




builder.Services.AddIdentity<ConsultationsApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

using var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

var logger = loggerFactory.CreateLogger("Evolve");

try
{
    using var cnx = new SqliteConnection(connectionString);

    var evolve = new Evolve(cnx, msg => logger.LogInformation(msg))
    {
        Locations = new[] { "Database/Migrations" },
        IsEraseDisabled = true
    };

    evolve.Migrate();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Database migration failed.");
    throw;
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ConsultationsApplicationUser>>();

    await DbSeeder.SeedAsync(context, userManager);
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

public partial class Program { }