using System.Collections.Generic;
using System.Threading.Tasks;
using ePlatform.Api;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.SampleNetCoreApp
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonClient commonClient;

        public CommonController(ICommonClient commonClient)
        {
            this.commonClient = commonClient;
        }
        [HttpGet("isuser/{id}")] //id=0010047133
        public async Task<ActionResult<bool>> Get(string id)
        {
            return await commonClient.IsUser(id, 1);
        }
        [HttpGet("getuser/{id}")] //id=0010047133
        public async Task<ActionResult<GibUserWithAliasModel>> GetUser(string id)
        {
            return await commonClient.GetUser(id);
        }

        [HttpGet("currency")]
        public async Task<ActionResult<List<CurrencyModel>>> CurrencyCodeList()
        {
            return await commonClient.CurrencyCodeList();
        }
        [HttpGet("unit")]
        public async Task<ActionResult<List<UnitCodeModel>>> UnitCodeList()
        {
            return await commonClient.UnitCodeList();
        }
        [HttpGet("taxtypecode")]
        public async Task<ActionResult<List<TaxExemptionReasonModel>>> TaxExemptionReasonList()
        {
            return await commonClient.TaxExemptionReasonList();
        }
        [HttpGet("country")]
        public async Task<ActionResult<List<CountryModel>>> CountrList()
        {
            return await commonClient.CountrList();
        }
    }
}
