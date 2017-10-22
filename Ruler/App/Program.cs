using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Ruler;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ConfigurationSettings.AppSettings["Ruler.RulesPath"] == null)
                throw new Exception("App.config缺少Key：Ruler.RulesPath");
            REnginer enginer = REnginer.GetInstance(ConfigurationSettings.AppSettings["Ruler.RulesPath"]);

            string result="";
            enginer.FindRule("考勤", "迟到")                             //获取需要的规则对象
                .SetIn("time", DateTime.Now.TimeOfDay)      //设置输入对象
                .SetOut("", result)                                //设置输出对象
                .Run();
        }
    }
}
