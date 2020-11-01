using System;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Core.Queries;

namespace WorkerTracking.Core.Helpers
{
    public static class PaginationHelper
    {
        public static int GetSkipRows(GetAllWorkersQuery request)
        {
            return (request.PageNumber - 1) * request.PageSize;
        }
    }
}
