using Pgvector; // 导入PostgreSQL向量支持命名空间，用于语义搜索

namespace eShop.Catalog.API.Services; // 命名空间定义

/// <summary>
/// 目录AI服务接口，用于提供语义搜索功能
/// </summary>
public interface ICatalogAI
{
    /// <summary>获取AI系统是否启用。</summary>
    bool IsEnabled { get; }

    /// <summary>获取指定文本的嵌入向量。</summary>
    /// <param name="text">要获取嵌入向量的文本</param>
    /// <returns>嵌入向量，或null如果无法生成</returns>
    ValueTask<Vector?> GetEmbeddingAsync(string text);
    
    /// <summary>获取指定商品的嵌入向量。</summary>
    /// <param name="item">要获取嵌入向量的商品</param>
    /// <returns>嵌入向量，或null如果无法生成</returns>
    ValueTask<Vector?> GetEmbeddingAsync(CatalogItem item);

    /// <summary>获取指定商品集合的嵌入向量。</summary>
    /// <param name="item">要获取嵌入向量的商品集合</param>
    /// <returns>嵌入向量列表，或null如果无法生成</returns>
    ValueTask<IReadOnlyList<Vector>?> GetEmbeddingsAsync(IEnumerable<CatalogItem> item);
}
