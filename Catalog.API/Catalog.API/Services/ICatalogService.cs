using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetItemByIdAsync(Guid id);
        Task<PaginatedItems<CatalogItem>> GetItemsAsync(int pageIndex, int pageSize);
        Task<PaginatedItems<CatalogItem>> GetItemsByBrandAndTypeAsync(Guid? brandId, Guid? typeId, int pageIndex, int pageSize);
        Task<CatalogItem> CreateItemAsync(CatalogItem item);
        Task<CatalogItem> UpdateItemAsync(CatalogItem item);
        Task DeleteItemAsync(Guid id);
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
        Task<CatalogBrand> GetBrandByIdAsync(Guid id);
        Task<CatalogBrand> CreateBrandAsync(CatalogBrand brand);
        Task<CatalogBrand> UpdateBrandAsync(CatalogBrand brand);
        Task DeleteBrandAsync(Guid id);
        Task<IEnumerable<CatalogType>> GetTypesAsync();
        Task<CatalogType> GetTypeByIdAsync(Guid id);
        Task<CatalogType> CreateTypeAsync(CatalogType type);
        Task<CatalogType> UpdateTypeAsync(CatalogType type);
        Task DeleteTypeAsync(Guid id);
    }
}