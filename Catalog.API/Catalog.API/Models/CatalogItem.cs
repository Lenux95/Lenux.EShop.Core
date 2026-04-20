using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.API.Models
{
    /// <summary>
    /// 商品实体类
    /// </summary>
    public class CatalogItem
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Key,Required]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// 商品图片文件名
        /// </summary>
        public string PictureFileName { get; set; } = string.Empty;

        /// <summary>
        /// 商品类型ID
        /// </summary>
        [Column(TypeName = "char(36)")]
        public Guid CatalogTypeId { get; set; }
        
        /// <summary>
        /// 商品类型导航属性
        /// </summary>
        public CatalogType CatalogType { get; set; } = null!;

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        [Column(TypeName = "char(36)")]
        public Guid CatalogBrandId { get; set; }
        
        /// <summary>
        /// 商品品牌导航属性
        /// </summary>
        public CatalogBrand CatalogBrand { get; set; } = null!;
        
        /// <summary>
        /// 可用库存数量
        /// </summary>
        public int AvailableStock { get; set; }
        
        /// <summary>
        /// 补货阈值，当库存低于此值时需要补货
        /// </summary>
        public int RestockThreshold { get; set; }
        
        /// <summary>
        /// 最大库存阈值，防止过度补货
        /// </summary>
        public int MaxStockThreshold { get; set; }
    }
}