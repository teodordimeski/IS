using System.Net.Http.Json;
using Domain.Dto;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationsApiClient : IConsultationsApiClient<ExternalConsultationsDto>
{
    private readonly HttpClient _client;

    public ConsultationsApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<ExternalConsultationsDto> GetAllConsultationsModifiedSinceAsync(DateTime dateLastModified)
    {
        var path = $"/api/external/consultations?modifiedSince={dateLastModified}";
        
        var response = await _client.GetAsync(path);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ExternalConsultationsDto>();
        }
        
        throw new Exception();
    }
}