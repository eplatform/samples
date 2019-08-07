namespace ePlatform.Integration.Models.Enums
{
    public enum InvoiceStatus
    {
        [EnumText("Hepsi")]
        All = 999,
        [EnumText("Taslak")]
        Draft = 0,
        [EnumText("Test")]
        Test = 5,
        [EnumText("İptal")]
        Canceled = 10,
        [EnumText("Kuyrukta")]
        Queued = 20,
        [EnumText("Gönderim Bekliyor")]
        Running = 30,
        [EnumText("Hata")]
        Error = 40,
        [EnumText("GIB'e İletildi")]
        SentToGib = 50,
        [EnumText("Onaylandı")]
        Approved = 60,
        [EnumText("Onaylanıyor")]
        WaitingApprove = 61,
        [EnumText("Onaylama Hatası")]
        FailedApprove = 62,
        [EnumText("Otomatik Onaylandı")]
        AutomaticApproved = 65,
        [EnumText("Onay Bekliyor")]
        WaitingForAprovement = 70,
        [EnumText("Reddedildi")]
        Declined = 80,
        [EnumText("Reddediliyor")]
        WaitingDecline = 81,
        [EnumText("Reddetme Hatası")]
        FailedDecline = 82,
        [EnumText("İade")]
        Return = 90,
        [EnumText("İade ediliyor")]
        WaitingReturn = 91,
        [EnumText("İade Hatası")]
        FailedReturn = 92,
        [EnumText("e-Arşiv İptal")]
        EArsivCanceled = 100
    }
}