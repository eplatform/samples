using System;
using ePlatform.Api;

namespace ePlatform.Api.SampleNetCoreConsole
{
    public static class eFaturaHelper
    {
        /// <summary>
        /// Fonksiyona ettn numarası guid olarak gönderilirse, ettn numarası bu guid ile doldurulur.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UblBuilderModel fillUblModel(Guid id)
        {
            var model = new UblBuilderModel();
            model.RecordType = 1;
            model.Status = 20;
            model.XsltCode = null;
            model.LocalReferenceId = null;
            model.UseManualInvoiceId = false;
            model.Note = null;
            model.Notes = null;//new List<NoteModel>();
            //model.Notes.Add(new NoteModel() 
            //    { 
            //        Note = "Test notu"
            //    }
            //);
            //model.Notes.Add(new NoteModel()
            //    {
            //        Note = "Test notu 2"
            //    }
            //);

            //AddressBook
            model.AddressBook = new AddressBookModel();
            model.AddressBook.Name = "Alıcı adı";
            model.AddressBook.ReceiverPersonSurName = "Alıcı Soyadı";
            model.AddressBook.IdentificationNumber = "1234567803";
            model.AddressBook.Alias = "urn:mail:defaulttest3pk@medyasoft.com.tr";
            model.AddressBook.RegisterNumber = "11112222333444";
            model.AddressBook.ReceiverStreet = "Bulvar / cadde / sokak";
            model.AddressBook.ReceiverBuildingName = "Bina adı";
            model.AddressBook.ReceiverBuildingNumber = "1";
            model.AddressBook.ReceiverDoorNumber = "11";
            model.AddressBook.ReceiverSmallTown = "Kasaba / Köy";
            model.AddressBook.ReceiverZipCode = "34000";
            model.AddressBook.ReceiverDistrict = "Üsküdar";
            model.AddressBook.ReceiverCity = "İSTANBUL";
            model.AddressBook.ReceiverCountry = "Türkiye";
            model.AddressBook.ReceiverPhoneNumber = "2123546363";
            model.AddressBook.ReceiverFaxNumber = "2123543636";
            model.AddressBook.ReceiverEmail = "deneme@deneme.com";
            model.AddressBook.ReceiverWebSite = "www.deneme.com";
            model.AddressBook.ReceiverTaxOffice = "Üsküdar vergi dairesi";

            //GeneralInfoModel
            model.GeneralInfoModel = new GeneralInfoBaseModel();
            model.GeneralInfoModel.Ettn = id;
            

            model.GeneralInfoModel.Prefix = "";
            model.GeneralInfoModel.InvoiceNumber = "";
            model.GeneralInfoModel.InvoiceProfileType = InvoiceProfileType.TEMELFATURA;
            model.GeneralInfoModel.IssueDate = new DateTime(2022, 04, 07, 14, 50, 51);
            model.GeneralInfoModel.Type = InvoiceType.SATIS;
            model.GeneralInfoModel.ReturnInvoiceNumber = null;
            model.GeneralInfoModel.ReturnInvoiceDate = null;
            model.GeneralInfoModel.CurrencyCode = "TRY";
            model.GeneralInfoModel.ExchangeRate = 0;
            //model.GeneralInfoModel.TotalAmount = null;
            //model.GeneralInfoModel.IssueTime = new DateTime(2022, 04, 07, 14, 50, 51);,
            //model.GeneralInfoModel.SlipNumber = null;

            //InvoiceLines
            //var TaxesModelEx = new List<InvoiceLineTaxBaseModel>();
            //var Taxes1 = new InvoiceLineTaxBaseModel() {
            //    TaxName = "",
            //    TaxTypeCode = "",
            //    TaxRate = 0,
            //    TaxAmount = 0,
            //    WithHoldingCode = "",
            //    VatExemptionReasonCode = "",
            //    VatExemptionReason = "",
            //};
            //TaxesModelEx.Add(Taxes1);

            //InvoiceLineDeliveryInfoBaseModel deliveryModel = new InvoiceLineDeliveryInfoBaseModel() {
            //    DeliveryTermsId = "",
            //    PackagingTypeCode = "",
            //    PackagingId = "",
            //    Quantity = "",
            //    TransportModeCode = "",
            //    RequiredCustomsId ="",
            //    DeliveryCountry = "",
            //    DeliveryCity ="",
            //    DeliveryDistrict ="",
            //    DeliveryStreetName ="",
            //    DeliveryBuildingNumber ="",
            //    DeliveryBuildingName = "",
            //    DeliveryPostalZone = "",
            //    AirTransportId = "",
            //    RoadTransportId ="",
            //    RailTransportId="",
            //    MaritimeTransportId=""
            //};
            model.InvoiceLines.Add(new InvoiceLineBaseModel<InvoiceLineTaxBaseModel>()
            {
                InventoryCard = "Asus Laptop",
                Amount = 1,
                DiscountAmount = 870,
                //LineAmount modelde yok
                VatAmount = 1522.1519999999998m,
                UnitCode = "C62",
                UnitPrice = 8700,
                DiscountRate = 10,
                VatRate = 18,
                VatExemptionReasonCode = null,
                Description = "İlk açıklama",
                Note = "ilk note",
                SellersItemIdentification = "11111",
                BuyersItemIdentification = "22222",
                ManufacturersItemIdentification = "33333"


                //VatExemptionReason = "",
                //LineExtensionAmount = 0,
                //Taxes = TaxesModelEx,
                //AllownceCharges ?
                //InvoiceLineDeliveryInfoModel = deliveryModel,
                //SerialNumberList=null,
                //TagNumber="",
                //GoodsOwnerName="",
                //GoodsOwnerIdentifier=""
            });

            //RelatedDispatchList
            //model.RelatedDespatchList.Add(new RelatedDespatchBaseModel()
            //{
            //    DespatchNumber = "",
            //    IssueDate = new System.DateTime(),
            //});

            //UblSettingsModel
            //model.UblSettingsModel.UseCalculatedVatAmount = false;
            //model.UblSettingsModel.UseCalculatedTotalSummary = false;
            //model.UblSettingsModel.HideDespatchMessage = false;

            //PaymentMeansModel
            //model.PaymentMeansModel.PaymentMeansCode = new Models.PaymentMeansType();
            //model.PaymentMeansModel.PaymentDueDate = new System.DateTime();
            //model.PaymentMeansModel.PaymentChannelCode = "";
            //model.PaymentMeansModel.InstructionNote = "";
            //model.PaymentMeansModel.PayeeFinancialAccountId = "";
            //model.PaymentMeansModel.PayeeFinancialAccountCurrencyCode = "";

            //PaymentTermsModel
            //model.PaymentTermsModel.Amount = null;
            //model.PaymentTermsModel.Note = "";
            //model.PaymentTermsModel.PenaltySurchargePercent = null;

            //OrderInfoModel
            //model.OrderInfoModel.OrderNumber = "";
            //model.OrderInfoModel.OrderDate = new System.DateTime();
            //model.OrderInfoModel.InvoiceDocumentModel = new Models.InvoiceDocumentModel()
            //{
            //    InvoiceId = "",
            //    DocumentId = "",
            //    DocumentType = "0:Stok Fişi",
            //    DocumentBase64 = "",
            //    Bytes = null,
            //    FileName = "",
            //    DocumentDate = new System.DateTime(),
            //    IsFileExist = false,
            //    //DocumentDateInString

            //};
            //model.OrderInfoModel.DispatcherNameSurname = "";
            //model.OrderInfoModel.ShipmentDate = new System.DateTime();

            //AdditionalInvoiceTypeInfo
            //model.AdditionalInvoiceTypeInfo.AccountingCostType = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerCode = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerName = "";
            //model.AdditionalInvoiceTypeInfo.DocumentNumber = "";

            //BuyerCustomerInfoModel
            //model.BuyerCustomerInfoModel.FirstName = "";
            //model.BuyerCustomerInfoModel.FamilyName = "";
            //model.BuyerCustomerInfoModel.Nationality = "";
            //model.BuyerCustomerInfoModel.TouristCountry = "";
            //model.BuyerCustomerInfoModel.TouristCity = "";
            //model.BuyerCustomerInfoModel.TouristDistrict = "";
            //model.BuyerCustomerInfoModel.FinancialInstitutionName = "";
            //model.BuyerCustomerInfoModel.PassportNumber = "";
            //model.BuyerCustomerInfoModel.FinancialAccountId = "";
            //model.BuyerCustomerInfoModel.CurrencyCode = "";
            //model.BuyerCustomerInfoModel.PaymentNote = "";
            //model.BuyerCustomerInfoModel.IssueDate = new System.DateTime();
            //model.BuyerCustomerInfoModel.CompanyId = "";
            //model.BuyerCustomerInfoModel.PartyName = "";
            //model.BuyerCustomerInfoModel.RegistrationName = "";
            //model.BuyerCustomerInfoModel.BuyerStreet = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingName = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingNumber = "";
            //model.BuyerCustomerInfoModel.BuyerDoorNumber = "";
            //model.BuyerCustomerInfoModel.BuyerSmallTown = "";
            //model.BuyerCustomerInfoModel.BuyerDistrict = "";
            //model.BuyerCustomerInfoModel.BuyerZipCode = "";
            //model.BuyerCustomerInfoModel.BuyerCity = "";
            //model.BuyerCustomerInfoModel.BuyerCountry = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerFaxNumber = "";
            //model.BuyerCustomerInfoModel.BuyerEmail = "";
            //model.BuyerCustomerInfoModel.BuyerWebSite =  "";
            //model.BuyerCustomerInfoModel.BuyerTaxOffice = "";

            //TaxRepresentativeParyInfoModel
            //model.TaxRepresentativePartyInfoModel.RepresentativeVkn = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeAlias = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCitySubdivisionName = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCity = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCountry = "";

            //AllowanceCharges
            //model.AllowanceCharges.Add(new AllowanceChargeModel() { 
            //    Allo
            //})


            return model;
        }

