using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NRuler;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            string rulesPath = string.Empty;
            if (ConfigurationSettings.AppSettings["NRuler.RulesPath"] == null)
                throw new Exception("不存在REngine.RulefilesPath的key，在AppSetting中");
            rulesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationSettings.AppSettings["NRuler.RulesPath"]);
            if (!System.IO.Directory.Exists(rulesPath))
                throw new Exception("规则文件目录不存在");

            REnginer enginer = REnginer.GetInstance(rulesPath);

            Console.WriteLine("Fee");
            {
                enginer.LoadRules();
                
                Console.WriteLine("0: " + enginer.Execute("交税"));

                Console.WriteLine("1000: " + enginer.Execute("交税", "员工", enginer.SetParam("salary", 1000)));
                Console.WriteLine("6000: " + enginer.Execute("交税", "员工", enginer.SetParam("salary", 6000)));
                Console.WriteLine("8000: " + enginer.Execute("交税", "员工", enginer.SetParam("salary", 8000)));
                Console.WriteLine("500: " + enginer.Execute("交税", "员工", enginer.SetParam("salary", 500)));

                Console.WriteLine("12000: " + enginer.Execute("交税", "领导", enginer.SetParam("salary", 12000)));
                Console.WriteLine("21000: " + enginer.Execute("交税", "领导", enginer.SetParam("salary", 21000)));
                Console.WriteLine("1000: " + enginer.Execute("交税", "领导", enginer.SetParam("salary", 1000)));

                Console.WriteLine("员工.7000: " + enginer.Execute("交税", "ALL", enginer.SetParam("user", new User { post = "员工", salary = 7000 })));
                Console.WriteLine("员工.9200: " + enginer.Execute("交税", "ALL", enginer.SetParam("user", new User { post = "员工", salary = 9200 })));
                Console.WriteLine("领导.19000: " + enginer.Execute("交税", "ALL", enginer.SetParam("user", new User { post = "领导", salary = 19000 })));
                Console.WriteLine("领导.23000: " + enginer.Execute("交税", "ALL", enginer.SetParam("user", new User { post = "领导", salary = 23000 })));
                
                Console.WriteLine("请修改规则<领导>:");
                string ruleStr = Console.ReadLine();
                enginer.UpdateRule("交税", "领导", ruleStr);
                Console.WriteLine("30000: " + enginer.Execute("交税", "领导", enginer.SetParam("salary", 30000)));

                Console.ReadKey();
            }
        }
    }

    public class User 
    {
        public string post { get; set; }
        public int salary { get; set; }

        public int wl()
        {
            return salary += 500;
        }
    }
}
