using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ePlatform.Api;

namespace ePlatform.Api.SampleNetFrameworkMvc
{
    public class HomeController : Controller
    {
        private readonly ICommonClient _commonClient;
        private readonly IOutboxInvoiceClient _outboxInvoiceClient;

        public HomeController(
            ICommonClient commonClient,
            IOutboxInvoiceClient outboxInvoiceClient)
        {
            _commonClient = commonClient;
            _outboxInvoiceClient = outboxInvoiceClient;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _commonClient.CountrList());
        }

        public async Task<ActionResult> Invoices()
        {
            //return
            //    null;
            var model = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageIndex(1)
                .QueryFor(q => q.ExecutionDate, Operator.LessThan, DateTime.Now)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                // .QueryFor(q => q.InvoiceNumber, Operator.Equal, "EPK2019000001731")
                // .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Error)
                .Build();
            var invoices = await _outboxInvoiceClient.GetUIInvoices(new UIInvoices
            {
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now
            });
            return View(invoices);
        }
    }
}
