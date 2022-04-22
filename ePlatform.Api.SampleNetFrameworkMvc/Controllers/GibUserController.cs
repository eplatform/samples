using System.Threading.Tasks;
using System.Web.Mvc;
using ePlatform.Api;

namespace ePlatform.Api.SampleNetFrameworkMvc
{
    public class GibUserController : Controller
    {
        // GET: GibUser
        private readonly ICommonClient commonClient;

        public GibUserController(ICommonClient commonClient)
        {
            this.commonClient = commonClient;
        }
        public async Task<ActionResult> Index()
        {
            return View(await commonClient.GetUserAliasListZip());
        }
    }
}