        /// <summary>
        /// Fonksiyon içerisine değer gönderilmezse ettn numarası null olarak kalır.
        /// </summary>
        /// <returns></returns>
        public static UblBuilderModel fillUblModel()
        {
            var model = new UblBuilderModel();
            model.RecordType = 1;
            model.Status = 20;
            model.XsltCode = null;
            model.LocalReferenceId = null;
            model.UseManualInvoiceId = false;
            model.Note = null;
            model.Notes = null;//new List<NoteModel>();
            //model.Notes.Add(new NoteModel() 
            //    { 
            //        Note = "Test notu"
            //    }
            //);
            //model.Notes.Add(new NoteModel()
            //    {
            //        Note = "Test notu 2"
            //    }
            //);

            //AddressBook
            model.AddressBook = new AddressBookModel();
            model.AddressBook.Name = "Güncel Alıcı adı 2";
            model.AddressBook.ReceiverPersonSurName = "Alıcı Soyadı";
            model.AddressBook.IdentificationNumber = "1234567803";
            model.AddressBook.Alias = "urn:mail:defaulttest3pk@medyasoft.com.tr";
            model.AddressBook.RegisterNumber = "11112222333444";
            model.AddressBook.ReceiverStreet = "Bulvar / cadde / sokak";
            model.AddressBook.ReceiverBuildingName = "Bina adı";
            model.AddressBook.ReceiverBuildingNumber = "1";
            model.AddressBook.ReceiverDoorNumber = "11";
            model.AddressBook.ReceiverSmallTown = "Kasaba / Köy";
            model.AddressBook.ReceiverZipCode = "34000";
            model.AddressBook.ReceiverDistrict = "Üsküdar";
            model.AddressBook.ReceiverCity = "İSTANBUL";
            model.AddressBook.ReceiverCountry = "Türkiye";
            model.AddressBook.ReceiverPhoneNumber = "2123546363";
            model.AddressBook.ReceiverFaxNumber = "2123543636";
            model.AddressBook.ReceiverEmail = "deneme@deneme.com";
            model.AddressBook.ReceiverWebSite = "www.deneme.com";
            model.AddressBook.ReceiverTaxOffice = "Üsküdar vergi dairesi";

            //GeneralInfoModel
            model.GeneralInfoModel = new GeneralInfoBaseModel();
            model.GeneralInfoModel.Ettn = null;//System.Guid.NewGuid();//System.Guid.Parse("");
            model.GeneralInfoModel.Prefix = "";
            model.GeneralInfoModel.InvoiceNumber = "";
            model.GeneralInfoModel.InvoiceProfileType = InvoiceProfileType.TEMELFATURA;
            model.GeneralInfoModel.IssueDate = new DateTime(2022, 04, 07, 14, 50, 51);
            model.GeneralInfoModel.Type = InvoiceType.SATIS;
            model.GeneralInfoModel.ReturnInvoiceNumber = null;
            model.GeneralInfoModel.ReturnInvoiceDate = null;
            model.GeneralInfoModel.CurrencyCode = "TRY";
            model.GeneralInfoModel.ExchangeRate = 0;
            //model.GeneralInfoModel.TotalAmount = null;
            //model.GeneralInfoModel.IssueTime = new DateTime(2022, 04, 07, 14, 50, 51);,
            //model.GeneralInfoModel.SlipNumber = null;

            //InvoiceLines
            //var TaxesModelEx = new List<InvoiceLineTaxBaseModel>();
            //var Taxes1 = new InvoiceLineTaxBaseModel() {
            //    TaxName = "",
            //    TaxTypeCode = "",
            //    TaxRate = 0,
            //    TaxAmount = 0,
            //    WithHoldingCode = "",
            //    VatExemptionReasonCode = "",
            //    VatExemptionReason = "",
            //};
            //TaxesModelEx.Add(Taxes1);

            //InvoiceLineDeliveryInfoBaseModel deliveryModel = new InvoiceLineDeliveryInfoBaseModel() {
            //    DeliveryTermsId = "",
            //    PackagingTypeCode = "",
            //    PackagingId = "",
            //    Quantity = "",
            //    TransportModeCode = "",
            //    RequiredCustomsId ="",
            //    DeliveryCountry = "",
            //    DeliveryCity ="",
            //    DeliveryDistrict ="",
            //    DeliveryStreetName ="",
            //    DeliveryBuildingNumber ="",
            //    DeliveryBuildingName = "",
            //    DeliveryPostalZone = "",
            //    AirTransportId = "",
            //    RoadTransportId ="",
            //    RailTransportId="",
            //    MaritimeTransportId=""
            //};
            model.InvoiceLines.Add(new InvoiceLineBaseModel<InvoiceLineTaxBaseModel>()
            {
                InventoryCard = "Asus Laptop",
                Amount = 1,
                DiscountAmount = 870,
                //LineAmount modelde yok
                VatAmount = 1522.1519999999998m,
                UnitCode = "C62",
                UnitPrice = 8700,
                DiscountRate = 10,
                VatRate = 18,
                VatExemptionReasonCode = null,
                Description = "İlk açıklama",
                Note = "ilk note",
                SellersItemIdentification = "11111",
                BuyersItemIdentification = "22222",
                ManufacturersItemIdentification = "33333"


                //VatExemptionReason = "",
                //LineExtensionAmount = 0,
                //Taxes = TaxesModelEx,
                //AllownceCharges ?
                //InvoiceLineDeliveryInfoModel = deliveryModel,
                //SerialNumberList=null,
                //TagNumber="",
                //GoodsOwnerName="",
                //GoodsOwnerIdentifier=""
            });

            //RelatedDispatchList
            //model.RelatedDespatchList.Add(new RelatedDespatchBaseModel()
            //{
            //    DespatchNumber = "",
            //    IssueDate = new System.DateTime(),
            //});

            //UblSettingsModel
            //model.UblSettingsModel.UseCalculatedVatAmount = false;
            //model.UblSettingsModel.UseCalculatedTotalSummary = false;
            //model.UblSettingsModel.HideDespatchMessage = false;

            //PaymentMeansModel
            //model.PaymentMeansModel.PaymentMeansCode = new Models.PaymentMeansType();
            //model.PaymentMeansModel.PaymentDueDate = new System.DateTime();
            //model.PaymentMeansModel.PaymentChannelCode = "";
            //model.PaymentMeansModel.InstructionNote = "";
            //model.PaymentMeansModel.PayeeFinancialAccountId = "";
            //model.PaymentMeansModel.PayeeFinancialAccountCurrencyCode = "";

            //PaymentTermsModel
            //model.PaymentTermsModel.Amount = null;
            //model.PaymentTermsModel.Note = "";
            //model.PaymentTermsModel.PenaltySurchargePercent = null;

            //OrderInfoModel
            //model.OrderInfoModel.OrderNumber = "";
            //model.OrderInfoModel.OrderDate = new System.DateTime();
            //model.OrderInfoModel.InvoiceDocumentModel = new Models.InvoiceDocumentModel()
            //{
            //    InvoiceId = "",
            //    DocumentId = "",
            //    DocumentType = "0:Stok Fişi",
            //    DocumentBase64 = "",
            //    Bytes = null,
            //    FileName = "",
            //    DocumentDate = new System.DateTime(),
            //    IsFileExist = false,
            //    //DocumentDateInString

            //};
            //model.OrderInfoModel.DispatcherNameSurname = "";
            //model.OrderInfoModel.ShipmentDate = new System.DateTime();

            //AdditionalInvoiceTypeInfo
            //model.AdditionalInvoiceTypeInfo.AccountingCostType = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerCode = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerName = "";
            //model.AdditionalInvoiceTypeInfo.DocumentNumber = "";

            //BuyerCustomerInfoModel
            //model.BuyerCustomerInfoModel.FirstName = "";
            //model.BuyerCustomerInfoModel.FamilyName = "";
            //model.BuyerCustomerInfoModel.Nationality = "";
            //model.BuyerCustomerInfoModel.TouristCountry = "";
            //model.BuyerCustomerInfoModel.TouristCity = "";
            //model.BuyerCustomerInfoModel.TouristDistrict = "";
            //model.BuyerCustomerInfoModel.FinancialInstitutionName = "";
            //model.BuyerCustomerInfoModel.PassportNumber = "";
            //model.BuyerCustomerInfoModel.FinancialAccountId = "";
            //model.BuyerCustomerInfoModel.CurrencyCode = "";
            //model.BuyerCustomerInfoModel.PaymentNote = "";
            //model.BuyerCustomerInfoModel.IssueDate = new System.DateTime();
            //model.BuyerCustomerInfoModel.CompanyId = "";
            //model.BuyerCustomerInfoModel.PartyName = "";
            //model.BuyerCustomerInfoModel.RegistrationName = "";
            //model.BuyerCustomerInfoModel.BuyerStreet = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingName = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingNumber = "";
            //model.BuyerCustomerInfoModel.BuyerDoorNumber = "";
            //model.BuyerCustomerInfoModel.BuyerSmallTown = "";
            //model.BuyerCustomerInfoModel.BuyerDistrict = "";
            //model.BuyerCustomerInfoModel.BuyerZipCode = "";
            //model.BuyerCustomerInfoModel.BuyerCity = "";
            //model.BuyerCustomerInfoModel.BuyerCountry = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerFaxNumber = "";
            //model.BuyerCustomerInfoModel.BuyerEmail = "";
            //model.BuyerCustomerInfoModel.BuyerWebSite =  "";
            //model.BuyerCustomerInfoModel.BuyerTaxOffice = "";

            //TaxRepresentativeParyInfoModel
            //model.TaxRepresentativePartyInfoModel.RepresentativeVkn = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeAlias = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCitySubdivisionName = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCity = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCountry = "";

            //AllowanceCharges
            //model.AllowanceCharges.Add(new AllowanceChargeModel() { 
            //    Allo
            //})


            return model;
        }

