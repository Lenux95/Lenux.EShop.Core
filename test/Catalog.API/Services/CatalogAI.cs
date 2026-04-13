using System.Diagnostics; // 导入诊断命名空间，用于性能测量
using Microsoft.Extensions.AI; // 导入AI扩展命名空间，用于嵌入生成
using Pgvector; // 导入PostgreSQL向量支持命名空间，用于语义搜索

namespace eShop.Catalog.API.Services; // 命名空间定义

/// <summary>
/// 目录AI服务实现，提供语义搜索功能
/// </summary>
public sealed class CatalogAI : ICatalogAI // 密封类，实现ICatalogAI接口
{
    /// <summary>嵌入向量的维度</summary>
    private const int EmbeddingDimensions = 384;

    /// <summary>嵌入生成器，用于生成文本的嵌入向量</summary>
    private readonly IEmbeddingGenerator<string, Embedding<float>>? _embeddingGenerator;

    /// <summary>Web主机环境</summary>
    private readonly IWebHostEnvironment _environment;

    /// <summary>AI操作的日志记录器</summary>
    private readonly ILogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="environment">Web主机环境</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="embeddingGenerator">嵌入生成器，可选</param>
    public CatalogAI(
        IWebHostEnvironment environment, 
        ILogger<CatalogAI> logger, 
        IEmbeddingGenerator<string, Embedding<float>>? embeddingGenerator = null)
    {
        _embeddingGenerator = embeddingGenerator;
        _environment = environment;
        _logger = logger;
    }

    /// <inheritdoc/>
    public bool IsEnabled => _embeddingGenerator is not null; // 检查嵌入生成器是否存在，确定AI是否启用

    /// <inheritdoc/>
    public ValueTask<Vector?> GetEmbeddingAsync(CatalogItem item) =>
        IsEnabled ?
            GetEmbeddingAsync(CatalogItemToString(item)) : // 如果启用，将商品转换为字符串并获取嵌入
            ValueTask.FromResult<Vector?>(null); // 如果未启用，返回null

    /// <inheritdoc/>
    public async ValueTask<IReadOnlyList<Vector>?> GetEmbeddingsAsync(IEnumerable<CatalogItem> items)
    {
        if (IsEnabled)
        {
            long timestamp = Stopwatch.GetTimestamp(); // 开始计时

            // 为所有商品生成嵌入向量
            GeneratedEmbeddings<Embedding<float>> embeddings = await _embeddingGenerator!.GenerateAsync(items.Select(CatalogItemToString));
            // 转换为Vector类型并限制维度
            var results = embeddings.Select(m => new Vector(m.Vector[0..EmbeddingDimensions])).ToList();

            // 记录生成嵌入的时间
            if (_logger.IsEnabled(LogLevel.Trace))
            {
                _logger.LogTrace("Generated {EmbeddingsCount} embeddings in {ElapsedMilliseconds}s", results.Count, Stopwatch.GetElapsedTime(timestamp).TotalSeconds);
            }

            return results;
        }

        return null; // 如果未启用，返回null
    }

    /// <inheritdoc/>
    public async ValueTask<Vector?> GetEmbeddingAsync(string text)
    {
        if (IsEnabled)
        {
            long timestamp = Stopwatch.GetTimestamp(); // 开始计时

            // 生成文本的嵌入向量
            var embedding = await _embeddingGenerator!.GenerateVectorAsync(text);
            // 限制向量维度
            embedding = embedding[0..EmbeddingDimensions];

            // 记录生成嵌入的时间
            if (_logger.IsEnabled(LogLevel.Trace))
            {
                _logger.LogTrace("Generated embedding in {ElapsedMilliseconds}s: '{Text}'", Stopwatch.GetElapsedTime(timestamp).TotalSeconds, text);
            }

            return new Vector(embedding); // 转换为Vector类型并返回
        }

        return null; // 如果未启用，返回null
    }

    /// <summary>
    /// 将商品转换为字符串，用于生成嵌入向量
    /// </summary>
    /// <param name="item">商品</param>
    /// <returns>商品的字符串表示</returns>
    private static string CatalogItemToString(CatalogItem item) => $"{item.Name} {item.Description}";
}
