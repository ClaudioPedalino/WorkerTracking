using System.Collections.Generic;

namespace WorkerTracking.Core.Common
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data, int totalResults, int? pageNumber = 1, int? pageSize = 25)
        {
            PageNumber = pageNumber;
            TotalResults = totalResults;
            PageSize = pageSize;
            Data = data;
        }

        public int? PageNumber { get; }
        public int TotalResults { get; }
        public int? PageSize { get; }
        public string OrderBy { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

}
