using System;
using System.Collections.Generic;

namespace ePlatform.Api.SampleNetCoreConsole
{
    public class eBiletHelper
    {
        #region Ticket Helper Methods
        public static TicketBuilderModel CreateSampleEventTicketModel(TicketStatus status)
        {
            var eventTicket = CreateSampleTicketModel(status);
            eventTicket.TicketType = TicketType.ETKINLIK;
            eventTicket.EventTime = DateTime.Now.AddDays(2);
            eventTicket.EventName = "Tarkan Konseri - 21";
            eventTicket.EventLocation = "Harbiye Açık Hava 2021";
            eventTicket.EventCity = "İstanbul";
            eventTicket.EventCityId = 80;
            eventTicket.EventMunicipality = "Beyoğlu";
            eventTicket.EventDescription = "Tarkan - Harbiye Açık Hava Konseri 2021";
            eventTicket.EventOrganizerVknTckn = "12345678901";
            return eventTicket;
        }
        public static TicketBuilderModel CreateSamplePassengerTicketModel(TicketStatus status)
        {
            var passengerTicket = CreateSampleTicketModel(status);
            passengerTicket.TicketType = TicketType.YOLCU;
            passengerTicket.VehiclePlate = "34ABC34";
            passengerTicket.DepartureDate = DateTime.Now.AddDays(2);
            passengerTicket.ExpeditionTime = DateTime.Now.AddDays(2);
            passengerTicket.ExpeditionNumber = "123";
            passengerTicket.DepartureLocation = "Sakarya";
            passengerTicket.VehicleOperatingTitle = "Test";
            passengerTicket.VehicleOperatingVknTckn = "12345678901";
            passengerTicket.VehicleOperatingTaxCenter = "Test";
            return passengerTicket;
        }

        public static TicketBuilderModel CreateSampleTicketModel(TicketStatus status)
        {
            return new TicketBuilderModel
            {
                Ettn = Guid.NewGuid(),
                Status = status,
                TicketNumber = $"ABC{DateTime.Now.Year}{new Random().Next(100000000, 999999999)}",
                Prefix = "ABC",
                ReferenceNumber = "RFR1344",
                TicketDate = DateTime.Now,
                DocumentType = DocumentType.SATIS,
                CurrencyCode = Currency.TRY,
                Identifier = "273378723",
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                CustomerStreet = "a",
                CustomerBuildingName = "a",
                CustomerBuildingNo = "a",
                CustomerDoorNo = "a",
                CustomerTown = "a",
                CustomerDistrict = "a",
                CustomerCity = "a",
                CustomerTelephone = "5367766470",
                CustomerEmail = "johndoe@eplatform.com.tr",
                IsEmailSend = true,
                PaymentType = PaymentType.DIGER,
                PaymentDescription = "aa",
                SeatNumber = "A34",
                CommissionAmount = 10,
                CommissionTaxAmount = 10,
                TicketLines = new List<TicketLine>
                {
                    new TicketLine
                    {
                        ServiceType = ServiceType.DIGER,
                        ServiceDescription = "Bilet Bedeli + Hizmet Bedeli",
                        Amount = 100,
                        DiscountRate = 10,
                        DiscountAmount = 10,
                        VatRate = 18,
                        VatAmount = 16.2m,
                        Taxes = new List<TaxModel>
                        {
                            new TaxModel
                            {
                                TaxCode = "0030",
                                TaxName = "Özel vergi",
                                TaxRate = 20,
                                TaxAmount = 100
                            },
                            new TaxModel
                            {
                                TaxCode = "0030",
                                TaxName = "Özel vergi",
                                TaxRate = 20,
                                TaxAmount = 100
                            }
                        }
                    }
                },
                Notes = new List<NoteModel>
                {
                    new NoteModel {Note = "örnek not 1"},
                    new NoteModel {Note = "örnek not 2"}
                }
            };
        }
        #endregion
    }
}
