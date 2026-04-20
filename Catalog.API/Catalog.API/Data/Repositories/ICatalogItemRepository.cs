using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface ICatalogItemRepository
    {
        Task<CatalogItem> GetByIdAsync(Guid id);
        Task<IEnumerable<CatalogItem>> GetAllAsync(int pageIndex, int pageSize);
        Task<IEnumerable<CatalogItem>> GetByBrandAndTypeAsync(Guid? brandId, Guid? typeId, int pageIndex, int pageSize);
        Task AddAsync(CatalogItem item);
        Task UpdateAsync(CatalogItem item);
        Task DeleteAsync(Guid id);
        Task<int> CountAsync();
    }
}