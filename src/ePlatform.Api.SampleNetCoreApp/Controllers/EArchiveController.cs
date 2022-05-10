using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ePlatform.Api;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.SampleNetCoreApp
{

    [Route("api/[controller]")]
    [ApiController]
    public class EArchiveController : ControllerBase
    {
        private readonly IEArchiveInvoiceClient earchiveClient;

        public EArchiveController(IEArchiveInvoiceClient earchiveClient)
        {
            this.earchiveClient = earchiveClient;
        }

        //[HttpGet("{id}")] //B980FFE7-A6F0-4072-AACE-119CCB40A483
        //public async Task<ActionResult<OutboxInvoiceGetModel>> Get(Guid id)
        //{
        //    return await earchiveClient.Get(id);
        //}

        [HttpGet("cancelinvoice")] //f201ba2e-881f-4798-a715-d6090a28d7b2
        public async Task<ActionResult> Get()
        {
            Guid[] model = new Guid[]{
                new Guid("f201ba2e-881f-4798-a715-d6090a28d7b2"),
                new Guid("f201ba2e-881f-4798-b623-d6090a28d7b2")};
            return Ok(await earchiveClient.Cancel(model));
        }

        [HttpGet("getmailddetail/{id}")] //e6f321ba-12c3-460b-87b3-04ac9887deb
        public async Task<ActionResult<List<EarsivInvoiceMailModel>>> GetMailDetail(string id)
        {
            return Ok(await earchiveClient.GetMailDetail(id));
        }


    }
}
