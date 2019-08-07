namespace ePlatform.Integration.Models
{
    public class InvoiceLineModel
    {
        public string InventoryCard { get; set; }
        public bool SerialNoEnabled { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal LineAmount { get; set; }
        public string UnitCode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public string VatExemptionReasonCode { get; set; }
        public string VatExemptionReason { get; set; }
    }
}