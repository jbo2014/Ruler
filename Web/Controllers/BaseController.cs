using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NRuler;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected REnginer enginer = null;

        public BaseController() 
        {
            string rulesPath = string.Empty;
            if (ConfigurationManager.AppSettings["NRuler.RulesPath"] == null)
                throw new Exception("不存在REngine.RulefilesPath的key，在AppSetting中");
            rulesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["NRuler.RulesPath"]);
            if (!System.IO.Directory.Exists(rulesPath))
                throw new Exception("规则文件目录不存在");

            enginer = REnginer.GetInstance(rulesPath);
            enginer.LoadRules();
        }

    }
}
