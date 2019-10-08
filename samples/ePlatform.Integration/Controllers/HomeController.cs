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

        //http://localhost:5004/v1/outboxinvoice/ubl/f201ba2e-881f-4798-a715-d6090a28d7b2
        //Aynı örneği inboxinvoice içinde kullanabilirsiniz,sadece url ve id değiştirilmelidir.
        [HttpGet]
        [Route("outboxInvoice/ubl/{id}")]

        public async Task<ActionResult> GetOutboxUbl(Guid id)
        {
            ConvertToZipFile convertZip = new ConvertToZipFile();

            if (resToken != null)
            {
                string token = resToken.access_token;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://efaturaservicetest.isim360.com/v1/outboxInvoice/ubl/{id}"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var response = await client.SendAsync(request);
                    var asString = await response.Content.ReadAsByteArrayAsync();
                    using (var stream = convertZip.UnzipFile(asString))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(InvoiceType));
                        var x = (InvoiceType)serializer.Deserialize(stream);

                    }
                }
                return Ok("Ubl başarıyla Deserialize edilmiştir.");
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
                    InvoiceNumber = "EPA2019131231477",//Manuel fatura İd tanımlamalarında yenilenmelidir
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
                    UseManualInvoiceId = true,//Fatura oluşturulurke belirlenen fatura numarasını atar false durumunda otomatik bir fatura belirler
                    GeneralInfoModel = generalInfo,
                    AddressBook = addressBook,
                    InvoiceLines = invoiceLines,
                    RecordType = 0//e-fatura:1,e-arşiv:0 gönderilmelidir.
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

        //https://localhost:5001/home/postubl


        [HttpGet]
        [Route("postubl")]
        public async Task<IActionResult> PostZip()
        {
            if (resToken != null)
            {
                //UBL oluşturmak için kullanığımız Metodumuzu Çağırıyoruz

                var ublHandler = new UblHandler();
                (string invoiceBase64Model, _) = ublHandler.CreateUbl();

                //Http.Post requestini oluşturmak için isteğin Bodysini oluşturuyoruz,

                OutboxInvoiceZipModel model = new OutboxInvoiceZipModel();
                model.InvoiceZip = invoiceBase64Model;
                model.Status = 0;
                model.CheckLocalReferenceId = false;
                model.Prefix = "";
                model.UseManualInvoiceId = true;
                // model.TargetAlias = "urn:mail:defaulttest11pk@medyasoft.com.tr"; //Birden fazla posta kutusu varsa TargetAlias'ta belirtilen posta kutusu kullanılır eğer gönderilmezse userfirstAlias true yapılmalı
                model.AppType = 2;
                model.UseFirstAlias = true;//birden fazla posta Kutusu varsa true yollandığında ilk posta kutusuna otomatik fatura kesilerek taslak olarak kaydedilir
                model.EArsivInfo = new EArsivInfoModel() { SendEMail = false };

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
                return Ok("Token almak için /gettoken uç noktasını çağırın");
        }
        [HttpGet]
        [Route("templateubl")]
        public async Task<IActionResult> TemplateUbl()
        {
            var ublHandler = new UblHandler();
            string invoiceBase64Model = default;
            var currentDirectory = Directory.GetCurrentDirectory();

            (string tmpStr, InvoiceType invoiceType) = ublHandler.CreateUbl();
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
                //Current Directory'de oluşturulan xml dosya örneği Template içerisine yazılıyor,
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter($"{currentDirectory}" + @"\Template\outXml_" + invoiceType.UUID.Value.ToString() + ".xml", true))
                {
                    file.WriteLine(decoded);
                }

                //Oluşturmuş olduğumuz xml dosyasını ZipFileToByte metodu ile byte koduna dönüştürdük,
                var zipFile = convertZip.ZipFileToByte(stream, invoiceType.UUID.Value.ToString() + ".xml");

                //Streame dönüştürdüğümüz bytcodunu base64 stringe dönüştürmemiz gerekmektedir ve bu işlem burada yapılmaktadır,
                invoiceBase64Model = Convert.ToBase64String(zipFile);
            }
            //oluşturulan base64 model çıktısını Template klasörü içer,isine yazıyoruz postman içerisinde  InvoiceZip içerisinde gönderilebilir.
            using (System.IO.StreamWriter file =
               new System.IO.StreamWriter($"{currentDirectory}" + @"\Template\invoiceBase64Model_" + invoiceType.UUID.Value.ToString() + ".txt", true))
            {
                file.WriteLine(invoiceBase64Model);
            }
            return Ok("Template dosyaları OutFiles klasörüne ilgili fatura numarası ile oluşturuldu");
        }
    }
}
