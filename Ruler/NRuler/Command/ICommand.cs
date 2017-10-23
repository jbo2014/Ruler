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
        protected IWorker _worker;
        protected Rule rule;

        public ICommand(IWorker worker)
        {
            this._worker = worker;
        }
 
        // 命令执行方法
        public abstract object Action();
    }
}
