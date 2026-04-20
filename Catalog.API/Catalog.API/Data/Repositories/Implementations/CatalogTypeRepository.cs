using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.Repositories.Implementations
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly CatalogDbContext _context;

        public CatalogTypeRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CatalogType>> GetAllAsync()
        {
            return await _context.CatalogTypes.OrderBy(t => t.Type).ToListAsync();
        }

        public async Task<CatalogType> GetByIdAsync(Guid id)
        {
            return await _context.CatalogTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(CatalogType type)
        {
            await _context.CatalogTypes.AddAsync(type);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CatalogType type)
        {
            _context.CatalogTypes.Update(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var type = await GetByIdAsync(id);
            if (type != null)
            {
                _context.CatalogTypes.Remove(type);
                await _context.SaveChangesAsync();
            }
        }
    }
}