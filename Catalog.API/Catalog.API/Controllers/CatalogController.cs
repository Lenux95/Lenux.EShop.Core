using Catalog.API.Models;
using Catalog.API.Models.Api;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogItemsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogItemsController> _logger;

        public CatalogItemsController(ICatalogService catalogService, ILogger<CatalogItemsController> logger)
        {
            _catalogService = catalogService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<PaginatedItems<CatalogItem>> GetItems(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? brandId = null,
            [FromQuery] Guid? typeId = null)
        {
            _logger.LogInformation("查询商品列表, PageIndex={PageIndex}, PageSize={PageSize}", pageIndex, pageSize);
            return await _catalogService.GetItemsByBrandAndTypeAsync(brandId, typeId, pageIndex, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItem>> GetItem(Guid id)
        {
            var item = await _catalogService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound(ApiResponse.Fail(404, "商品不存在"));
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<CatalogItem>> CreateItem([FromBody] CatalogItem item)
        {
            var createdItem = await _catalogService.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatalogItem>> UpdateItem(Guid id, [FromBody] CatalogItem item)
        {
            if (id != item.Id)
            {
                return BadRequest(ApiResponse.Fail(400, "请求ID与商品ID不匹配"));
            }

            return await _catalogService.UpdateItemAsync(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            await _catalogService.DeleteItemAsync(id);
            return Ok();
        }

        [HttpGet("serch")]
        public async Task<PaginatedItems<CatalogItem>> GetItem(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? brandId = null)
        {
            return await _catalogService.GetItemsByBrandAndTypeAsync(brandId, null, pageIndex, pageSize);
        }
    }
}
