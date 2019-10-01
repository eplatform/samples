using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ePlatform.Integration.Models.Invoice21;

namespace ePlatform.Integration.Helpers
{
    public class UblHandler
    {
        public (string, InvoiceType) CreateUbl()
        {
            // PostZip metodu ile ilk önce göndermek istediğiniz faturayı XML olarak oluşturmaktayız,
            //XML oluşturmak için ePlatform.eFatura.Service.Models.Invoice21 Modelinden yararlanılmaktadır,
            //Invoice21 modelini istemiş olduğunuz bilgiler ile oluşturduktan sonra,
            //İlgili Modeli ilk önce XML formatına dönüştürerek dönüştürülen XML dosyasınıda Ziplemek için
            //bytcode dönüştürmekteyiz daha sonra bu bytcodu Base64 formatıyla stringe dönültürerek outbox uç noktasına
            //Http.Post isteği ile faturayı zip olarak gönderebilirsiniz,
            //Model Doldurma aşaması

            // Aşağıda oluşturulan modeli Outfiles içerisine bytcode ve xml olarak kaydetmemizi sağlar.

            //Müşteri Başladı
            var customer_partyIdentification = new List<PartyIdentificationType>();
            customer_partyIdentification.Add(new PartyIdentificationType { ID = new IDType { schemeID = "TCKN", Value = "36509171316" } });
            //Müşteri Bitti

            //Satıcı Başladı
            var supplierParty_partyIdentification = new List<PartyIdentificationType>();
            supplierParty_partyIdentification.Add(new PartyIdentificationType { ID = new IDType { schemeID = "VKN", Value = "1234567803" } });
            var supplierParty_communicationType = new List<CommunicationType>();
            supplierParty_communicationType.Add(new CommunicationType { Value = new ePlatform.Integration.Models.Invoice21.ValueType { Value = "denemee" } });
            //Satıcı Bitti

            //Vergiler
            var taxSubTotal = new List<TaxSubtotalType>();
            taxSubTotal.Add(new TaxSubtotalType
            {
                TaxAmount = new TaxAmountType { Value = 2214, currencyID = "TRY" },
                Percent = new PercentType1 { Value = 18 },
                TaxCategory = new TaxCategoryType { TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "KDV Gerçek" }, TaxTypeCode = new TaxTypeCodeType { Value = "0015" } } }
            });
            var taxTotal = new List<TaxTotalType>();
            taxTotal.Add(new TaxTotalType
            {
                TaxAmount = new TaxAmountType { currencyID = "TRY", Value = 2214 },
                TaxSubtotal = taxSubTotal.ToArray()
            });

            //not
            var note = new List<NoteType>();
            note.Add(new NoteType { Value = "Not1" });
            //not

