using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ePlatform.Integration.Models;
using ePlatform.Integration.Helpers;
using ePlatform.Core.Models;
using ePlatform.Integration.Models.Invoice21;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ePlatform.Integration.Models.Enums;

namespace ePlatform.Integration.Controllers
{
    /*
     İlgili servis uç noktasını her uç noktanın üzerinde bulunan url ile tetikleyebilirsiniz,ilk önce gettoken uç noktasından
     token almış olmak gerekmektedir.

    Create uç noktası ile üretmiş olduğunuz fatura id'sini /outboxInvoice/(id) şeklinde giderek yeni oluşturulmuş olan
    fatura örneğine ulaşılabilir,
    Oluşturulan fatura modeli postman örneklerinde bulunan 'OutboxInvoice_Model Complex' 
    modeli ile isteğe göre genişletilebilir. 
      */

    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        public static TokenResponseModel resToken;
        public HomeController()
        {
        }

        /*
        Diğer servisleri çağarabilmek için ilk önce gettoken uç noktasını çağırarak resToken değişkenine
        Güncel token bilgimizi yazdırıyoruz,Daha sonra diğer uç noktalar bu tokeni kullanarak bize
        cevap dönebileceklerdir.
         */

        // GET https://localhost:5001/home/gettoken
        [HttpGet]
        [Route("gettoken")]
        public async Task<ActionResult> GetToken()
        {
            var model = new TokenModel() { username = "serviceuser01@isim360.com", password = "ePlatform123+", client_id = "serviceApi" };
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://coretest.isim360.com/v1/token"))
            {

                var keyValues = new List<KeyValuePair<string, string>>();
                keyValues.Add(new KeyValuePair<string, string>("username", model.username));
                keyValues.Add(new KeyValuePair<string, string>("password", model.password));
                keyValues.Add(new KeyValuePair<string, string>("client_id", model.client_id));

                request.Content = new FormUrlEncodedContent(keyValues);

                var response = await client.SendAsync(request);
                if ((int)response.StatusCode == 422)//UnprocessableEntity
                {
                    var result = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(await response.Content.ReadAsStringAsync())?.FirstOrDefault();
                }
                var asString = await response.Content.ReadAsStringAsync();
                resToken = JsonConvert.DeserializeObject<TokenResponseModel>(asString);
                return Ok(resToken);
            }

        }

        /*
        id bilgisine göre gelen faturaları görüntülenmektedir,
         */
        // https://localhost:5001/home/inboxInvoice/f201ba2e-881f-4798-a715-d6090a28d7b2
        [HttpGet]
        [Route("inboxInvoice/{id}")]

