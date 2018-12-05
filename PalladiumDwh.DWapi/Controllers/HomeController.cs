using System.Web.Http.Description;
using System.Web.Mvc;

namespace PalladiumDwh.DWapi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
