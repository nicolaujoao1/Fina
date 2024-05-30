using System.Text.Json.Serialization;

namespace Fina.Core.Responses
{
    public class PagedResponse<T> : Response<T>
    {
        [JsonConstructor]
        public PagedResponse(T? data, int totalcount, int currentPage=1,int pageSize=Configuration.DefaultPageSize):base(data)
        {
            Data = data;
            TotalCount = totalcount;    
            CurrentPage = currentPage;  
            PageSize = pageSize;    
        }
        public PagedResponse(T? data, int code=Configuration.DefaultStatusCode,string? message=null) : base(data, code,message)
        {
            
        }



        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalPages / (double)PageSize);
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int TotalCount { get; set; }
    }
}
