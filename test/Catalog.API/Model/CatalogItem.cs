using System.ComponentModel.DataAnnotations; // 导入数据注解命名空间，用于属性验证
using System.Text.Json.Serialization; // 导入JSON序列化命名空间，用于控制JSON序列化行为
using Pgvector; // 导入PostgreSQL向量支持命名空间，用于语义搜索

namespace eShop.Catalog.API.Model; // 命名空间定义

/// <summary>
/// 商品模型类，表示目录中的商品
/// </summary>
public class CatalogItem
{
    /// <summary>
    /// 商品ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 商品名称，必填字段
    /// </summary>
    [Required] // 标记为必填字段
    public string Name { get; set; }

    /// <summary>
    /// 商品描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 商品价格
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 商品图片文件名
    /// </summary>
    public string? PictureFileName { get; set; }

    /// <summary>
    /// 商品类型ID，外键
    /// </summary>
    public int CatalogTypeId { get; set; }

    /// <summary>
    /// 商品类型导航属性
    /// </summary>
    public CatalogType? CatalogType { get; set; }

    /// <summary>
    /// 商品品牌ID，外键
    /// </summary>
    public int CatalogBrandId { get; set; }

    /// <summary>
    /// 商品品牌导航属性
    /// </summary>
    public CatalogBrand? CatalogBrand { get; set; }

    /// <summary>
    /// 库存数量
    /// </summary>
    public int AvailableStock { get; set; }

    /// <summary>
    /// 补货阈值，当库存低于此值时应补货
    /// </summary>
    public int RestockThreshold { get; set; }

    /// <summary>
    /// 最大库存阈值，由于仓库物理/物流限制，任何时候的最大库存数量
    /// </summary>
    public int MaxStockThreshold { get; set; }

    /// <summary>商品描述的可选嵌入向量，用于语义搜索。</summary>
    [JsonIgnore] // 序列化时忽略此字段
    public Vector? Embedding { get; set; }

    /// <summary>
    /// 商品是否正在补货
    /// </summary>
    public bool OnReorder { get; set; }

    /// <summary>
    /// 构造函数，初始化商品名称
    /// </summary>
    /// <param name="name">商品名称</param>
    public CatalogItem(string name) { Name = name; }

    /// <summary>
    /// 减少库存数量，并确保未突破补货阈值。
    /// 
    /// 如果商品库存充足，则此调用结束时返回的整数应与quantityDesired相同。
    /// 如果库存不足，方法将移除所有可用库存并将该数量返回给客户端。
    /// 在这种情况下，客户端有责任确定返回的数量是否与quantityDesired相同。
    /// 传入负数是无效的。
    /// </summary>
    /// <param name="quantityDesired">期望减少的库存数量</param>
    /// <returns>实际从库存中移除的数量</returns>
    public int RemoveStock(int quantityDesired)
    {
        // 检查库存是否为空
        if (AvailableStock == 0)
        {
            throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
        }

        // 检查请求数量是否有效
        if (quantityDesired <= 0)
        {
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        }

        // 计算实际可以移除的数量，取期望数量和可用库存的较小值
        int removed = Math.Min(quantityDesired, this.AvailableStock);

        // 更新可用库存
        this.AvailableStock -= removed;

        // 返回实际移除的数量
        return removed;
    }

    /// <summary>
    /// 增加库存数量
    /// </summary>
    /// <param name="quantity">要增加的库存数量</param>
    /// <returns>已添加到库存的数量</returns>
    public int AddStock(int quantity)
    {
        // 记录原始库存数量
        int original = this.AvailableStock;

        // 检查添加后的库存是否超过最大库存阈值
        if ((this.AvailableStock + quantity) > this.MaxStockThreshold)
        {
            // 只添加到最大库存阈值
            this.AvailableStock += (this.MaxStockThreshold - this.AvailableStock);
        }
        else
        {
            // 正常添加库存
            this.AvailableStock += quantity;
        }

        // 重置补货标志
        this.OnReorder = false;

        // 返回实际添加的数量
        return this.AvailableStock - original;
    }
}
