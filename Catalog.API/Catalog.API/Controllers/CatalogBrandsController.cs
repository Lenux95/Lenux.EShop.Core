using Catalog.API.Models;
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
        public async Task<ActionResult<IEnumerable<CatalogBrand>>> GetBrands()
        {
            var brands = await _catalogService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogBrand>> GetBrand(int id)
        {
            var brand = await _catalogService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<CatalogBrand>> CreateBrand([FromBody] CatalogBrand brand)
        {
            var createdBrand = await _catalogService.CreateBrandAsync(brand);
            return CreatedAtAction(nameof(GetBrand), new { id = createdBrand.Id }, createdBrand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatalogBrand>> UpdateBrand(int id, [FromBody] CatalogBrand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            var updatedBrand = await _catalogService.UpdateBrandAsync(brand);
            return Ok(updatedBrand);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            await _catalogService.DeleteBrandAsync(id);
            return NoContent();
        }
    }
}