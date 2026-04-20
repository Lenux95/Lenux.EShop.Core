using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface ICatalogTypeRepository
    {
        Task<IEnumerable<CatalogType>> GetAllAsync();
        Task<CatalogType> GetByIdAsync(Guid id);
        Task AddAsync(CatalogType type);
        Task UpdateAsync(CatalogType type);
        Task DeleteAsync(Guid id);
    }
}