        public async Task<ActionResult> GetInboxInvoice(Guid id)
        {
            if (resToken != null)
            {
                string token = resToken.access_token;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://efaturaservicetest.isim360.com/v1/inboxInvoice/{id}"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var asString = await response.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject<InboxInvoiceGetModel>(asString);
                    return Ok(x);
                }
            }
            else
            {
                return Ok("Token almak için /gettoken uç noktasını çağırın");
            }
        }

        /*
        id bilgisine göre giden faturaları görüntülenebilmektedir,
         */
        // https://localhost:5001/home/outboxInvoice/B980FFE7-A6F0-4072-AACE-119CCB40A483
        [HttpGet]
        [Route("outboxInvoice/{id}")]

        public async Task<ActionResult> GetOutboxInvoice(Guid id)
        {
            if (resToken != null)
            {
                string token = resToken.access_token;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://efaturaservicetest.isim360.com/v1/outboxInvoice/{id}"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var asString = await response.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject<OutboxInvoiceGetModel>(asString);
                    return Ok(x);
                }
            }
            else
            {
                return Ok("Token almak için /gettoken uç noktasını çağırın");
            }
        }
        [HttpGet]
        [Route("cancel/{id}")]

        public async Task<ActionResult> CancelOutboxInvoice(Guid id)
        {
            if (resToken != null)
            {
                List<Guid> model = new List<Guid>();
                model.Add(id);
                string parsedModel = JsonConvert.SerializeObject(model);
                string token = resToken.access_token;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Put, $"https://efaturaservicetest.isim360.com/v1/earchive/cancelinvoice"))
                {
                    if (!String.IsNullOrEmpty(parsedModel))
                    {
                        var content = new StringContent(parsedModel, Encoding.UTF8, "application/json");
                        request.Content = content;
                    }

                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var asString = await response.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject<OutboxInvoiceGetModel>(asString);
                    return Ok(x);
                }
            }
            else
            {
                return Ok("Token almak için /gettoken uç noktasını çağırın");
            }
        }
        /*
        Giden fatura listesi görüntülenebilmektedir,
         */
        // https://localhost:5001/home/list
        [HttpGet]
        [Route("list")]

        public async Task<ActionResult> Getlist(Guid id)
        {
            if (resToken != null)
            {
                var searchmodel = new InvoiceSearchModel()
                {
                    PageIndex = 1,
                    PageSize = 50,
                    IsDesc = true,
                    StartDate = "2018-10-01 00:00:00",
                    EndDate = "2018-11-01 00:00:00"
                };
                var sb = new StringBuilder();
                sb.AppendFormat("pageIndex={0}&", searchmodel.PageIndex);
                sb.AppendFormat("pageSize={0}&", searchmodel.PageSize);
                sb.AppendFormat("IsDesc={0}&", searchmodel.IsDesc);
                sb.AppendFormat("startDate={0}&", searchmodel.StartDate);
                sb.AppendFormat("endDate={0}&", searchmodel.EndDate);


                string token = resToken.access_token;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://efaturaservicetest.isim360.com/v1/outboxinvoice/list?{sb.ToString()}"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var asString = await response.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject<PagedList<OutboxInvoiceGetModel>>(asString);
                    return Ok(x);
                }
            }
            else
            {
                return Ok("Token almak için /gettoken uç noktasını çağırın");
            }
        }

        /*
        Yeni fatura oluşturmamızı sağlar,Oluşturulan fatura id'si ve fatura numara bilgisini dönmektedir,
         */
        //https://localhost:5001/home/create
        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            if (resToken != null)
            {

                var generalInfo = new GeneralInfoModel()
                {
                    Ettn = null,
                    Prefix = null,
                    InvoiceNumber = null,
                    InvoiceProfileType = (int)InvoiceProfilType.EARSIVFATURA,
                    IssueDate = DateTime.Now.ToString(),
                    Type = 1,
                    CurrencyCode = "TRY"
                };

                var addressBook = new AddressBookModel()
                {
                    IdentificationNumber = "1111111111",
                    ReceiverPersonSurName = "Test",
                    Name = "Test",
                    ReceiverCity = "İstanbul",
                    ReceiverDistrict = "Test",
                    ReceiverCountryId = 1
                };

                var invoiceLines = new List<InvoiceLineModel>();
                var invoiceLine = new InvoiceLineModel()
                {
                    InventoryCard = "Test",
                    Amount = 1,
                    DiscountAmount = 0,
                    LineAmount = 100,
                    UnitCode = "C62",
                    UnitPrice = 100,
                    VatRate = 10,
                    VatExemptionReasonCode = "201"
                };
                invoiceLines.Add(invoiceLine);

                OutboxInvoiceCreateModel model = new OutboxInvoiceCreateModel()
                {
                    InvoiceId = Guid.Empty.ToString(),
                    Status = (int)InvoiceStatus.Draft,
                    XsltCode = null,
                    UseManualInvoiceId = false,
                    GeneralInfoModel = generalInfo,
                    AddressBook = addressBook,
                    InvoiceLines = invoiceLines,
                    RecordType = 0
                };

                string token = resToken.access_token;
                string parsedModel = JsonConvert.SerializeObject(model);
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://efaturaservicetest.isim360.com/v1/outboxInvoice/create"))
                {
                    if (!String.IsNullOrEmpty(parsedModel))
                    {
                        var content = new StringContent(parsedModel, Encoding.UTF8, "application/json");
                        request.Content = content;
                    }
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var asString = await response.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject(asString);
                    return Ok(x);
                }
            }
            else
            {
                return Ok("Token almak için /gettoken uç noktasını çağırın");
            }
        }

        //https://localhost:5001/home/postzip

        [HttpGet]
        [Route("postzip")]
        public async Task<IActionResult> PostZip()
        {
            if (resToken != null)
            {

                // PostZip metodu ile ilk önce göndermek istediğiniz faturayı XML olarak oluşturmaktayız,
                //XML oluşturmak için ePlatform.Integration.Models.Invoice21 Modelinden yararlanılmaktadır,
                //Invoice21 modelini istemiş olduğunuz bilgiler ile oluşturduktan sonra,
                //İlgili Modeli ilk önce XML formatına dönüştürerek dönüştürülen XML dosyasınıda Ziplemek için
                //bytcode dönüştürmekteyiz daha sonra bu bytcodu Base64 formatıyla stringe dönültürerek outbox uç noktasına
                //Http.Post isteği ile faturayı zip olarak gönderebilirsiniz,

                //Model Doldurma aşaması
                //Müşteri Başladı
                var customer_partyIdentification = new List<PartyIdentificationType>();
                customer_partyIdentification.Add(new PartyIdentificationType { ID = new IDType { schemeID = "VKN", Value = "1234567801" } });
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
                var taxSubTotalList = new List<TaxSubtotalType>();
                taxSubTotalList.Add(new TaxSubtotalType
                {
                    TaxAmount = new TaxAmountType { Value = 2214, currencyID = "TRY" },
                    Percent = new PercentType1 { Value = 18 },
                    TaxCategory = new TaxCategoryType { TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "KDV Gerçek" }, TaxTypeCode = new TaxTypeCodeType { Value = "0015" } } }
                });

                var line = new List<InvoiceLineType>();
                line.Add(new InvoiceLineType
                {
                    ID = new IDType { Value = "1" },
                    LineExtensionAmount = new LineExtensionAmountType { currencyID = "TRY", Value = 12300 },
                    Item = new ItemType { Name = new NameType1 { Value = "NoteBook" }, SellersItemIdentification = new ItemIdentificationType { ID = new IDType { Value = "123456" } } },
                    Price = new PriceType { PriceAmount = new PriceAmountType { currencyID = "TRY", Value = 12300 } },
                    InvoicedQuantity = new InvoicedQuantityType { unitCode = "NIU", Value = 1 },
                    TaxTotal = new TaxTotalType { TaxAmount = new TaxAmountType { Value = 2214, currencyID = "TRY" }, TaxSubtotal = taxSubTotalList.ToArray() }
                });

                //AdditionalDocumentReference
                var additionalDocumentReference = new List<DocumentReferenceType>();

                //kalemler bitti   

                //string path = @"general.xslt";
                //string s = File.ReadAllText(path, Encoding.UTF8);
                //byte[] bytes = Encoding.UTF8.GetBytes(s);

                ////Xslt eklenmesi
                //additionalDocumentReference.Add(new DocumentReferenceType { ID = new IDType { Value = "CDA79E4E-EE13-4D9C-B625-87286EC30358" }, IssueDate = new IssueDateType { Value = Convert.ToDateTime("2015-01-01") }, DocumentType = new DocumentTypeType { Value = "Xslt" }, Attachment = new AttachmentType { EmbeddedDocumentBinaryObject = new EmbeddedDocumentBinaryObjectType { characterSetCode = "UTF-8", encodingCode = "Base64", filename = "WRK2015000000001.xslt", mimeCode = "application/xml", Value = bytes } } });

                var invoiceType = new InvoiceType
                {
                    UBLVersionID = new UBLVersionIDType { Value = "2.1" },
                    CustomizationID = new CustomizationIDType { Value = "TR1.2" },
                    ProfileID = new ProfileIDType { Value = "TEMELFATURA" },
                    InvoiceTypeCode = new InvoiceTypeCodeType { Value = "SATIS" },
                    CopyIndicator = new CopyIndicatorType { Value = false },
                    ID = new IDType { Value = "WRK2015000000010" },
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
                            Contact = new ContactType { ElectronicMail = new ElectronicMailType { Value = "eposta@hotmail.com" }, Note = new NoteType { Value = "deneme" }, Telefax = new TelefaxType { Value = "03225982030" }, Telephone = new TelephoneType { Value = "03225982030" } },
                            PostalAddress = new AddressType { Country = new CountryType { Name = new NameType1 { Value = "CountryName" } }, Room = new RoomType { Value = "room" }, StreetName = new StreetNameType { Value = "street" }, BuildingName = new BuildingNameType { Value = "buldingname" }, BuildingNumber = new BuildingNumberType { Value = "BuildingNumber" }, CitySubdivisionName = new CitySubdivisionNameType { Value = "CitySubdivisionName" }, CityName = new CityNameType { Value = "CityName" }, Region = new RegionType { Value = "region" } },
                        }
                    },
                    AccountingSupplierParty = new SupplierPartyType
                    {
                        Party = new PartyType
                        {
                            WebsiteURI = new WebsiteURIType { Value = "www.xxxx.com.tr" },
                            PartyIdentification = supplierParty_partyIdentification.ToArray(),
                            PartyName = new PartyNameType { Name = new NameType1 { Value = "Satıcı Firma Ltd.Şti" } },
                            PartyTaxScheme = new PartyTaxSchemeType { TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "deneme" }, TaxTypeCode = new TaxTypeCodeType { Value = "deneme" } } },
                            Contact = new ContactType { ElectronicMail = new ElectronicMailType { Value = "eposta@hotmail.com" }, Note = new NoteType { Value = "deneme" }, Telefax = new TelefaxType { Value = "03225982030" }, Telephone = new TelephoneType { Value = "03225982030" } },
                            PostalAddress = new AddressType { Country = new CountryType { Name = new NameType1 { Value = "CountryName" } }, Room = new RoomType { Value = "room" }, StreetName = new StreetNameType { Value = "street" }, BuildingName = new BuildingNameType { Value = "buldingname" }, BuildingNumber = new BuildingNumberType { Value = "BuildingNumber" }, CitySubdivisionName = new CitySubdivisionNameType { Value = "CitySubdivisionName" }, CityName = new CityNameType { Value = "CityName" }, Region = new RegionType { Value = "region" } },
                        }
                    },
                    LegalMonetaryTotal = new MonetaryTotalType { LineExtensionAmount = new LineExtensionAmountType { currencyID = "TRY", Value = 12300 }, TaxExclusiveAmount = new TaxExclusiveAmountType { currencyID = "TRY", Value = 12300 }, TaxInclusiveAmount = new TaxInclusiveAmountType { currencyID = "TRY", Value = 14514 }, AllowanceTotalAmount = new AllowanceTotalAmountType { currencyID = "TRY", Value = 0 }, PayableAmount = new PayableAmountType { currencyID = "TRY", Value = 14514 } },
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

                    //Oluşturmuş olduğumuz xml dosyasını ZipFileToByte metodu ile byte koduna dönüştürdük,
                    var zipFile = convertZip.ZipFileToByte(stream, invoiceType.UUID.Value.ToString() + ".xml");

                    //Streame dönüştürdüğümüz bytcodunu base64 stringe dönüştürmemiz gerekmektedir ve bu işlem burada yapılmaktadır,
                    invoiceBase64Model = Convert.ToBase64String(zipFile);
                }

                //Http.Post requestini oluşturmak için isteğin Bodysini oluşturuyoruz,
                OutboxInvoiceZipModel model = new OutboxInvoiceZipModel();

                model.InvoiceZip = invoiceBase64Model;
                model.Status = 20;
                model.CheckLocalReferenceId = false;
                model.Prefix = "";
                model.UseManualInvoiceId = false;
                model.TargetAlias = "urn:mail:defaulttest11pk@medyasoft.com.tr";
                model.AppType = 1;

                string token = resToken.access_token;
                string parsedModel = JsonConvert.SerializeObject(model);
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://efaturaservicetest.isim360.com/v1/outboxInvoice"))
                {
                    if (!String.IsNullOrEmpty(parsedModel))
                    {
                        var content = new StringContent(parsedModel, Encoding.UTF8, "application/json");
                        request.Content = content;
                    }
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var asString = await response.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject(asString);
                    return Ok(x);
                }
            }
            else
            {
                return Ok("Token almak için /gettoken uç noktasını çağırın");
            }
        }

        /*
        id bilgisine göre giden faturaların pdf'i indirilebilir,
         */
        // https://localhost:5001/home/outboxInvoice/pdf/3f407a48-4f2c-4b12-8a16-be43bc776042
        [HttpGet]
        [Route("pdf/{id}")]
        public async Task<ActionResult> GetPdf(Guid id)
        {
            if (resToken != null)
            {
                string token = resToken.access_token;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://efaturaservicetest.isim360.com/v1/outboxInvoice/pdf/{id}"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var stream = await response.Content.ReadAsStreamAsync();
                    using (var destinationStream = new FileStream("test.zip", FileMode.Create))
                    {
                        await stream.CopyToAsync(destinationStream);
                    }
                }
                return Ok();
            }
            else
            {
                return Ok("Token almak için /gettoken uç noktasını çağırın");
            }
        }
    }
}