        public static CreateInvoiceModelV2 fillUblModelWithFile(string FullPath)
        {
            var model = new CreateInvoiceModelV2();
            model.InvoiceFile = FullPath;
            model.AppType = 1;
            model.Status = 20;
            model.LocalReferenceId = "";
            model.Prefix = "";
            model.XsltCode = "";
            model.CheckLocalReferenceId = false;
            model.TargetAlias = "urn:mail:defaulttest3pk@medyasoft.com.tr";//Test sisteminde 1234567803 vkn sine ait alias değeridir. (Alıcı posta kutusu)
            model.UseFirstAlias = false;
            return model;
        }

        /// <summary>
        /// Fonksiyon parametresiz çağırılırsa ettn numarası otomatik olarak atanır ve fatura oluşturulur.
        /// </summary>
        /// <returns></returns>
        public static UblBuilderModel fillEArchiveUblModel()
        {

            var model = new UblBuilderModel();
            model.RecordType = 0;
            model.Status = 20;
            model.IsNew = true;
            model.LocalReferenceId = null;
            model.UseManualInvoiceId = false;
            model.Note = null;
            //model.XsltCode = null;
            //model.Notes.Add(new NoteModel()
            //{
            //    Note = "Test notu"
            //}
            //);
            //model.Notes.Add(new NoteModel()
            //{
            //    Note = "Test notu 2"
            //}
            //);


            //GeneralInfoModel
            model.GeneralInfoModel = new GeneralInfoBaseModel();
            model.GeneralInfoModel.Ettn = null;
            model.GeneralInfoModel.InvoiceProfileType = InvoiceProfileType.EARSIVFATURA;
            model.GeneralInfoModel.IssueDate = new DateTime(2022, 04, 07, 14, 50, 51);
            model.GeneralInfoModel.Type = InvoiceType.SATIS;
            model.GeneralInfoModel.CurrencyCode = "TRY";
            model.GeneralInfoModel.ExchangeRate = 0;
            model.GeneralInfoModel.TotalAmount = null;
            model.GeneralInfoModel.InvoiceNumber = "";
            model.GeneralInfoModel.IssueTime = new DateTime(2022, 04, 07, 14, 50, 51);
            model.GeneralInfoModel.Prefix = null;
            model.GeneralInfoModel.ReturnInvoiceDate = null;
            model.GeneralInfoModel.ReturnInvoiceNumber = null;
            model.GeneralInfoModel.SlipNumber = null;


            //AddressBook
            model.AddressBook = new AddressBookModel();
            model.AddressBook.IdentificationNumber = "1123581321";
            model.AddressBook.Name = "Burak";
            model.AddressBook.ReceiverBuildingName = null;
            model.AddressBook.ReceiverPersonSurName = "";
            //Modelde receiver number yok
            model.AddressBook.ReceiverStreet = "";
            model.AddressBook.ReceiverEmail = "abb@gmail.com";
            model.AddressBook.ReceiverDistrict = "PENDİK";
            model.AddressBook.ReceiverCity = "İSTANBUL";
            model.AddressBook.ReceiverCountry = "Türkiye";
            model.AddressBook.RegisterNumber = null;
            model.AddressBook.ReceiverTaxOffice = "";
            model.AddressBook.ReceiverPhoneNumber = "";
            //model.AddressBook.ReceiverDoorNumber = "";
            //model.AddressBook.ReceiverSmallTown = "";
            //model.AddressBook.ReceiverZipCode = "";
            //model.AddressBook.ReceiverFaxNumber = "";
            //model.AddressBook.ReceiverWebSite = "";



            //InvoiceLines
            //var TaxesModelEx = new List<InvoiceLineTaxBaseModel>();
            //var Taxes1 = new InvoiceLineTaxBaseModel()
            //{
            //    TaxName = "",
            //    TaxTypeCode = "",
            //    TaxRate = 0,
            //    TaxAmount = 0,
            //    WithHoldingCode = "",
            //    VatExemptionReasonCode = "",
            //    VatExemptionReason = "",
            //};
            //TaxesModelEx.Add(Taxes1);
            //InvoiceLineDeliveryInfoBaseModel deliveryModel = new InvoiceLineDeliveryInfoBaseModel()
            //{
            //    DeliveryTermsId = "",
            //    PackagingTypeCode = "",
            //    PackagingId = "",
            //    Quantity = "",
            //    TransportModeCode = "",
            //    RequiredCustomsId = "",
            //    DeliveryCountry = "",
            //    DeliveryCity = "",
            //    DeliveryDistrict = "",
            //    DeliveryStreetName = "",
            //    DeliveryBuildingNumber = "",
            //    DeliveryBuildingName = "",
            //    DeliveryPostalZone = "",
            //    AirTransportId = "",
            //    RoadTransportId = "",
            //    RailTransportId = "",
            //    MaritimeTransportId = ""
            //};


            model.InvoiceLines.Add(new InvoiceLineBaseModel<InvoiceLineTaxBaseModel>()
            {
                InventoryCard = "YEDEK SİM KART(%8)",
                //disableVatExemption modelde yok
                Amount = 1,
                UnitCode = "C62",
                UnitPrice = 0.05m,
                VatRate = 8.0m,
                DiscountAmount = 0,
                VatAmount = 0,
                LineExtensionAmount = 0.05m,
                VatExemptionReasonCode = ""
                //Description = "",
                //DiscountRate = 0,
                //VatExemptionReason = "",
                //Note = "",
                //SellersItemIdentification = "",
                //BuyersItemIdentification = "",
                //ManufacturersItemIdentification = "",
                //Taxes = TaxesModelEx,
                //AllownceCharges ?
                //InvoiceLineDeliveryInfoModel = deliveryModel,
                //SerialNumberList = null,
                //TagNumber = "",
                //GoodsOwnerName = "",
                //GoodsOwnerIdentifier = ""
            });

            //ArchiveInfoModel
            model.ArchiveInfoModel = new ArchiveInfoBaseModel();
            model.ArchiveInfoModel.IsInternetSale = false;
            model.ArchiveInfoModel.ShipmentDate = null;
            model.ArchiveInfoModel.ShipmentSendType = null;
            model.ArchiveInfoModel.ShipmentSenderName = null;
            model.ArchiveInfoModel.ShipmentSenderSurname = null;
            model.ArchiveInfoModel.ShipmentSenderTcknVkn = null;
            model.ArchiveInfoModel.SubscriptionType = null;
            model.ArchiveInfoModel.SubscriptionTypeNumber = null;
            model.ArchiveInfoModel.WebsiteUrl = null;

            //PaymentMeansModel
            model.PaymentMeansModel = new PaymentMeansBaseModel();
            model.PaymentMeansModel.InstructionNote = null;
            model.PaymentMeansModel.PayeeFinancialAccountCurrencyCode = "TRY";
            model.PaymentMeansModel.PayeeFinancialAccountId = null;
            model.PaymentMeansModel.PaymentMeansCode = 0;
            model.PaymentMeansModel.PaymentDueDate = null;
            model.PaymentMeansModel.PaymentChannelCode = "0";

            //UblSettingsModel
            model.UblSettingsModel = new UblSettingsModel();
            model.UblSettingsModel.UseCalculatedVatAmount = true;
            model.UblSettingsModel.HideDespatchMessage = false;
            //model.UblSettingsModel.UseCalculatedTotalSummary = false;

            //EArchiveInfo
            model.EArsivInfo = new EArsivInfoModel();
            model.EArsivInfo.SendEMail = true;
            model.EArsivInfo.EMailAddress = "abb@gmail.com";
            //model.EArsivInfo.AllowOldEArsivCustomer = true;




            ////RelatedDispatchList
            //model.RelatedDespatchList.Add(new RelatedDespatchBaseModel()
            //{
            //    DespatchNumber = "",
            //    IssueDate = new System.DateTime(),
            //});

            ////PaymentTermsModel
            //model.PaymentTermsModel.Amount = null;
            //model.PaymentTermsModel.Note = "";
            //model.PaymentTermsModel.PenaltySurchargePercent = null;

            ////OrderInfoModel
            //model.OrderInfoModel.OrderNumber = "";
            //model.OrderInfoModel.OrderDate = new System.DateTime();
            //model.OrderInfoModel.InvoiceDocumentModel = new Models.InvoiceDocumentModel()
            //{
            //    InvoiceId = "",
            //    DocumentId = "",
            //    DocumentType = "0:Stok Fişi",
            //    DocumentBase64 = "",
            //    Bytes = null,
            //    FileName = "",
            //    DocumentDate = new System.DateTime(),
            //    IsFileExist = false,
            //    //DocumentDateInString?

            //};
            //model.OrderInfoModel.DispatcherNameSurname = "";
            //model.OrderInfoModel.ShipmentDate = new System.DateTime();





            ////AdditionalInvoiceTypeInfo
            //model.AdditionalInvoiceTypeInfo.AccountingCostType = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerCode = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerName = "";
            //model.AdditionalInvoiceTypeInfo.DocumentNumber = "";

            ////BuyerCustomerInfoModel
            //model.BuyerCustomerInfoModel.FirstName = "";
            //model.BuyerCustomerInfoModel.FamilyName = "";
            //model.BuyerCustomerInfoModel.Nationality = "";
            //model.BuyerCustomerInfoModel.TouristCountry = "";
            //model.BuyerCustomerInfoModel.TouristCity = "";
            //model.BuyerCustomerInfoModel.TouristDistrict = "";
            //model.BuyerCustomerInfoModel.FinancialInstitutionName = "";
            //model.BuyerCustomerInfoModel.PassportNumber = "";
            //model.BuyerCustomerInfoModel.FinancialAccountId = "";
            //model.BuyerCustomerInfoModel.CurrencyCode = "";
            //model.BuyerCustomerInfoModel.PaymentNote = "";
            //model.BuyerCustomerInfoModel.IssueDate = new System.DateTime();
            //model.BuyerCustomerInfoModel.CompanyId = "";
            //model.BuyerCustomerInfoModel.PartyName = "";
            //model.BuyerCustomerInfoModel.RegistrationName = "";
            //model.BuyerCustomerInfoModel.BuyerStreet = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingName = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingNumber = "";
            //model.BuyerCustomerInfoModel.BuyerDoorNumber = "";
            //model.BuyerCustomerInfoModel.BuyerSmallTown = "";
            //model.BuyerCustomerInfoModel.BuyerDistrict = "";
            //model.BuyerCustomerInfoModel.BuyerZipCode = "";
            //model.BuyerCustomerInfoModel.BuyerCity = "";
            //model.BuyerCustomerInfoModel.BuyerCountry = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerFaxNumber = "";
            //model.BuyerCustomerInfoModel.BuyerEmail = "";
            //model.BuyerCustomerInfoModel.BuyerWebSite = "";
            //model.BuyerCustomerInfoModel.BuyerTaxOffice = "";

            ////TaxRepresentativeParyInfoModel
            //model.TaxRepresentativePartyInfoModel.RepresentativeVkn = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeAlias = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCitySubdivisionName = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCity = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCountry = "";

            ////AllowanceCharges
            ////model.AllowanceCharges.Add(new AllowanceChargeModel() { 
            ////    Allo
            ////})


            return model;
        }

