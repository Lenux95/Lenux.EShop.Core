using System;
using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogItemsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogItemsController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedItems<CatalogItem>>> GetItems(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? brandId = null,
            [FromQuery] int? typeId = null)
        {
            var items = await _catalogService.GetItemsByBrandAndTypeAsync(brandId, typeId, pageIndex, pageSize);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItem>> GetItem(int id)
        {
            var item = await _catalogService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CatalogItem>> CreateItem([FromBody] CatalogItem item)
        {
            var createdItem = await _catalogService.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatalogItem>> UpdateItem(int id, [FromBody] CatalogItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _catalogService.UpdateItemAsync(item);
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await _catalogService.DeleteItemAsync(id);
            return NoContent();
        }

        [HttpGet("serch")]
        public async Task<ActionResult<PaginatedItems<CatalogItem>>> GetItem([FromQuery] int pageIndex = 0,[FromQuery] int pageSize = 10,[FromQuery] int? brandId = null)
        {
            var items = await _catalogService.GetItemsByBrandAndTypeAsync(brandId, 1, pageIndex, pageSize);
            return Ok(items);
        }
    }
}