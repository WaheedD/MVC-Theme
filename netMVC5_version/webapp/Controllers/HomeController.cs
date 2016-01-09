#region Using

using System.Web.Mvc;
using SmartAdminMvc.Models;

#endregion

namespace SmartAdminMvc.Controllers
{
    [SmartAdminMvcAuthorize]
    public class HomeController : Controller
    {
        // GET: home/index
        public ActionResult Index()
        {
            return View();
        }
    }
}