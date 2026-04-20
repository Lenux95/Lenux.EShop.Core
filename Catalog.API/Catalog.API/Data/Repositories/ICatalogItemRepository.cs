using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface ICatalogItemRepository
    {
        Task<CatalogItem> GetByIdAsync(int id);
        Task<IEnumerable<CatalogItem>> GetAllAsync(int pageIndex, int pageSize);
        Task<IEnumerable<CatalogItem>> GetByBrandAndTypeAsync(int? brandId, int? typeId, int pageIndex, int pageSize);
        Task AddAsync(CatalogItem item);
        Task UpdateAsync(CatalogItem item);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}