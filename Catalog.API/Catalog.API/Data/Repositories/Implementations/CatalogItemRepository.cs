using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.Repositories.Implementations
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly CatalogDbContext _context;

        public CatalogItemRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<CatalogItem> GetByIdAsync(Guid id)
        {
            return await _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CatalogItem>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetByBrandAndTypeAsync(Guid? brandId, Guid? typeId, int pageIndex, int pageSize)
        {
            var query = _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .AsQueryable();

            if (brandId.HasValue)
            {
                query = query.Where(c => c.CatalogBrandId == brandId);
            }

            if (typeId.HasValue)
            {
                query = query.Where(c => c.CatalogTypeId == typeId);
            }

            return await query
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(CatalogItem item)
        {
            await _context.CatalogItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CatalogItem item)
        {
            _context.CatalogItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.CatalogItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync()
        {
            return await _context.CatalogItems.CountAsync();
        }
    }
}