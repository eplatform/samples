using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePlatform.Integration.Models
{
    public class OutboxInvoiceGetModel
    {
        public Guid Id { get; set; }
        public Guid? EnvelopeId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int TipType { get; set; }
        public string TargetTitle { get; set; }
        public string TargetVknTckn { get; set; }
        public string TargetAlias { get; set; }
        public bool IsArchived { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public string Currency { get; set; }
        public decimal? TaxTotal { get; set; }
        public DateTime? SentDate { get; set; }
        public Guid? ResponseEnvelopeId { get; set; }
        public string LocalReferenceId { get; set; }
        public string Message { get; set; }
        public int AppType { get; set; }
        public string Reason { get; set; }
        public string Prefix { get; set; }
        public EArsivInvoiceGetModel EarsivInvoice { get; set; }
    }
}
