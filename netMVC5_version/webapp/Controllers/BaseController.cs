using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class BaseController : Controller
    {
        public virtual bool IsAuthenticated
        {
            get
            {
                return false;
            }
        }
    }
}
