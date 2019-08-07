using System.Collections.Generic;

namespace ePlatform.Integration.Models
{
    public class OutboxInvoiceCreateModel
    {
        public string InvoiceId { get; set; }
        public int Status { get; set; }
        public bool IsNew { get; set; }
        public string XsltCode { get; set; }
        public string LocalReferenceId { get; set; }
        public bool UseManualInvoiceId { get; set; }
        public AddressBookModel AddressBook { get; set; }
        public EArsivInfoModel EArsivInfo { get; set; }
        public int RecordType { get; set; }
        public string Note { get; set; }
        public GeneralInfoModel GeneralInfoModel { get; set; }
        public List<InvoiceLineModel> InvoiceLines { get; set; }
    }
}