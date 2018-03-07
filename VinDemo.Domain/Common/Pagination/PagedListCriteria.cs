namespace VinDemo.Domain.Common.Pagination
{
    public class PagingCriteria
    {
        public PagingCriteria(int pageNumber, int pageSize, string sortBy, bool sortByDescending)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortBy = sortBy;
            SortByDescending = sortByDescending;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool SortByDescending { get; set; }
    }
}