        /// <summary>
        /// Fonksiyona guid olarak id parametresi gönderilirse bu id ile fatura modeli doldurulur.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UblBuilderModel fillEArchiveUblModel(Guid id)
        {

            var model = new UblBuilderModel();
            model.RecordType = 0;
            model.Status = 20;
            model.IsNew = true;
            model.LocalReferenceId = null;
            model.UseManualInvoiceId = false;
            model.Note = null;
            //model.XsltCode = null;
            //model.Notes.Add(new NoteModel()
            //{
            //    Note = "Test notu"
            //}
            //);
            //model.Notes.Add(new NoteModel()
            //{
            //    Note = "Test notu 2"
            //}
            //);


            //GeneralInfoModel
            model.GeneralInfoModel = new GeneralInfoBaseModel();
            model.GeneralInfoModel.Ettn = id;
            model.GeneralInfoModel.InvoiceProfileType = InvoiceProfileType.EARSIVFATURA;
            model.GeneralInfoModel.IssueDate = new DateTime(2022, 04, 07, 14, 50, 51);
            model.GeneralInfoModel.Type = InvoiceType.SATIS;
            model.GeneralInfoModel.CurrencyCode = "TRY";
            model.GeneralInfoModel.ExchangeRate = 0;
            model.GeneralInfoModel.TotalAmount = null;
            model.GeneralInfoModel.InvoiceNumber = "";
            model.GeneralInfoModel.IssueTime = new DateTime(2022, 04, 07, 14, 50, 51);
            model.GeneralInfoModel.Prefix = null;
            model.GeneralInfoModel.ReturnInvoiceDate = null;
            model.GeneralInfoModel.ReturnInvoiceNumber = null;
            model.GeneralInfoModel.SlipNumber = null;


            //AddressBook
            model.AddressBook = new AddressBookModel();
            model.AddressBook.IdentificationNumber = "1123581321";
            model.AddressBook.Name = "Burak";
            model.AddressBook.ReceiverBuildingName = null;
            model.AddressBook.ReceiverPersonSurName = "";
            //Modelde receiver number yok
            model.AddressBook.ReceiverStreet = "";
            model.AddressBook.ReceiverEmail = "abb@gmail.com";
            model.AddressBook.ReceiverDistrict = "PENDİK";
            model.AddressBook.ReceiverCity = "İSTANBUL";
            model.AddressBook.ReceiverCountry = "Türkiye";
            model.AddressBook.RegisterNumber = null;
            model.AddressBook.ReceiverTaxOffice = "";
            model.AddressBook.ReceiverPhoneNumber = "";
            //model.AddressBook.ReceiverDoorNumber = "";
            //model.AddressBook.ReceiverSmallTown = "";
            //model.AddressBook.ReceiverZipCode = "";
            //model.AddressBook.ReceiverFaxNumber = "";
            //model.AddressBook.ReceiverWebSite = "";



            //InvoiceLines
            //var TaxesModelEx = new List<InvoiceLineTaxBaseModel>();
            //var Taxes1 = new InvoiceLineTaxBaseModel()
            //{
            //    TaxName = "",
            //    TaxTypeCode = "",
            //    TaxRate = 0,
            //    TaxAmount = 0,
            //    WithHoldingCode = "",
            //    VatExemptionReasonCode = "",
            //    VatExemptionReason = "",
            //};
            //TaxesModelEx.Add(Taxes1);
            //InvoiceLineDeliveryInfoBaseModel deliveryModel = new InvoiceLineDeliveryInfoBaseModel()
            //{
            //    DeliveryTermsId = "",
            //    PackagingTypeCode = "",
            //    PackagingId = "",
            //    Quantity = "",
            //    TransportModeCode = "",
            //    RequiredCustomsId = "",
            //    DeliveryCountry = "",
            //    DeliveryCity = "",
            //    DeliveryDistrict = "",
            //    DeliveryStreetName = "",
            //    DeliveryBuildingNumber = "",
            //    DeliveryBuildingName = "",
            //    DeliveryPostalZone = "",
            //    AirTransportId = "",
            //    RoadTransportId = "",
            //    RailTransportId = "",
            //    MaritimeTransportId = ""
            //};


            model.InvoiceLines.Add(new InvoiceLineBaseModel<InvoiceLineTaxBaseModel>()
            {
                InventoryCard = "YEDEK SİM KART(%8)",
                //disableVatExemption modelde yok
                Amount = 1,
                UnitCode = "C62",
                UnitPrice = 0.05m,
                VatRate = 8.0m,
                DiscountAmount = 0,
                VatAmount = 0,
                LineExtensionAmount = 0.05m,
                VatExemptionReasonCode = ""
                //Description = "",
                //DiscountRate = 0,
                //VatExemptionReason = "",
                //Note = "",
                //SellersItemIdentification = "",
                //BuyersItemIdentification = "",
                //ManufacturersItemIdentification = "",
                //Taxes = TaxesModelEx,
                //AllownceCharges ?
                //InvoiceLineDeliveryInfoModel = deliveryModel,
                //SerialNumberList = null,
                //TagNumber = "",
                //GoodsOwnerName = "",
                //GoodsOwnerIdentifier = ""
            });

            //ArchiveInfoModel
            model.ArchiveInfoModel = new ArchiveInfoBaseModel();
            model.ArchiveInfoModel.IsInternetSale = false;
            model.ArchiveInfoModel.ShipmentDate = null;
            model.ArchiveInfoModel.ShipmentSendType = null;
            model.ArchiveInfoModel.ShipmentSenderName = null;
            model.ArchiveInfoModel.ShipmentSenderSurname = null;
            model.ArchiveInfoModel.ShipmentSenderTcknVkn = null;
            model.ArchiveInfoModel.SubscriptionType = null;
            model.ArchiveInfoModel.SubscriptionTypeNumber = null;
            model.ArchiveInfoModel.WebsiteUrl = null;

            //PaymentMeansModel
            model.PaymentMeansModel = new PaymentMeansBaseModel();
            model.PaymentMeansModel.InstructionNote = null;
            model.PaymentMeansModel.PayeeFinancialAccountCurrencyCode = "TRY";
            model.PaymentMeansModel.PayeeFinancialAccountId = null;
            model.PaymentMeansModel.PaymentMeansCode = 0;
            model.PaymentMeansModel.PaymentDueDate = null;
            model.PaymentMeansModel.PaymentChannelCode = "0";

            //UblSettingsModel
            model.UblSettingsModel = new UblSettingsModel();
            model.UblSettingsModel.UseCalculatedVatAmount = true;
            model.UblSettingsModel.HideDespatchMessage = false;
            //model.UblSettingsModel.UseCalculatedTotalSummary = false;

            //EArchiveInfo
            model.EArsivInfo = new EArsivInfoModel();
            model.EArsivInfo.SendEMail = true;
            model.EArsivInfo.EMailAddress = "abb@gmail.com";
            //model.EArsivInfo.AllowOldEArsivCustomer = true;




            ////RelatedDispatchList
            //model.RelatedDespatchList.Add(new RelatedDespatchBaseModel()
            //{
            //    DespatchNumber = "",
            //    IssueDate = new System.DateTime(),
            //});

            ////PaymentTermsModel
            //model.PaymentTermsModel.Amount = null;
            //model.PaymentTermsModel.Note = "";
            //model.PaymentTermsModel.PenaltySurchargePercent = null;

            ////OrderInfoModel
            //model.OrderInfoModel.OrderNumber = "";
            //model.OrderInfoModel.OrderDate = new System.DateTime();
            //model.OrderInfoModel.InvoiceDocumentModel = new Models.InvoiceDocumentModel()
            //{
            //    InvoiceId = "",
            //    DocumentId = "",
            //    DocumentType = "0:Stok Fişi",
            //    DocumentBase64 = "",
            //    Bytes = null,
            //    FileName = "",
            //    DocumentDate = new System.DateTime(),
            //    IsFileExist = false,
            //    //DocumentDateInString?

            //};
            //model.OrderInfoModel.DispatcherNameSurname = "";
            //model.OrderInfoModel.ShipmentDate = new System.DateTime();





            ////AdditionalInvoiceTypeInfo
            //model.AdditionalInvoiceTypeInfo.AccountingCostType = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerCode = "";
            //model.AdditionalInvoiceTypeInfo.TaxPayerName = "";
            //model.AdditionalInvoiceTypeInfo.DocumentNumber = "";

            ////BuyerCustomerInfoModel
            //model.BuyerCustomerInfoModel.FirstName = "";
            //model.BuyerCustomerInfoModel.FamilyName = "";
            //model.BuyerCustomerInfoModel.Nationality = "";
            //model.BuyerCustomerInfoModel.TouristCountry = "";
            //model.BuyerCustomerInfoModel.TouristCity = "";
            //model.BuyerCustomerInfoModel.TouristDistrict = "";
            //model.BuyerCustomerInfoModel.FinancialInstitutionName = "";
            //model.BuyerCustomerInfoModel.PassportNumber = "";
            //model.BuyerCustomerInfoModel.FinancialAccountId = "";
            //model.BuyerCustomerInfoModel.CurrencyCode = "";
            //model.BuyerCustomerInfoModel.PaymentNote = "";
            //model.BuyerCustomerInfoModel.IssueDate = new System.DateTime();
            //model.BuyerCustomerInfoModel.CompanyId = "";
            //model.BuyerCustomerInfoModel.PartyName = "";
            //model.BuyerCustomerInfoModel.RegistrationName = "";
            //model.BuyerCustomerInfoModel.BuyerStreet = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingName = "";
            //model.BuyerCustomerInfoModel.BuyerBuildingNumber = "";
            //model.BuyerCustomerInfoModel.BuyerDoorNumber = "";
            //model.BuyerCustomerInfoModel.BuyerSmallTown = "";
            //model.BuyerCustomerInfoModel.BuyerDistrict = "";
            //model.BuyerCustomerInfoModel.BuyerZipCode = "";
            //model.BuyerCustomerInfoModel.BuyerCity = "";
            //model.BuyerCustomerInfoModel.BuyerCountry = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerPhoneNumber = "";
            //model.BuyerCustomerInfoModel.BuyerFaxNumber = "";
            //model.BuyerCustomerInfoModel.BuyerEmail = "";
            //model.BuyerCustomerInfoModel.BuyerWebSite = "";
            //model.BuyerCustomerInfoModel.BuyerTaxOffice = "";

            ////TaxRepresentativeParyInfoModel
            //model.TaxRepresentativePartyInfoModel.RepresentativeVkn = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeAlias = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCitySubdivisionName = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCity = "";
            //model.TaxRepresentativePartyInfoModel.RepresentativeCountry = "";

            ////AllowanceCharges
            ////model.AllowanceCharges.Add(new AllowanceChargeModel() { 
            ////    Allo
            ////})


            return model;
        }

        public static CreateEarchiveInvoiceModel fillEArchiveModelWithFile(string FullPath)
        {
            var model = new CreateEarchiveInvoiceModel();
            model.InvoiceFile = FullPath;
            model.Status = 20;
            model.LocalReferenceId = "";
            model.CheckLocalReferenceId = false;
            model.Prefix = "";
            model.UseManualInvoiceId = false;
            model.XsltCode = "";
            model.SendEMail = false;
            model.EMailAddress = "";
            model.AllowOldEArsivCustomer = false;

            return model;
        }
    }
}
