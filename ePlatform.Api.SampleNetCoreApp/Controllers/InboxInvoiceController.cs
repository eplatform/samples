using System;
using System.Threading.Tasks;
using ePlatform.Api;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.SampleNetCoreApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxInvoiceController : ControllerBase
    {
        private readonly IInboxInvoiceClient inboxInvoiceClient;

        public InboxInvoiceController(IInboxInvoiceClient inboxInvoiceClient)
        {
            this.inboxInvoiceClient = inboxInvoiceClient;
        }

        /*[HttpGet("{id}")] //f201ba2e-881f-4798-a715-d6090a28d7b2
        public async Task<ActionResult<InboxInvoiceGetModel>> Get(Guid id)
        {
            return await inboxInvoiceClient.Get(id);
        }*/

        /*[HttpGet("getinvoice/{id}")] //1d9a9825-d0bb-4303-9d87-f3ae435cedc4
        public async Task<ActionResult<UblBuilderModel>> GetInvoice(Guid id)
        {
            return await inboxInvoiceClient.GetInvoice(id);
        }*/

        [HttpGet("list")]
        public async Task<ActionResult<PagedList<InboxInvoiceGetModel>>> GetList()
        {
            var model = new QueryFilterBuilder<InboxInvoiceGetModel>()
                .PageIndex(1)
                .QueryFor(q => q.InvoiceNumber, Operator.Equal, "EPK2019000001731")
            // .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Error)
                .Build();
            return await inboxInvoiceClient.Get(model);
        }

        [HttpGet("previewhtml/{id}")] //48653485-b3ac-4daa-b9b4-aa8418a3a86b
        public async Task<IActionResult> PreviewHtml(Guid id)
        {
            var t = await inboxInvoiceClient.GetHtml(id, false);
            return Ok(t);
        }

        [HttpGet("pdf/{id}")] //48653485-b3ac-4daa-b9b4-aa8418a3a86b
        public async Task<IActionResult> GetPdf(Guid id)
        {
            var data = await inboxInvoiceClient.GetPdf(id, false);
            return File(data, "application/pdf", $"{id}.zip");
        }

        [HttpGet("ubl/{id}")] //f201ba2e-881f-4798-a715-d6090a28d7b2
        public async Task<IActionResult> GetUbl(Guid id)
        {
            var data = await inboxInvoiceClient.GetUbl(id);
            return File(data, "application/pdf", $"{id}.zip");
        }

        // [HttpGet("sendorremovearchive/{id}")] //f201ba2e-881f-4798-a715-d6090a28d7b2
        // public async Task<IActionResult> SendOrRemoveArchive(Guid id)
        // {
        //     var data = await inboxInvoiceClient.SendOrRemoveArchive(id, false);
        //     return Ok(data);
        // }
    }
}
