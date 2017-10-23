using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NRuler.Worker;
using NRuler.Command;

namespace NRuler
{
    public class REnginer
    {
        #region 声明属性
        
        // 定义一个静态变量来保存类的实例
        private static REnginer uniqueInstance;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        private static RConfig config;

        #endregion

        // 定义私有构造函数，使外界不能创建该类实例
        private REnginer(string rulesPath)
        {
            SetRulesPath(rulesPath);
        }

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static REnginer GetInstance(string rulesPath)
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            // 双重锁定只需要一句判断就可以了
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new REnginer(rulesPath);
                    }
                }
            }
            return uniqueInstance;
        }

        /// <summary>
        /// 确定规则文件目录
        /// </summary>
        private void SetRulesPath(string rulesPath)
        {
            config = new RConfig();

            if (string.IsNullOrEmpty(rulesPath))
                throw new Exception("请先在配置文件中指定规则文件目录");

            config.RulefilesPath = rulesPath;
            config.RulefilesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.RulefilesPath);

            if (!System.IO.Directory.Exists(config.RulefilesPath))
                throw new Exception("规则文件目录不存在");
        }
        
        /// <summary>
        /// 加载所有规则
        /// </summary>
        public void LoadRules()
        {
            var files = System.IO.Directory.GetFiles(config.RulefilesPath, "*.rule");
            if (files == null || files.Length == 0)
                throw new Exception("rule文件不存在");

            StringBuilder text = new StringBuilder();
            files.ToList().ForEach(file =>
            {
                var fileText = System.IO.File.ReadAllText(file);
                text.AppendLine(fileText);
            });

            List<RuleRegion> regions = RegionParser.ParseRegions(text.ToString());
            if (regions == null || regions.Count == 0)
                throw new Exception("region不存在");

            regions.ForEach(region =>
            {
                var rules = RuleParser.ParseRules(region.RegionContent);

                if (regions == null || regions.Count == 0)
                    throw new Exception(string.Format("region '{0}' 无法找到rule", region.RegionName));

                rules.ForEach(rule =>
                {
                    rule.RegionName = region.RegionName;
                });

                rules.ForEach(rule =>
                {
                    var key = string.Format("{0}.{1}", rule.RegionName, rule.RuleName);
                    config.RuleDefinations[key] = rule;
                });
            });
        }

        /// <summary>
        /// 加载单独规则
        /// </summary>
        public void LoadRule() 
        {

        }

        /// <summary>
        /// 修改规则
        /// </summary>
        public void UpdateRule()
        {

        }


        public void Execute() 
        {
            IWorker worker = new WorkerJsnet();
            ICommand command = new CommandJsnet(worker);
            command.

            Invoke i = new Invoke(command);

            i.ExecuteCommand();
        }
    }
}
