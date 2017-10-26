using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using NRuler;

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
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<Models.Section>>(form["RuleContent"]);
            if (list.Any())
            {

                foreach (var item in list)
                {

                }
            }

            if (true)
                return Content("OK");
            else
                return new RedirectResult("/ExeRule");
        }

    }
}
