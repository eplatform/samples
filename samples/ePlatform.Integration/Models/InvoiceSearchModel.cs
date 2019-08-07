using System;
using ePlatform.Extensions.Models;

namespace ePlatform.Integration.Models
{
    public class InvoiceSearchModel : PagingModel
    {
        public string InvoiceNumber { get; set; }
        public string TargetVknTckn { get; set; }
        public int? Type { get; set; }
        public int? TipType { get; set; }
        public int? Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime? ExecutionStartDate { get; set; }
        public DateTime? ExecutionEndDate { get; set; }
    }
}