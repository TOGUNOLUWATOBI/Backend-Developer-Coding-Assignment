using RestfulApiExtension.Models;

namespace RestfulApiExtension.Services.Interfaces;

public interface IProductObjectService
{
    Task<IEnumerable<ProductObject>> GetAllAsync(string? name, int page, int pageSize);
    Task<ProductObject?> GetByIdAsync(string id);
    Task<object?> CreateAsync(ProductObject productObject);
    Task<bool> DeleteAsync(string id);
}