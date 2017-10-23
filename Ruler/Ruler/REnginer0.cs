using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruler.Worker;
using Noesis.Javascript;

namespace Ruler
{
    public class REnginer
    {
        #region 声明属性
        
        // 定义一个静态变量来保存类的实例
        private static REnginer uniqueInstance;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        public RConfig config;

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

            config.RulesPath = rulesPath;
            config.RulesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.RulesPath);

            if (!System.IO.Directory.Exists(config.RulesPath))
                throw new Exception("规则文件目录不存在");
        }

        /// <summary>
        /// 查找所需规则
        /// </summary>
        public void FindRule(params string[] ruleStrs)
        {
            //反转数组
            Array.Reverse(ruleStrs);
        }
    }
}
