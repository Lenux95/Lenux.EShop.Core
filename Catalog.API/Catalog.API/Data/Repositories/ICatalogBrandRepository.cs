using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface ICatalogBrandRepository
    {
        Task<IEnumerable<CatalogBrand>> GetAllAsync();
        Task<CatalogBrand> GetByIdAsync(Guid id);
        Task AddAsync(CatalogBrand brand);
        Task UpdateAsync(CatalogBrand brand);
        Task DeleteAsync(Guid id);
    }
}