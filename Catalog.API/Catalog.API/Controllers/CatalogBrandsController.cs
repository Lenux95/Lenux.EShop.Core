using Catalog.API.Models;
using Catalog.API.Models.Api;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogBrandsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogBrandsController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IEnumerable<CatalogBrand>> GetBrands()
        {
            return await _catalogService.GetBrandsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogBrand>> GetBrand(Guid id)
        {
            var brand = await _catalogService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound(ApiResponse.Fail(404, "品牌不存在"));
            }
            return brand;
        }

        [HttpPost]
        public async Task<ActionResult<CatalogBrand>> CreateBrand([FromBody] CatalogBrand brand)
        {
            var createdBrand = await _catalogService.CreateBrandAsync(brand);
            return CreatedAtAction(nameof(GetBrand), new { id = createdBrand.Id }, createdBrand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatalogBrand>> UpdateBrand(Guid id, [FromBody] CatalogBrand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest(ApiResponse.Fail(400, "请求ID与品牌ID不匹配"));
            }

            return await _catalogService.UpdateBrandAsync(brand);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            await _catalogService.DeleteBrandAsync(id);
            return Ok();
        }
    }
}
