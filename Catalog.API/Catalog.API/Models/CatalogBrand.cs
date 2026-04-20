using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.API.Models
{
    /// <summary>
    /// 商品品牌实体类
    /// </summary>
    public class CatalogBrand
    {
        /// <summary>
        /// 品牌ID
        /// </summary>
        [Key, Required]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Brand { get; set; } = string.Empty;
    }
}