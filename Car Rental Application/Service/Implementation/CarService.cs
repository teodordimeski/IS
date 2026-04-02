using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class CarService : ICarService 
{
    private readonly IRepository <Car> _carRepository;

    public CarService(IRepository<Car> carRepository)
    {
        _carRepository = carRepository;
    }


    public async Task<Car> GetByIdNotNullAsync(Guid id)
    {
        var result = await _carRepository.GetAsync(
            selector: x =>x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public async Task<List<Car>> GetAllAsync(string? LocationName)
    {
        if (LocationName == null)
        {
            return await _carRepository.GetAllAsync(selector: x => x);
        }
        return await _carRepository.GetAllAsync(
            selector: x => x,
            predicate: x => x.Location.Name == LocationName);
    }

    public async Task<Car> CreateAsync(CarDto dto)
    {
        var Car = new Car()
        {
            Make = dto.Make,
            Model = dto.Model,
            Category = dto.Category,
            LocationId = dto.LocationId
        };
        
        return await _carRepository.InsertAsync(Car);
    }

    public async Task<Car> UpdateAsync(Guid id, CarDto dto)
    {
        var toUpdate = await GetByIdNotNullAsync(id);
        toUpdate.Model = dto.Model;
        toUpdate.Make = dto.Make;
        toUpdate.Category = dto.Category;
        toUpdate.LocationId = dto.LocationId;
        
        return await _carRepository.UpdateAsync(toUpdate);
    }

    public async Task<Car> DeleteByIdAsync(Guid id)
    {
        var toUpdate = await GetByIdNotNullAsync(id);
        
        return await _carRepository.DeleteAsync(toUpdate);
    }

    public async Task<PaginatedResult<Car>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _carRepository.GetAllPagedAsync(
            selector: x => x,
            pageNumber: pageNumber,
            pageSize: pageSize,
            include: x => x.Include(y => y.Rentals));
    }
}