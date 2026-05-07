namespace Catalog.API.Models.Dtos
{
    //public class AgentQueryResponse
    //{
    //    public bool Success { get; set; }
    //    public string Message { get; set; } = string.Empty;
    //    public List<AgentResult> Results { get; set; } = new List<AgentResult>();
    //}

    //public class AgentResult
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; } = string.Empty;
    //    public string Content { get; set; } = string.Empty;
    //}
    public class AgentQueryResponse
    {
        public string Answer { get; set; }
    }
}
