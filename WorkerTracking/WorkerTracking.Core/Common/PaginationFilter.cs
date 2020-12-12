namespace WorkerTracking.Core.Common
{
    public class PaginationFilter
    {
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 25;
        }

        public PaginationFilter(int pageNumber)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 100 ? 100 : 100;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
