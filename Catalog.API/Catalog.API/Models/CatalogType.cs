using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.API.Models
{
    /// <summary>
    /// 商品类型实体类
    /// </summary>
    [Table("catalog_type")]
    public class CatalogType
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        [Key, Required]
        [Column("id",TypeName = "char(36)")]
        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = string.Empty;
    }
}