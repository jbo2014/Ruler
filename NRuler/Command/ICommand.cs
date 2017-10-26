using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NRuler.Worker;
using NRuler.Model;

namespace NRuler.Command
{
    public abstract class ICommand
    {
        // 命令应该知道接收者是谁，所以有Receiver这个成员变量
        internal IWorker _worker;
        internal Rule _rule;
        internal ParamInfo[] _paramArray;

        internal ICommand(IWorker worker, Rule rule, params ParamInfo[] paramArray)
        {
            this._worker = worker;
            this._paramArray = paramArray;
            this._rule = rule;
        }
 
        // 命令执行方法
        internal abstract object Action();

        // 命令执行方法
        internal abstract T Action<T>();
    }
}
