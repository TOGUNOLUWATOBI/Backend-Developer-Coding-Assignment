using RestfulApiExtension.Models;
using RestfulApiExtension.Services.Interfaces;

namespace RestfulApiExtension.Services;

public class ProductObjectService : IProductObjectService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductObjectService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    //TODO : Get all Objects
    public async Task<IEnumerable<ProductObject>> GetAllAsync(string? name, int page, int pageSize)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<List<ProductObject>>("https://api.restful-api.dev/objects");

        // Check if the response is null
        if (response == null)
            return Enumerable.Empty<ProductObject>();

        // filter result by name
        var filtered = response.Where(p => string.IsNullOrEmpty(name) || (p.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false));
        
        // Paginate the filtered result
        return filtered.Skip((page - 1) * pageSize).Take(pageSize);
    }

    //TODO : Get Object by id
    public async Task<ProductObject?> GetByIdAsync(string id)
    {
        var client = _httpClientFactory.CreateClient();
        return await client.GetFromJsonAsync<ProductObject>($"https://api.restful-api.dev/objects/{id}");
    }

    //TODO : Create Object
    public async Task<object?> CreateAsync(ProductObject productObject)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("https://api.restful-api.dev/objects", productObject);

        // Check if the response is successful
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<object>();
    }

    //TODO : Delete Object
    public async Task<bool> DeleteAsync(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://api.restful-api.dev/objects/{id}");
        return response.IsSuccessStatusCode;
    }
}