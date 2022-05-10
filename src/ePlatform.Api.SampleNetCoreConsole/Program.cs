using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ePlatform.Api.SampleNetCoreConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddePlatformClients(clientOptions =>
                {
                    clientOptions.InvoiceServiceUrl = "https://efaturaservicetest.isim360.com";
                    clientOptions.TicketServiceUrl = "https://ebiletservicetest.isim360.com";
                    clientOptions.ApiKey = "";
                })
                .BuildServiceProvider();


            //var authClient = serviceProvider.GetService<IAuthClient>();
            var outboxInvoiceClient = serviceProvider.GetService<IOutboxInvoiceClient>();
            var inboxInvoiceClient = serviceProvider.GetService<IInboxInvoiceClient>();
            var commonClient = serviceProvider.GetService<ICommonClient>();
            var earchiveClient = serviceProvider.GetService<IEArchiveInvoiceClient>();
            var eventTicketClient = serviceProvider.GetService<IEventTicketClient>();
            var passengerTicketClient = serviceProvider.GetService<IPassengerTicketClient>();

            int menuCode = -1;
            int subMenuCode = -1;
            do
            {
                System.Console.WriteLine("ePlatform eBelge Invoice Sample Menu");
                System.Console.WriteLine("1 - Common Client fonksiyonlarını kontrol etmek için");
                System.Console.WriteLine("2 - E Arşiv Client fonksiyonlarını kontrol etmek için");
                System.Console.WriteLine("3 - Giden Fatura fonksiyonlarını kontrol etmek için");
                System.Console.WriteLine("4 - Gelen Fatura fonksiyonlarını kontrol etmek için\n");
                System.Console.WriteLine("10 - e-Bilet Fonksiyonları");
                System.Console.WriteLine("9 - Test Fonksiyonu");
                System.Console.WriteLine("0 - Çıkış");

                System.Console.Write("Lütfen kodu girin:");
                menuCode = System.Convert.ToInt32(System.Console.ReadLine());
                System.Console.WriteLine("Menucode : " + menuCode);
                System.Console.Clear();

                if (menuCode == 1)//Common Clients
                {
                    #region CommonClientFunctions

                    subMenuCode = -1;
                    System.Console.WriteLine("Common Client fonksiyonları menüsü");
                    System.Console.WriteLine("1 - /v1/staticlist/currency");
                    System.Console.WriteLine("2 - /v1/staticlist/unit");
                    System.Console.WriteLine("3 - /v1/staticlist/taxexemptionreason");
                    System.Console.WriteLine("4 - /v1/staticlist/withholding");
                    System.Console.WriteLine("5 - /v1/staticlist/taxtypecode");
                    System.Console.WriteLine("6 - /v1/staticlist/taxoffice");
                    System.Console.WriteLine("7 - /v1/staticlist/country\n");
                    System.Console.Write("Lütfen test etmek istediğiniz fonksiyonun kodunu girin:");
                    subMenuCode = System.Convert.ToInt32(System.Console.ReadLine());


                    switch (subMenuCode)
                    {

                        case 1:
                            var currencies = await commonClient.CurrencyCodeList();
                            currencies.ForEach(c => System.Console.WriteLine($"{c.Code} {c.Name}"));
                            break;

                        case 2:

                            System.Console.WriteLine("Testing endpoint: /v1/staticlist/unit");
                            var UnitCodeListTest = await commonClient.UnitCodeList();
                            UnitCodeListTest.ForEach(c => System.Console.WriteLine("\n" + c.Code + "\n" +
                                c.CreatedDate + "\n" +
                                c.Description + "\n" +
                                c.Format + "\n" +
                                c.Id + "\n" +
                                c.IsEnabled + "\n" +
                                c.IsSelectedUnitCode + "\n" +
                                c.UnitPrice + "\n" +
                                c.UpdatedDate + "\n\n" +
                                "***************"));
                            break;

                        case 3:
                            System.Console.WriteLine("Testing endpoint: /v1/staticlist/taxexemptionreason");
                            var TaxExemptionReasonListTest = await commonClient.TaxExemptionReasonList();
                            TaxExemptionReasonListTest.ForEach(x => System.Console.WriteLine("\n" + x.Description + "\n" +
                                x.Id + "\n" +
                                x.IsEnabled + "\n" +
                                x.RelatedType + "\n" +
                                x.Value + "\n\n" +
                                "******************"
                                ));
                            break;

                        case 4:
                            System.Console.WriteLine("Testing endpoint: /v1/staticlist/withholding");
                            var WithHoldingListTest = await commonClient.WithHoldingList();
                            WithHoldingListTest.ForEach(x => System.Console.WriteLine("\n" +
                                "CreatedBy : " + x.CreatedBy + "\n" +
                                "CreatedDate : " + x.CreatedDate + "\n" +
                                "Description : " + x.Description + "\n" +
                                "id : " + x.Id + "\n" +
                                "isEnabled : " + x.IsEnabled + "\n" +
                                "Rate : " + x.Rate + "\n" +
                                "UpdatedBy : " + x.UpdatedBy + "\n" +
                                "UpdatedDate : " + x.UpdatedDate + "\n" +
                                "Value : " + x.Value + "\n" +
                                "Value2 : " + x.Value2 + "\n\n" +
                                "******************"
                                ));
                            break;

                        case 5:
                            System.Console.WriteLine("Testing endpoint: /v1/staticlist/taxtypecode");
                            var TaxTypeCodeListTest = await commonClient.TaxTypeCodeList();
                            TaxTypeCodeListTest.ForEach(x => System.Console.WriteLine("\n" +
                                "Code : " + x.Code + "\n" +
                                "Description : " + x.Description + "\n" +
                                "IsCalculationBaseOnLineAmount : " + x.IsCalculationBaseOnLineAmount + "\n" +
                                "Enabled : " + x.IsEnabled + "\n" +
                                "isNegative : " + x.IsNegative + "\n" +
                                "isProtata : " + x.IsProrata + "\n" +
                                "Name : " + x.Name + "\n\n" +
                                "*********************"
                                ));
                            break;

                        case 6:
                            System.Console.WriteLine("Testing endpoint: /v1/staticlist/taxoffice");
                            var TaxOfficeListTest = await commonClient.TaxOfficeList();
                            TaxOfficeListTest.ForEach(x => System.Console.WriteLine("\n" +
                                "CityId : " + x.CityId + "\n" +
                                "CityName : " + x.CityName + "\n" +
                                "Code : " + x.Code + "\n" +
                                "Name : " + x.Name + "\n\n" +
                                "*******************"
                                ));
                            break;

                        case 7:
                            System.Console.WriteLine("Testing endpoint: /v1/staticlist/country");
                            var CountrListTest = await commonClient.CountrList();
                            CountrListTest.ForEach(x => System.Console.WriteLine("\n" +
                                "Id: " + x.Id + "\n" +
                                "Name : " + x.Name + "\n\n" +
                                "***************"));
                            break;

                        default:
                            menuCode = -1;
                            subMenuCode = -1;
                            break;
                    }
                    #endregion
                }
                else if (menuCode == 2)
                {
                    #region EArchiveInvoiceClientFunctions

                    subMenuCode = -1;
                    System.Console.WriteLine("Common Client fonksiyonları menüsü");
                    System.Console.WriteLine("1 - POST /v2/earchive/create");
                    System.Console.WriteLine("2 - (-) PUT /v2/earchive/update/{id} - (Sadece hatalı ve taslak durumundaki faturalar güncellenebilir.)");
                    System.Console.WriteLine("3 - POST ubl /v2/earchive");
                    System.Console.WriteLine("4 - (-) PUT ubl /v2/earchive/{id} - (Sadece hatalı ve taslak durumundaki faturalar güncellenebilir.)");
                    System.Console.WriteLine("5 - GET status /v2/earchive/{id}/status");
                    System.Console.WriteLine("6 - GET html /v2/earchive/{id}/html/{isStandartXslt} - (Stream Başlatır)");
                    System.Console.WriteLine("7 - GET pdf /v2/earchive/{id}/pdf/{isStandartXslt} - (Stream Başlatır)");
                    System.Console.WriteLine("8 - GET ubl /v2/earchive/{id}/ubl - (Stream Başlatır)");
                    System.Console.WriteLine("9 - GET arayüzden kesilen e arşiv faturalar v2/earchive/withnulllocalreferences");
                    System.Console.WriteLine("10 - GET email detail /v1/earchive/getmaildetail");
                    System.Console.WriteLine("11 - GET retryInvoiceMailForError /v1/earchive/retryinvoicemail/{id} - (Fonksiyon değer döndürmüyor)");
                    System.Console.WriteLine("12 - (-) POST RetryInvoiceMail /v1/earchive/retryinvoicemail - (Fonksiyonun durumu kontrol edilecek.)");
                    System.Console.WriteLine("13 - PUT CancelInvoice /v1/earchive/cancelinvoice - (60 statuslu ettn gerekli)\n");
                    System.Console.Write("Lütfen test etmek istediğiniz fonksiyonun kodunu girin:");
                    subMenuCode = System.Convert.ToInt32(System.Console.ReadLine());
                    switch (subMenuCode)
                    {
                        case 1://post /v2/earchive/create
                            System.Console.WriteLine("Endpoint : /v2/earchive/create");
                            UblBuilderModel testUblModel = eFaturaHelper.fillEArchiveUblModel();
                            var EArchivePostModelResult = await earchiveClient.Create(testUblModel);
                            System.Console.WriteLine("\n" +
                                "ID : " + EArchivePostModelResult.Id + "\n" +
                                EArchivePostModelResult.InvoiceNumber + "\n\n" +
                                "**********************");
                            break;

                        case 2://put /v2/earchive/update/{id}
                            System.Console.WriteLine("Endpoint : /v2/earchive/update/{id}");
                            var EArchivePutTestId = System.Guid.Parse("eda4d4a8-c621-45cf-aa4d-a796c5e768e6");//Test edilecek id girilmeli
                            UblBuilderModel EArchivePutUblModelTest = eFaturaHelper.fillEArchiveUblModel(EArchivePutTestId);
                            var EArchivePutModelResult = await earchiveClient.Update(EArchivePutTestId, EArchivePutUblModelTest);
                            System.Console.WriteLine("\n" +
                                "ID : " + EArchivePutModelResult.Id + "\n" +
                                "InvoiceNumber : " + EArchivePutModelResult.InvoiceNumber + "\n\n" +
                                "**********************");
                            break;

                        case 3://post ubl /v2/earchive
                            System.Console.WriteLine("Endpoint : /v2/earchive");
                            string EAPostFile = System.Environment.CurrentDirectory + "\\Static\\eArsivFatura.xml";//Dosyayı bin/debug/netcoreapp3.0/Static içerisinde bulabilirsiniz.
                            CreateEarchiveInvoiceModel EAInvoicePostUblTest = eFaturaHelper.fillEArchiveModelWithFile(EAPostFile);
                            var EAInvoicePostUblResponse = await earchiveClient.PostUbl(EAInvoicePostUblTest);
                            System.Console.WriteLine("\n" +
                                "ID : " + EAInvoicePostUblResponse.Id + "\n" +
                                "InvoiceNumber : " + EAInvoicePostUblResponse.InvoiceNumber + "\n\n" +
                                "**********************");
                            break;

                        case 4://put ubl /v2/earchive/{id} ==> Sadece hatalı ve taslak durumundaki fatura için güncelleme yapabilirsiniz hatası alındı.
                            System.Console.WriteLine("Endpoint : /v2/earchive/{id}");
                            var EAInvoicePutUblId = System.Guid.Parse("F9804F3C-B1DB-4E1A-3F17-08DA192769E2");
                            string EAPutFile = System.Environment.CurrentDirectory + "\\Static\\eArsivFatura.xml";//Dosyayı bin/debug/netcoreapp3.0/Static içerisinde bulabilirsiniz.
                            CreateEarchiveInvoiceModel EAInvoicePutUblTest = eFaturaHelper.fillEArchiveModelWithFile(EAPutFile);
                            var EAInvoicePutUblResponse = await earchiveClient.PutUbl(EAInvoicePutUblId, EAInvoicePutUblTest);
                            System.Console.WriteLine("\n" +
                                "ID : " + EAInvoicePutUblResponse.Id + "\n\n" +
                                "**********************");
                            break;

                        case 5://get status /v2/earchive/{id}/status
                            System.Console.WriteLine("Endpoint: /v2/earchive/{id}/status");
                            var EArchiveGetStatusId = System.Guid.Parse("1244eab8-ddad-4551-bacc-051abd49e1a3");
                            var EarchiveGetStatusTest = await earchiveClient.GetStatus(EArchiveGetStatusId);
                            System.Console.WriteLine("\n" +
                                "Id : " + EarchiveGetStatusTest.Id + "\n" +
                                "InvoiceNumber : " + EarchiveGetStatusTest.InvoiceNumber + "\n" +
                                "Message : " + EarchiveGetStatusTest.Message + "\n" +
                                "Status : " + EarchiveGetStatusTest.Status + "\n\n" +
                                "**********************");
                            break;

                        case 6://get html /v2/earchive/{id}/html/{isStandartXslt}
                            Stream EAGetHtmlTest = await earchiveClient.GetHtml(System.Guid.Parse("bf579d26-b73e-4dd3-bd1b-1226ffc28d9d"), true);
                            System.Console.WriteLine(EAGetHtmlTest);
                            break;

                        case 7://get pdf /v2/earchive/{id}/pdf/{isStandartXslt}
                            Stream EAGetPdfTest = await earchiveClient.GetPdf(System.Guid.Parse("B1120903-EE7E-4432-8ACF-038207F17CD0"), true);
                            System.Console.WriteLine(EAGetPdfTest);
                            break;

                        case 8://get ubl /v2/earchive/{id}/ubl
                            Stream EAGetUblTest = await earchiveClient.GetUbl(System.Guid.Parse("5FD1469C-2B42-449D-A09E-1551E59BBBF9"));
                            System.Console.WriteLine(EAGetUblTest);
                            break;

                        case 9://get arayüzden kesilen e arşiv faturalar /v2/earchive/withnulllocalreferences
                            System.Console.WriteLine("Endpoint: /v2/earchive/withnulllocalreferences");
                            UIInvoices EAarayuzdenKesilenlerTest = new UIInvoices();
                            EAarayuzdenKesilenlerTest.StartDate = new System.DateTime(2022, 03, 07, 14, 50, 51);
                            EAarayuzdenKesilenlerTest.EndDate = new System.DateTime(2022, 04, 07, 14, 50, 51);
                            var ArayuzdenKesilenlerResponse = await earchiveClient.GetUIInvoices(EAarayuzdenKesilenlerTest);
                            foreach (var item in ArayuzdenKesilenlerResponse)
                            {
                                System.Console.WriteLine("\n" +
                                "Id : " + item.Id + "\n" +
                                "Status : " + item.Status + "\n\n" +
                                "**********************");
                            }
                            break;

                        case 10://get email detail /v1/earchive/getmaildetail
                            System.Console.WriteLine("Endpoint: /v1/earchive/getmaildetail");
                            var EarchiveGetEmailDetailId = "1244eab8-ddad-4551-bacc-051abd49e1a3";
                            var EarchiveGetEmailDetailTest = await earchiveClient.GetMailDetail(EarchiveGetEmailDetailId);
                            foreach (var item in EarchiveGetEmailDetailTest)
                            {
                                System.Console.WriteLine("\n" +
                                    "Id: " + item.Id + "\n" +
                                    "InvoiceId : " + item.InvoiceId + "\n" +
                                    "EmailAdress : " + item.EmailAddress + "\n" +
                                    "EmailStatus : " + item.EmailStatus + "\n" +
                                    "LastTryDate : " + item.LastTryDate + "\n" +
                                    "CreatedDate : " + item.CreatedDate + "\n" +
                                "**********************");
                            }
                            break;

                        case 11://get retryInvoiceMailForError /v1/earchive/retryinvoicemail/{id} ==> Test edilemedi. Uygun bir mail adersi ile test edilmeli.
                            System.Console.WriteLine("Endpoint: /v1/earchive/retryinvoicemail/{id}");
                            var EArchiveRetryInvoiceMailForErrorTestId = System.Guid.Parse("daa8f23b-7f01-40f0-a6f3-09ea265590a3");
                            var EARetryInvoiceMailRes = await earchiveClient.RetryInvoiceMail(EArchiveRetryInvoiceMailForErrorTestId);
                            System.Console.WriteLine("\n" + EARetryInvoiceMailRes +
                                "**********************");
                            break;

                        case 12://post RetryInvoiceMail /v1/earchive/retryinvoicemail
                            System.Console.WriteLine("Endpoint: /v1/earchive/retryinvoicemail\n\nFonksiyonun durumu kontrol edilecek.");
                            //RetryMailModel retryModel = new RetryMailModel();
                            //retryModel.Id = System.Guid.Parse("9BC954F4-94E3-42D9-8DC0-392E0A2577EA");
                            //retryModel.EmailAddresses = "deneme1@deneme.com;deneme2@deneme.com";
                            //var EarchiveRetryInvoiceMailRes = await earchiveClient.RetryInvoiceWithDifferentMails(retryModel);
                            //System.Console.WriteLine("\n" + EarchiveRetryInvoiceMailRes + "\n" +
                            //    "**********************");
                            break;

                        case 13://put CancelInvoice /v1/earchive/cancelinvoice ==> Test edebilmek için statusu 60 olan faturaların ettn numaraları listeye girilmeli.
                            System.Console.WriteLine("Endpoint: /v1/earchive/cancelinvoice");
                            var EArchiveCancelUnvoiceId1 = System.Guid.Parse("78215532-990c-4ce6-a73c-cd6a272ef176");
                            var EArchiveCancelUnvoiceId2 = System.Guid.Parse("364c9c5a-9eec-4216-a58d-e620b6d509f0");
                            System.Guid[] gCancelInvoiceArray = new System.Guid[2];
                            gCancelInvoiceArray[0] = EArchiveCancelUnvoiceId1;
                            gCancelInvoiceArray[1] = EArchiveCancelUnvoiceId2;
                            var EarchiveCancelInvoiceTest = await earchiveClient.Cancel(gCancelInvoiceArray);
                            System.Console.WriteLine("\n" + EarchiveCancelInvoiceTest + "\n" +
                                "**********************");
                            break;

                        default:
                            menuCode = -1;
                            subMenuCode = -1;
                            break;
                    }
                    #endregion
                }
                else if (menuCode == 3)
                {
                    #region OutboxInvoiceClientFunctions
                    subMenuCode = -1;
                    System.Console.WriteLine("Outbox Invoice Client fonksiyonları menüsü");
                    System.Console.WriteLine("1 - POST /v1/outboxinvoice/create");
                    System.Console.WriteLine("2 - PUT /v1/outboxinvoice/update/{id}");
                    System.Console.WriteLine("3 - PUT updateStatus /v1/outboxinvoice/updatestatuslist - (Uygun ETTN numaraları ile test edilebilir.)");
                    System.Console.WriteLine("4 - POST ubl /v2/outboxinvoice - (Test datası doldurularak test edilebilir.)");
                    System.Console.WriteLine("5 - PUT ubl /v2/outboxinvoice/{id} - (Test datası doldurularak test edilebilir.)");
                    System.Console.WriteLine("6 - GET Status /v2/outboxinvoice/{id}/status");
                    System.Console.WriteLine("7 - GET GetInvoiceReason /v1/outboxinvoice/invoicereason/{id} - (Geçerli bir ETTN Numarası ile kontrol edilmeli)");
                    System.Console.WriteLine("8 - GET Arayüzden kesilen faturalar /v2/outboxinvoice/withnulllocalreferences");
                    System.Console.WriteLine("9 - GET Html /v2/outboxinvoice/{id}/html/{isStandartXslt} - (Stream başlatır)");
                    System.Console.WriteLine("10 - GET Pdf /v2/outboxinvoice/{id}/pdf/{isStandartXslt} - (Stream başlatır)");
                    System.Console.WriteLine("11 - GET Ubl /v2/outboxinvoice/{id}/ubl - (Stream başlatır)\n");
                    System.Console.Write("Lütfen test etmek istediğiniz fonksiyonun kodunu girin:");
                    subMenuCode = System.Convert.ToInt32(System.Console.ReadLine());
                    switch (subMenuCode)
                    {
                        case 1://post /v1/outboxinvoice/create
                            System.Console.WriteLine("Endpoint : /v1/outboxinvoice/create");
                            UblBuilderModel OutboxInvoicePostModel = eFaturaHelper.fillUblModel();// Id boş gönderilirse ettn numarası null olarak sunucuya iletilir.
                            var OutboxInvoicePostResultTest = await outboxInvoiceClient.Post(OutboxInvoicePostModel);
                            System.Console.WriteLine("\n" +
                                "ID : " + OutboxInvoicePostResultTest.Id + "\n" +
                                "InvoiceNumber : " + OutboxInvoicePostResultTest.InvoiceNumber + "\n\n" +
                                "**********************");
                            break;

                        case 2://put /v1/outboxinvoice/update/{id}
                            System.Console.WriteLine("Endpoint : /v1/outboxinvoice/update/{id}");
                            var OutboxInvoiceUpdatedIdTest = System.Guid.Parse("FF18EE43-5BD0-4977-B94A-08DA1DE9C812");
                            UblBuilderModel OutboxInvoicePutModel = eFaturaHelper.fillUblModel(OutboxInvoiceUpdatedIdTest);//Id dolu gönderilirse ettn numarası alanına bu id yerleştirilir.
                            //Güncellenecek id bu alana girilmelidir.
                            var OutboxInvoicePutResultTest = await outboxInvoiceClient.Update(OutboxInvoiceUpdatedIdTest, OutboxInvoicePutModel);
                            System.Console.WriteLine("\n" +
                                "ID : " + OutboxInvoicePutResultTest.Id + "\n" +
                                "InvoiceNumter : " + OutboxInvoicePutResultTest.InvoiceNumber + "\n\n" +
                                "**********************");
                            break;

                        case 3://put updateStatus /v1/outboxinvoice/updatestatuslist
                            System.Console.WriteLine("Endpoint : /v1/outboxinvoice/updatestatuslist");
                            UpdateInvoiceModel OutboxInvoiceUpdateStatusListTest = new UpdateInvoiceModel();
                            System.Guid[] OIUpdateStatusIds = new System.Guid[1];
                            OIUpdateStatusIds[0] = System.Guid.Parse("E60B76BD-DE6C-41DF-B788-08DA1DEB784D");
                            //OIUpdateStatusIds[1] = System.Guid.Parse("6F223EB2-F1A9-4C1D-BC3A-CF7F17078FED");
                            OutboxInvoiceUpdateStatusListTest.Ids = OIUpdateStatusIds;
                            OutboxInvoiceUpdateStatusListTest.Status = InvoiceStatus.Draft;//0 = taslak, 20 : Kaydet ve gönder
                            var OIUpdateStatusListResult = await outboxInvoiceClient.UpdateStatusList(OutboxInvoiceUpdateStatusListTest);
                            System.Console.WriteLine("\n" +
                                OIUpdateStatusListResult + "\n\n" +
                                "**********************");
                            break;

                        case 4://post ubl /v2/outboxinvoice
                               //                            System.Console.WriteLine("Endpoint : /v1/outboxinvoice/update/{id}");
                            string OIPostFilePath = System.Environment.CurrentDirectory + "\\Static\\gidenEFatura.xml";//Dosyayı bin/debug/netcoreapp3.0/Static içerisinde bulabilirsiniz.
                            CreateInvoiceModelV2 OIPostUblTest = eFaturaHelper.fillUblModelWithFile(OIPostFilePath);//Parametre olarak dosya yolu bekler
                            //Model doldurulmalı

                            var OIPostUblResponse = await outboxInvoiceClient.PostUbl(OIPostUblTest);
                            System.Console.WriteLine("\n" +
                                "ID : " + OIPostUblResponse.Id + "\n" +
                                "InvoiceNumber : " + OIPostUblResponse.InvoiceNumber + "\n\n" +
                                "**********************");
                            break;
                        case 5://put ubl /v2/outboxinvoice/{id}
                            System.Console.WriteLine("Endpoint : /v2/outboxinvoice/{id}");
                            var OIPutId = System.Guid.Parse("96BC8C4A-3B17-4B5C-222B-08DA1DEC4AF1");
                            string OIPutFilePath = System.Environment.CurrentDirectory + "\\Static\\gidenEFatura.xml";//Dosyayı bin/debug/netcoreapp3.0/Static içerisinde bulabilirsiniz.
                            CreateInvoiceModelV2 OIPutUblTest = eFaturaHelper.fillUblModelWithFile(OIPutFilePath);//Parametre olarak dosya yolu bekler
                            //Model doldurulmalı

                            var OIPutUblResponse = await outboxInvoiceClient.PutUbl(OIPutId, OIPutUblTest);
                            System.Console.WriteLine("\n" +
                                "ID : " + OIPutUblResponse.Id + "\n" +
                                "InvoiceNumber : " + OIPutUblResponse.InvoiceNumber + "\n\n" +
                                "**********************");
                            break;

                        case 6://get Status /v2/outboxinvoice/{id}/status
                            System.Console.WriteLine("Endpoint : /v2/outboxinvoice/{id}/status");
                            var OutboxInvoiceGetStatusTest = await outboxInvoiceClient.GetStatus(System.Guid.Parse("0a82238e-3679-4807-bbd9-08da193b9cfb"));
                            System.Console.WriteLine("\n" +
                                "EnvelopeId : " + OutboxInvoiceGetStatusTest.EnvelopeId + "\n" +
                                "EnvelopeMessage : " + OutboxInvoiceGetStatusTest.EnvelopeMessage + "\n" +
                                "EnvelopeStatus : " + OutboxInvoiceGetStatusTest.EnvelopeStatus + "\n" +
                                "Id : " + OutboxInvoiceGetStatusTest.Id + "\n" +
                                "InvoiceNumber : " + OutboxInvoiceGetStatusTest.InvoiceNumber + "\n" +
                                "Message : " + OutboxInvoiceGetStatusTest.Message + "\n" +
                                "Status : " + OutboxInvoiceGetStatusTest.Status + "\n\n" +
                                "**********************");
                            break;

                        case 7://GetInvoiceReason /v1/outboxinvoice/invoicereason/{id} ==> Uygun bir ettn numarası ile tekrar kontrol edilmeli
                            System.Console.WriteLine("Endpoint : /v1/outboxinvoice/invoicereason/{id}");
                            var GetInvoiceReasonTest = await outboxInvoiceClient.GetInvoiceReason(System.Guid.Parse("E13A6B76-733D-4DBA-E714-08DA1D23DCB6"));
                            System.Console.WriteLine("Response : " + GetInvoiceReasonTest + "\n\n");
                            break;

                        case 8://Get Arayüzden kesilen faturalar /v2/outboxinvoice/withnulllocalreferences
                            System.Console.WriteLine("Endpoint : /v2/outboxinvoice/withnulllocalreferences");
                            UIInvoices arayuzdenKesilenlerTest = new UIInvoices();
                            arayuzdenKesilenlerTest.StartDate = new System.DateTime(2022, 03, 07, 14, 50, 51);
                            arayuzdenKesilenlerTest.EndDate = new System.DateTime(2022, 04, 07, 14, 50, 51);
                            var ArayuzdenKesilenlerResponse = await outboxInvoiceClient.GetUIInvoices(arayuzdenKesilenlerTest);
                            foreach (var item in ArayuzdenKesilenlerResponse)
                            {
                                System.Console.WriteLine("\n" +
                                "Id : " + item.Id + "\n" +
                                "Status : " + item.Status + "\n\n" +
                                "**********************");
                            }
                            break;
                        case 9://Get Html /v2/outboxinvoice/{id}/html/{isStandartXslt}
                            Stream OIGetHtmlTest = await outboxInvoiceClient.GetHtml(System.Guid.Parse("bf579d26-b73e-4dd3-bd1b-1226ffc28d9d"), true);
                            System.Console.WriteLine(OIGetHtmlTest);
                            break;

                        case 10://Get Pdf /v2/outboxinvoice/{id}/pdf/{isStandartXslt}
                            Stream OIGetPdfTest = await outboxInvoiceClient.GetPdf(System.Guid.Parse("B1120903-EE7E-4432-8ACF-038207F17CD0"), true);
                            System.Console.WriteLine(OIGetPdfTest);
                            break;

                        case 11://Get Ubl /v2/outboxinvoice/{id}/ubl
                            Stream OIGetUblTest = await outboxInvoiceClient.GetUbl(System.Guid.Parse("5FD1469C-2B42-449D-A09E-1551E59BBBF9"));
                            System.Console.WriteLine(OIGetUblTest);
                            break;


                        default:
                            menuCode = -1;
                            subMenuCode = -1;
                            break;

                    }

                    #endregion
                }
                else if (menuCode == 4)
                {
                    #region InboxInvoiceClientFunctions
                    subMenuCode = -1;
                    System.Console.WriteLine("Outbox Invoice Client fonksiyonları menüsü");
                    System.Console.WriteLine("1 - GET list /v1/inboxinvoice/list");
                    System.Console.WriteLine("2 - GET status /v2/inboxinvoice/{id}/status");
                    System.Console.WriteLine("3 - GET html /v2/inboxinvoice/{id}/html/{isStandartXslt} - (Stream başlatır)");
                    System.Console.WriteLine("4 - GET pdf /v2/inboxinvoice/{id}/pdf/{isStandartXslt} - (Stream başlatır)");
                    System.Console.WriteLine("5 - GET get ubl /v2/inboxinvoice/{id}/ubl - (Stream başlatır)");
                    System.Console.WriteLine("6 - PUT IsNewFlag /v1/inboxinvoice/updateisnew\n");
                    System.Console.Write("Lütfen test etmek istediğiniz fonksiyonun kodunu girin:");
                    subMenuCode = System.Convert.ToInt32(System.Console.ReadLine());
                    switch (subMenuCode)
                    {
                        case 1://get list 
                            System.Console.WriteLine("testing endpoint: /v1/inboxinvoice/list");
                            var pagingModelInboxInvoiceGetList = new PagingModel();
                            pagingModelInboxInvoiceGetList.PageSize = 100;
                            pagingModelInboxInvoiceGetList.PageIndex = 1;
                            pagingModelInboxInvoiceGetList.IsDesc = true;
                            var InboxGetListTest = await inboxInvoiceClient.Get(pagingModelInboxInvoiceGetList);
                            foreach (var item in InboxGetListTest.Items)
                            {
                                System.Console.WriteLine("\n" +
                                "Id : " + item.Id + "\n" +
                                "CreatedDate : " + item.CreatedDate + "\n" +
                                "Currency : " + item.Currency + "\n" +
                                "CustomerId : " + item.CustomerId + "\n" +
                                "DigestValue : " + item.DigestValue + "\n" +
                                "Envelope : " + item.Envelope + "\n" +
                                "EnvelopeId : " + item.EnvelopeId + "\n" +
                                "ExecutionDate : " + item.ExecutionDate + "\n" +
                                "InvoiceNumber : " + item.InvoiceNumber + "\n" +
                                "IsAgentNew : " + item.IsAgentNew + "\n" +
                                "IsArchived : " + item.IsArchived + "\n" +
                                "IsNew : " + item.IsNew + "\n" +
                                "IsRead : " + item.IsRead + "\n" +
                                "IsVerified : " + item.IsVerified + "\n" +
                                "PayableAmount : " + item.PayableAmount + "\n" +
                                "SentDate : " + item.SentDate + "\n" +
                                "Status : " + item.Status + "\n" +
                                "TargetAlias : " + item.TargetAlias + "\n" +
                                "TargetTitle : " + item.TargetTitle + "\n" +
                                "TargetVknTckn : " + item.TargetVknTckn + "\n" +
                                "TipType : " + item.TipType + "\n" +
                                "TotalAmount : " + item.TotalAmount + "\n" +
                                "Type : " + item.Type + "\n\n" +
                                "**********************");
                            }
                            break;

                        case 2://get status /v2/inboxinvoice/{id}/status
                            System.Console.WriteLine("testing endpoint: /v2/inboxinvoice/{id}/status");
                            var InboxInvoiceGetStatusId = System.Guid.Parse("9758339E-EB0B-454C-8C2F-18059F944A3F");
                            var InboxGetStatusTest = await inboxInvoiceClient.GetStatus(InboxInvoiceGetStatusId);
                            System.Console.WriteLine("\n" +
                                "Id : " + InboxGetStatusTest.Id + "\n" +
                                "InvoiceNumber : " + InboxGetStatusTest.InvoiceNumber + "\n" +
                                "Status : " + InboxGetStatusTest.Status + "\n" +
                                "**********************");
                            break;

                        case 3://get html v2/inboxinvoice/{id}/html/{isStandartXslt}
                            Stream IIGetHtmlTest = await inboxInvoiceClient.GetHtml(System.Guid.Parse("9758339E-EB0B-454C-8C2F-18059F944A3F"), true);
                            System.Console.WriteLine(IIGetHtmlTest);
                            break;

                        case 4://get pdf
                            Stream IIGetPdfTest = await inboxInvoiceClient.GetPdf(System.Guid.Parse("9758339E-EB0B-454C-8C2F-18059F944A3F"), true);
                            System.Console.WriteLine(IIGetPdfTest);
                            break;
                        case 5://get ubl
                            Stream IIGetUblTest = await inboxInvoiceClient.GetUbl(System.Guid.Parse("9758339E-EB0B-454C-8C2F-18059F944A3F"));
                            System.Console.WriteLine(IIGetUblTest);
                            break;

                        case 6://put isNewFlag ==> JSON reader exception
                            System.Console.WriteLine("testing endpoint : /v1/inboxinvoice/updateisnew");
                            System.Guid InvoxInvoicePutIsNewFlagId1 = System.Guid.Parse("31831769-F99B-4A22-598A-08DA1B9242E9");
                            System.Guid InvoxInvoicePutIsNewFlagId2 = System.Guid.Parse("95AF87D3-8990-461A-13B8-08DA1B923FC3");
                            System.Guid InvoxInvoicePutIsNewFlagId3 = System.Guid.Parse("2D69C3BF-D02A-43BF-306B-08DA1B8CFAF2");
                            List<UpdateIsNewModel> updateIsNewTest = new List<UpdateIsNewModel>()
                               {
                                   new UpdateIsNewModel(){
                                       InvoiceId = InvoxInvoicePutIsNewFlagId1,
                                       IsNew = true
                                   },
                                   new UpdateIsNewModel(){
                                       InvoiceId =InvoxInvoicePutIsNewFlagId2,
                                       IsNew = true
                                   },
                                   new UpdateIsNewModel(){
                                       InvoiceId = InvoxInvoicePutIsNewFlagId3,
                                       IsNew = true
                                   }
                               };
                            var InboxInvoiceUpdateIsNewTest = await inboxInvoiceClient.UpdateIsNew(updateIsNewTest);
                            System.Console.WriteLine("Result : " + InboxInvoiceUpdateIsNewTest + "\n\n" +
                                "**********************");
                            break;

                        default:
                            menuCode = -1;
                            subMenuCode = -1;
                            break;

                    }

                    #endregion
                }
                else if (menuCode == 9)
                {
                    System.Console.WriteLine("Test fonksiyonu:");
                    subMenuCode = -1;
                    System.Console.WriteLine("guid : " + new System.Guid());
                    //outboxInvoiceClient.GetPdfTest(System.Guid.Parse("B1120903-EE7E-4432-8ACF-038207F17CD0"), true);
                    System.Console.WriteLine("Test fonksiyonu tamamlandı.");
                }
                else if (menuCode == 10)
                {
                    System.Console.WriteLine("e-Bilet Client Fonksiyonları");
                    System.Console.WriteLine("1 - POST /v1/event-ticket");
                    System.Console.WriteLine("2 - POST /v1/passenger-ticket");
                    subMenuCode = System.Convert.ToInt32(System.Console.ReadLine());
                    switch (subMenuCode)
                    {
                        case 1:
                            //Create a new event ticket
                            var eventTicketModel = eBiletHelper.CreateSampleEventTicketModel(TicketStatus.Draft);
                            var draftEventTicket = await eventTicketClient.Post(eventTicketModel);

                            // Get an event ticket
                            var eventTicket = await eventTicketClient.Get(new Guid(draftEventTicket.Ettn));

                            // Get an event detail
                            var eventTicketDetail = await eventTicketClient.GetDetail(new Guid(draftEventTicket.Ettn));

                            //Get a paginated event ticket list
                            var paginatedEventTicketList = await eventTicketClient.GetTicketList(
                                new QueryFilterBuilder<EventTicketModel>()
                                    .PageIndex(2)
                                    .PageSize(100)
                                    .QueryFor(x => x.IsArchived, Operator.Equal, false)
                                    .Build());

                            //Get an event ticket Pdf stream
                            var eventTicketPdfStream = await eventTicketClient.GetPdfs(new MultiSelectModel<Guid>
                            {
                                Selected = new List<Guid> { new Guid(draftEventTicket.Ettn) }
                            });

                            //Get an event ticket Html stream
                            var eventTicketHtmlStream = await eventTicketClient.GetHtml(new Guid(draftEventTicket.Ettn));

                            //Get an event ticket Xml stream
                            var eventTicketXmlStream = await eventTicketClient.GetXml(new Guid(draftEventTicket.Ettn));

                            //Get event tickets statuses
                            var eventTicketsStatuses = await eventTicketClient.GetStatuses(new List<Guid> { new Guid(draftEventTicket.Ettn) });

                            //Update an event ticket
                            eventTicketModel.CustomerCity = "Updated Test City";
                            var updatedEventTicket = await eventTicketClient.Put(eventTicketModel);

                            //Update the status of event tickets
                            var isUpdateStatusSucceed = await eventTicketClient.UpdateStatus(new UpdateTicketStatusModel()
                            {
                                Status = TicketStatus.Queued,
                                Ids = new List<Guid> { new Guid(draftEventTicket.Ettn) }
                            });

                            //Cancel an event ticket
                            var approvedEventTicketList = await eventTicketClient.GetTicketList(
                                new QueryFilterBuilder<EventTicketModel>()
                                    .PageSize(1)
                                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                                    .Build());
                            var isCancelSucceed = await eventTicketClient.Cancel(new CancelledTicketModel
                            {
                                Ids = approvedEventTicketList.Items.Select(ticket => ticket.Id).ToList()
                            });
                            break;
                        case 2:
                            var passsengerTicketModel = eBiletHelper.CreateSamplePassengerTicketModel(TicketStatus.Draft);
                            var draftPassengerTicket = await passengerTicketClient.Post(passsengerTicketModel);

                            // Get an passenger ticket
                            var passengerTicket = await passengerTicketClient.Get(new Guid(draftPassengerTicket.Ettn));

                            // Get an passenger detail
                            var passengerTicketDetail = await passengerTicketClient.GetDetail(new Guid(draftPassengerTicket.Ettn));

                            //Get a paginated passenger ticket list
                            var paginatedPassengerTicketList = await passengerTicketClient.GetTicketList(
                                new QueryFilterBuilder<PassengerTicketModel>()
                                    .PageIndex(2)
                                    .PageSize(100)
                                    .QueryFor(x => x.IsArchived, Operator.Equal, false)
                                    .Build());

                            //Get an passenger ticket Pdf stream
                            var passengerTicketPdfStream = await passengerTicketClient.GetPdfs(new MultiSelectModel<Guid>
                            {
                                Selected = new List<Guid> { new Guid(draftPassengerTicket.Ettn) }
                            });

                            //Get an passenger ticket Html stream
                            var passengerTicketHtmlStream = await passengerTicketClient.GetHtml(new Guid(draftPassengerTicket.Ettn));

                            //Get an passenger ticket Xml stream
                            var passengerTicketXmlStream = await passengerTicketClient.GetXml(new Guid(draftPassengerTicket.Ettn));

                            //Get passenger tickets statuses
                            var passengerTicketStatuses = await passengerTicketClient.GetStatuses(new List<Guid> { new Guid(draftPassengerTicket.Ettn) });

                            //Update an passenger ticket
                            passsengerTicketModel.CustomerCity = "Updated Test City";
                            var updatedPassengerTicket = await passengerTicketClient.Put(passsengerTicketModel);

                            //Update the status of passenger tickets
                            var isPassengerUpdateStatusSucceed = await passengerTicketClient.UpdateStatus(new UpdateTicketStatusModel()
                            {
                                Status = TicketStatus.Queued,
                                Ids = new List<Guid> { new Guid(draftPassengerTicket.Ettn) }
                            });

                            //Cancel an passenger ticket
                            var approvedPassengerTicketList = await passengerTicketClient.GetTicketList(
                                new QueryFilterBuilder<PassengerTicketModel>()
                                    .PageSize(1)
                                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                                    .Build());

                            var isPassengerCancelSucceed = await passengerTicketClient.Cancel(new CancelledTicketModel
                            {
                                Ids = approvedPassengerTicketList.Items.Select(ticket => ticket.Id).ToList()
                            });
                            break;
                        default:
                            break;
                    }
                }
                else if (menuCode == 0)
                {
                    subMenuCode = -1;
                    System.Console.WriteLine("Çıkış başarılı.");
                }
                else
                {
                    System.Console.WriteLine("Geçersiz bir kod girdiniz. Lütfen menüde belirtilen kodlardan birini giriniz.");
                }
            } while (menuCode != 0);



        }
    }
}
