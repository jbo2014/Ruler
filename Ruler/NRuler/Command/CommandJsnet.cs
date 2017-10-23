using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NRuler.Worker;

namespace NRuler.Command
{
    class CommandJsnet : ICommand
    {
        public CommandJsnet(IWorker worker)
            : base(worker)
        { 
        }
 
        public override object Action()
        {
            // 调用接收的方法，因为执行命令的是学生
            _worker.Run();
            return _worker.GetResult();
        }
    }
}