            //kalemler başladı
            var taxSubTotalList = new List<TaxSubtotalType>();//Oluşturduğumuz kdv ve vergi alanını oluşturuyoruz Bu alan içerisinde gönderilen bilgiler doğrultusunda fatura oluşturulur.
            taxSubTotalList.Add(new TaxSubtotalType
            {
                TaxableAmount = new TaxableAmountType { Value = 60, currencyID = "TRY" },
                TaxAmount = new TaxAmountType { Value = 10.80m, currencyID = "TRY" },
                Percent = new PercentType1 { Value = 18 },
                TaxCategory = new TaxCategoryType { TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "KDV Gerçek" }, TaxTypeCode = new TaxTypeCodeType { Value = "0015" } } }
            });

            var line = new List<InvoiceLineType>();
            line.Add(new InvoiceLineType // Bu alanda belirlenen değerler faturaya otomatik olarak yazılmaktadır.
            {
                ID = new IDType { Value = "1" },//Kaçıncı invoiceLines Olduğunu belirtir
                InvoicedQuantity = new InvoicedQuantityType { unitCode = "C62", Value = 12 },//Adet ve 13 tane olduğunu belirtir.
                Price = new PriceType { PriceAmount = new PriceAmountType { currencyID = "TRY", Value = 5 } },//birim fiyatını belirtir.
                LineExtensionAmount = new LineExtensionAmountType { currencyID = "TRY", Value = 60 },//birim fiyatı ve adet tutar çarpımı sonucunda mal/hizmet tutarını belirtir.
                Item = new ItemType { Name = new NameType1 { Value = "NoteBook" }, SellersItemIdentification = new ItemIdentificationType { ID = new IDType { Value = "123456" } } },//item bilgilerini belirtir
                TaxTotal = new TaxTotalType
                {
                    TaxAmount = new TaxAmountType { Value = 10.80m, currencyID = "TRY" },
                    TaxSubtotal = taxSubTotalList.ToArray()
                }
            });

            //AdditionalDocumentReference
            var additionalDocumentReference = new List<DocumentReferenceType>();

            //kalemler bitti   

            //string path = @"general.xslt";
            //string s = File.ReadAllText(path, Encoding.UTF8);
            //byte[] bytes = Encoding.UTF8.GetBytes(s);

            ////Xslt eklenmesi
            //additionalDocumentReference.Add(new DocumentReferenceType { ID = new IDType { Value = "CDA79E4E-EE13-4D9C-B625-87286EC30358" }, 
            // IssueDate = new IssueDateType { Value = Convert.ToDateTime("2015-01-01") }, DocumentType = new DocumentTypeType { Value = "Xslt" }, 
            // Attachment = new AttachmentType { EmbeddedDocumentBinaryObject = new EmbeddedDocumentBinaryObjectType { characterSetCode = "UTF-8", 
            // encodingCode = "Base64", filename = "WRK2015000000001.xslt", mimeCode = "application/xml", Value = bytes } } });
            additionalDocumentReference.Add(new DocumentReferenceType
            {
                ID = new IDType { Value = "SendType" },//Gönderimi zorunludur
                IssueDate = new IssueDateType { Value = Convert.ToDateTime("2019-01-01") },
                DocumentType = new DocumentTypeType { Value = "KAGIT" },
            });
            additionalDocumentReference.Add(new DocumentReferenceType
            {
                ID = new IDType { Value = "IsInternetSale" },//gönderimi zorunludur
                IssueDate = new IssueDateType { Value = Convert.ToDateTime("2019-01-01") },
                DocumentType = new DocumentTypeType { Value = "false" },
            });

            var invoiceType = new InvoiceType
            {
                UBLVersionID = new UBLVersionIDType { Value = "2.1" },
                CustomizationID = new CustomizationIDType { Value = "TR1.2" },
                ProfileID = new ProfileIDType { Value = "EARSIVFATURA" },
                InvoiceTypeCode = new InvoiceTypeCodeType { Value = "SATIS" },
                CopyIndicator = new CopyIndicatorType { Value = false },
                ID = new IDType { Value = "WRK2019000000015" },
                IssueDate = new IssueDateType { Value = Convert.ToDateTime("2019-01-01") },
                IssueTime = new IssueTimeType { Value = Convert.ToDateTime("00:00") },
                UUID = new UUIDType { Value = Guid.NewGuid().ToString().ToUpperInvariant() },
                DocumentCurrencyCode = new DocumentCurrencyCodeType { Value = "TRY" },
                LineCountNumeric = new LineCountNumericType { Value = 1 },
                AdditionalDocumentReference = additionalDocumentReference.ToArray(),
                TaxTotal = taxTotal.ToArray(),
                AccountingCustomerParty = new CustomerPartyType
                {
                    Party = new PartyType
                    {
                        WebsiteURI = new WebsiteURIType { Value = "www.xxxx.com.tr" },
                        PartyIdentification = customer_partyIdentification.ToArray(),
                        PartyName = new PartyNameType { Name = new NameType1 { Value = "Müşteri Firma Deneme Faturası Ltd.Şti" } },
                        PartyTaxScheme = new PartyTaxSchemeType { TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "deneme" }, TaxTypeCode = new TaxTypeCodeType { Value = "deneme" } } },
                        Contact = new ContactType
                        {
                            ElectronicMail = new ElectronicMailType { Value = "eposta@hotmail.com" },
                            Note = new NoteType { Value = "deneme" },
                            Telefax = new TelefaxType { Value = "03225982030" },
                            Telephone = new TelephoneType { Value = "03225982030" }
                        },
                        PostalAddress = new AddressType
                        {
                            Country = new CountryType { Name = new NameType1 { Value = "CountryName" } },
                            Room = new RoomType { Value = "room" },
                            StreetName = new StreetNameType { Value = "street" },
                            BuildingName = new BuildingNameType { Value = "buldingname" },
                            BuildingNumber = new BuildingNumberType { Value = "BuildingNumber" },
                            CitySubdivisionName = new CitySubdivisionNameType { Value = "CitySubdivisionName" },
                            CityName = new CityNameType { Value = "CityName" },
                            Region = new RegionType { Value = "region" }
                        },
                    }
                },
                AccountingSupplierParty = new SupplierPartyType
                {
                    Party = new PartyType
                    {
                        WebsiteURI = new WebsiteURIType { Value = "www.xxxx.com.tr" },
                        PartyIdentification = supplierParty_partyIdentification.ToArray(),
                        PartyName = new PartyNameType { Name = new NameType1 { Value = "Satıcı Firma Ltd.Şti" } },
                        PartyTaxScheme = new PartyTaxSchemeType
                        {
                            TaxScheme = new TaxSchemeType
                            {
                                Name = new NameType1 { Value = "deneme" },
                                TaxTypeCode = new TaxTypeCodeType { Value = "deneme" }
                            }
                        },
                        Contact = new ContactType
                        {
                            ElectronicMail = new ElectronicMailType { Value = "eposta@hotmail.com" },
                            Note = new NoteType { Value = "deneme" },
                            Telefax = new TelefaxType { Value = "03225982030" },
                            Telephone = new TelephoneType { Value = "03225982030" }
                        },
                        PostalAddress = new AddressType
                        {
                            Country = new CountryType { Name = new NameType1 { Value = "CountryName" } },
                            Room = new RoomType { Value = "room" },
                            StreetName = new StreetNameType { Value = "street" },
                            BuildingName = new BuildingNameType { Value = "buldingname" },
                            BuildingNumber = new BuildingNumberType { Value = "BuildingNumber" },
                            CitySubdivisionName = new CitySubdivisionNameType { Value = "CitySubdivisionName" },
                            CityName = new CityNameType { Value = "CityName" },
                            Region = new RegionType { Value = "region" }
                        },
                    }
                },
                LegalMonetaryTotal = new MonetaryTotalType
                {
                    LineExtensionAmount = new LineExtensionAmountType { currencyID = "TRY", Value = 12300 },
                    TaxExclusiveAmount = new TaxExclusiveAmountType { currencyID = "TRY", Value = 12300 },
                    TaxInclusiveAmount = new TaxInclusiveAmountType { currencyID = "TRY", Value = 14514 },
                    AllowanceTotalAmount = new AllowanceTotalAmountType { currencyID = "TRY", Value = 0 },
                    PayableAmount = new PayableAmountType { currencyID = "TRY", Value = 14514 }
                },
                Note = note.ToArray(),

                InvoiceLine = line.ToArray(),
            };


            //Invoice21 Modeli kullanılarak model doldurma işlemi Sona ermiştir,

            string invoiceBase64Model = default;

            using (var stream = new MemoryStream())
            {
                //Oluşturmuş olduğumuz modeli xml dosyasına döndürme işlemine geçildi
                ConvertToZipFile convertZip = new ConvertToZipFile();
                var writer = new XmlTextWriter(stream, Encoding.UTF8);
                var xmlSerializer = new XmlSerializer(typeof(InvoiceType));

                xmlSerializer.Serialize(writer, invoiceType, XmlNamespaceHelper.InvoiceNamespaces);
                writer.Flush();
                stream.Seek((long)0, SeekOrigin.Begin);
                string decoded = Encoding.UTF8.GetString(stream.ToArray());


                //Oluşturmuş olduğumuz xml dosyasını ZipFileToByte metodu ile byte koduna dönüştürdük,
                var zipFile = convertZip.ZipFileToByte(stream, invoiceType.UUID.Value.ToString() + ".xml");

                //Streame dönüştürdüğümüz bytcodunu base64 stringe dönüştürmemiz gerekmektedir ve bu işlem burada yapılmaktadır,
                invoiceBase64Model = Convert.ToBase64String(zipFile);
                return (invoiceBase64Model, invoiceType);
            }
        }
    }
}