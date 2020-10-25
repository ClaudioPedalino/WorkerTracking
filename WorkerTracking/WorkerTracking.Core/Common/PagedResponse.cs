using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WorkerTracking.Core.Common
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }

        public int? PageNumber { get; }
        public int? PageSize { get; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
    }

}
