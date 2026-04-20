using Catalog.API.Data.Repositories;
using Catalog.API.Models;

namespace Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogItemRepository _itemRepository;
        private readonly ICatalogBrandRepository _brandRepository;
        private readonly ICatalogTypeRepository _typeRepository;

        public CatalogService(
            ICatalogItemRepository itemRepository,
            ICatalogBrandRepository brandRepository,
            ICatalogTypeRepository typeRepository)
        {
            _itemRepository = itemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }

        public async Task<CatalogItem> GetItemByIdAsync(Guid id)
        {
            return await _itemRepository.GetByIdAsync(id);
        }

        public async Task<PaginatedItems<CatalogItem>> GetItemsAsync(int pageIndex, int pageSize)
        {
            var items = await _itemRepository.GetAllAsync(pageIndex, pageSize);
            var count = await _itemRepository.CountAsync();
            return new PaginatedItems<CatalogItem>(pageIndex, pageSize, count, items);
        }

        public async Task<PaginatedItems<CatalogItem>> GetItemsByBrandAndTypeAsync(Guid? brandId, Guid? typeId, int pageIndex, int pageSize)
        {
            var items = await _itemRepository.GetByBrandAndTypeAsync(brandId, typeId, pageIndex, pageSize);
            var count = await _itemRepository.CountAsync();
            return new PaginatedItems<CatalogItem>(pageIndex, pageSize, count, items);
        }

        public async Task<CatalogItem> CreateItemAsync(CatalogItem item)
        {
            await _itemRepository.AddAsync(item);
            return item;
        }

        public async Task<CatalogItem> UpdateItemAsync(CatalogItem item)
        {
            await _itemRepository.UpdateAsync(item);
            return item;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await _itemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<CatalogBrand> GetBrandByIdAsync(Guid id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<CatalogBrand> CreateBrandAsync(CatalogBrand brand)
        {
            await _brandRepository.AddAsync(brand);
            return brand;
        }

        public async Task<CatalogBrand> UpdateBrandAsync(CatalogBrand brand)
        {
            await _brandRepository.UpdateAsync(brand);
            return brand;
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            await _brandRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            return await _typeRepository.GetAllAsync();
        }

        public async Task<CatalogType> GetTypeByIdAsync(Guid id)
        {
            return await _typeRepository.GetByIdAsync(id);
        }

        public async Task<CatalogType> CreateTypeAsync(CatalogType type)
        {
            await _typeRepository.AddAsync(type);
            return type;
        }

        public async Task<CatalogType> UpdateTypeAsync(CatalogType type)
        {
            await _typeRepository.UpdateAsync(type);
            return type;
        }

        public async Task DeleteTypeAsync(Guid id)
        {
            await _typeRepository.DeleteAsync(id);
        }
    }
}