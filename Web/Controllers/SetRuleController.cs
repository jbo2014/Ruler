using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SetRuleController : BaseController
    {
        //
        // GET: /SetRule/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (true)
                return Content("OK");
            else
                return new RedirectResult("/ExeRule");
        }

    }
}
