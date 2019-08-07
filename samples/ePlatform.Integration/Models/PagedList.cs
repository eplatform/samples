using System.Collections.Generic;

namespace ePlatform.Integration.Models
{
    public class PagedList<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
        public IEnumerable<T> Items { get; set; }
    }
}