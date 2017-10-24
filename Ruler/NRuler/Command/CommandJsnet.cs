using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NRuler.Worker;
using NRuler.Model;

namespace NRuler.Command
{
    class CommandJsnet : ICommand
    {
        internal CommandJsnet(IWorker worker, Rule rule, params ParamInfo[] paramArray)
            : base(worker, rule, paramArray)
        { 
        }

        //internal object SetParams(params ParamInfo[] paramArray)
        //{
        //    return _worker.SetParams(paramArray);
        //}

        // 命令执行方法
        internal override object Action()
        {
            _worker.Run(_rule.RuleContent, _paramArray);
            return _worker.GetResult();
        }

        internal override T Action<T>()
        {
            _worker.Run(_rule.RuleContent, _paramArray);
            return _worker.GetResult<T>();
        }
    }
}
