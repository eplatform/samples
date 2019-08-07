namespace ePlatform.Integration.Models
{
    public class OutboxInvoiceZipModel
    {

        public string InvoiceZip { get; set; }
        public int Status { get; set; }
        public string LocalReferenceId { get; set; }
        public string Prefix { get; set; }
        public bool UseManualInvoiceId { get; set; }
        public bool? CheckLocalReferenceId { get; set; }
        public string TargetAlias { get; set; }

        public int AppType { get; set; }
        public EArsivInfoModel EArsivInfo { get; set; }

    }
}