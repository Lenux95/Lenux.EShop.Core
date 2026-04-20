using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.Repositories.Implementations
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly CatalogDbContext _context;

        public CatalogBrandRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CatalogBrand>> GetAllAsync()
        {
            return await _context.CatalogBrands.OrderBy(b => b.Brand).ToListAsync();
        }

        public async Task<CatalogBrand> GetByIdAsync(Guid id)
        {
            return await _context.CatalogBrands.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(CatalogBrand brand)
        {
            await _context.CatalogBrands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CatalogBrand brand)
        {
            _context.CatalogBrands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var brand = await GetByIdAsync(id);
            if (brand != null)
            {
                _context.CatalogBrands.Remove(brand);
                await _context.SaveChangesAsync();
            }
        }
    }
}