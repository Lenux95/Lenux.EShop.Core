using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface ICatalogTypeRepository
    {
        Task<IEnumerable<CatalogType>> GetAllAsync();
        Task<CatalogType> GetByIdAsync(int id);
        Task AddAsync(CatalogType type);
        Task UpdateAsync(CatalogType type);
        Task DeleteAsync(int id);
    }
}