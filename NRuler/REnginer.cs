using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NRuler.Worker;
using NRuler.Command;
using NRuler.Model;
using NRuler.Helper;
using System.Text.RegularExpressions;

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
            config.RulefilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.RulefilesPath);

            if (!System.IO.Directory.Exists(config.RulefilesPath))
                throw new Exception("规则文件目录不存在");
        }
        
        /// <summary>
        /// 原方法-加载所有规则
        /// </summary>
        public void LoadRules0()
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

            List<Region> regions = RegionParser.ParseRegions(text.ToString());
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
        /// 新方法-加载所有规则
        /// </summary>
        public void LoadRules()
        {
            var files = Directory.GetFiles(config.RulefilesPath, "*.rule");
            if (files == null || files.Length == 0)
                throw new Exception("rule文件不存在");

            List<Region> regions = new List<Region>();
            string fileName = string.Empty;
            StringBuilder text = new StringBuilder();
            files.ToList().ForEach(file =>
            {
                fileName = Path.GetFileNameWithoutExtension(file);
                var fileText = File.ReadAllText(file);
                config.FileRegion[fileName] = RegionParser.ParseRegions(fileText.ToString());
                regions.AddRange(config.FileRegion[fileName]);
                text.AppendLine(fileText);
            });

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
        public void UpdateRule(string fileName, string regionName, string ruleName, string ruleContent)
        {
            // 修改config中保存的Rule
            config.RuleDefinations[regionName + "." + ruleName].RuleContent = ruleContent;

            // 修改Rule文件中的规则内容
            var fileTxt = File.ReadAllText(config.RulefilesPath+"/"+fileName + ".rule");
            string regStr = @"(rule " + ruleName + @")[\s\S]*?(end rule)";
            string newFileTxt = RegexReplaceRule(fileTxt, "rule " + ruleName + "\r\n" + ruleContent + " end rule", regStr, 0, 1, false);
            File.WriteAllText(config.RulefilesPath + "/" + fileName + ".rule", newFileTxt, Encoding.UTF8);
        }

        /// <summary>
        /// 获取字符串,查找替换
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="val">替换值</param>
        /// <param name="rex">查找规则[正则,如:@"\d{2}",直接匹配字符,如:'abc']..注意正则转义</param>
        /// <param name="textIndex">要替换出现的位置从1开始</param>
        /// <param name="once">如果只出现一次,是否替换,[0:否,1:是]</param>
        /// <param name="flag">是否全部替换</param>
        /// <returns></returns>
        private string RegexReplaceRule(string text, string val, string rex, int textIndex,int once, bool flag)
        {
            Regex rx = new Regex(rex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection match = rx.Matches(text);
        
            //全部替换或者出现1次是否替换,once==1,替换
            if (flag == true ||(match.Count==1 && once==1))
            {
                return Regex.Replace(text, rex, val);
            }
       
            //判断出现的数量,与替换位置,这里是出现过2次
            if (match.Count > 1 && textIndex <= match.Count)
            {
                int count = 0;//定义出现位置
                string str1 = string.Empty;//定义出现字符位置的前面字符串
                string str2 = string.Empty;//定义出现字符位置的后面字符串
                foreach (Match m in match)
                {
                    int index = m.Index;
                    str1 = text.Substring(0, index);//截取前面字符串
                    string s = m.Value;//查找规则的值
                    str2 = text.Substring(index + s.Length);//截取后面字符串
                    count++;
                    //如果出现位置与查找位置相同,则返回
                    if (count == textIndex)
                    {
                        return str1 + val + str2;//前面字符串+替换的字符串+后面字符串
                    }
                }
            }
            return text;//如果没有找到,返回原字符串
        }
        
        private static string DefaultRuleName = "default";
        private static string LocateRuleContent(string ruleRegionId, string ruleId)
        {
            var key = string.Format("{0}.{1}", ruleRegionId, ruleId);
            if (config.RuleDefinations.ContainsKey(key))
                return config.RuleDefinations[key].RuleContent;

            if (config.ThrowExceptionIfNotfoundRule)
                throw new Exception("没有找到" + key);

            key = string.Format("{0}.{1}", ruleRegionId, DefaultRuleName);
            if (config.RuleDefinations.ContainsKey(key))
                return config.RuleDefinations[key].RuleContent;

            throw new Exception("没有找到" + key);
        }

        public ParamInfo SetParam(string name, object value) 
        {
            ParamInfo info = new ParamInfo();
            info.ParamName = name;
            info.ParamValue = value;
            return info;
        }

        public object Execute(string ruleRegionId, string ruleId=null, params ParamInfo[] paramArray) 
        {
            Rule rule = new Rule();
            rule.RegionName = ruleRegionId;
            rule.RuleName = string.IsNullOrEmpty(ruleId)?string.Empty:ruleId;
            rule.RuleContent = LocateRuleContent(ruleRegionId, ruleId);

            IWorker worker = new WorkerJsnet();
            ICommand command = new CommandJsnet(worker, rule, paramArray);
            Invoke i = new Invoke(command);

            //return i.ExecuteCommand<float>();
            return i.ExecuteCommand();
        }
    }
}
