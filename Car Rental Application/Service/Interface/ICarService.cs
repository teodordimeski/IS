using Domain.Dto;
using Domain.Models;

namespace Service.Interface;

public interface ICarService
{
    Task<Car> GetByIdNotNullAsync(Guid id);
    Task<List<Car>> GetAllAsync(string? LocationName);
    Task<Car> CreateAsync(CarDto dto);
    Task<Car> UpdateAsync(Guid id, CarDto dto);
    Task<Car> DeleteByIdAsync(Guid id);
    Task<PaginatedResult<Car>> GetPagedAsync(int pageNumber, int pageSize);
}