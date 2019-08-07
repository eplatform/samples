using System;

namespace ePlatform.Extensions.Models
{
    public class PagingModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortedColumn { get; set; }
        public string QueryFilter { get; set; }
        [Obsolete("FilterQuery yerine QueryFilter kullanın.")]
        public string FilterQuery { get; set; }
        public bool IsDesc { get; set; }
    }
}