namespace Catalog.API.Models
{
    public class PaginatedItems<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

        public PaginatedItems(int pageIndex, int pageSize, long count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}