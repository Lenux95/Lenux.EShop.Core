using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetItemByIdAsync(int id);
        Task<PaginatedItems<CatalogItem>> GetItemsAsync(int pageIndex, int pageSize);
        Task<PaginatedItems<CatalogItem>> GetItemsByBrandAndTypeAsync(int? brandId, int? typeId, int pageIndex, int pageSize);
        Task<CatalogItem> CreateItemAsync(CatalogItem item);
        Task<CatalogItem> UpdateItemAsync(CatalogItem item);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
        Task<CatalogBrand> GetBrandByIdAsync(int id);
        Task<CatalogBrand> CreateBrandAsync(CatalogBrand brand);
        Task<CatalogBrand> UpdateBrandAsync(CatalogBrand brand);
        Task DeleteBrandAsync(int id);
        Task<IEnumerable<CatalogType>> GetTypesAsync();
        Task<CatalogType> GetTypeByIdAsync(int id);
        Task<CatalogType> CreateTypeAsync(CatalogType type);
        Task<CatalogType> UpdateTypeAsync(CatalogType type);
        Task DeleteTypeAsync(int id);
    }
}