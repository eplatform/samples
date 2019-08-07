using System;
using ePlatform.Extensions.Models;

namespace ePlatform.Integration.Models
{
    public class GeneralInfoModel
    {
        public string Ettn { get; set; }
        public string Prefix { get; set; }
        public string CustomizationId { get; set; }
        public string InvoiceNumber { get; set; }
        public string SlipNumber { get; set; }
        public int InvoiceProfileType { get; set; }
        public string IssueDate { get; set; }
        public int Type { get; set; }
        public string ReturnInvoiceNumber { get; set; }
        public string ReturnInvoiceDate { get; set; }
        public string CurrencyCode { get; set; }
        public int ExchangeRate { get; set; }
    }
}