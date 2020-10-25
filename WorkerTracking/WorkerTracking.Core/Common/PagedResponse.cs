using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WorkerTracking.Core.Common
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data, int? pageNumber = 1, int? pageSize = 25)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }

        public int? PageNumber { get; }
        public int? PageSize { get; }
        public string OrderBy { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

}
