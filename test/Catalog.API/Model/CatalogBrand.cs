using System.ComponentModel.DataAnnotations; // 导入数据注解命名空间，用于属性验证

namespace eShop.Catalog.API.Model; // 命名空间定义

/// <summary>
/// 品牌模型类，表示商品的品牌
/// </summary>
public class CatalogBrand
{
    /// <summary>
    /// 构造函数，初始化品牌名称
    /// </summary>
    /// <param name="brand">品牌名称</param>
    public CatalogBrand(string brand) {
        Brand = brand;
    }

    /// <summary>
    /// 品牌ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 品牌名称，必填字段
    /// </summary>
    [Required] // 标记为必填字段
    public string Brand { get; set; }
}
