using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NRuler.Worker
{
    interface IWorker
    {
        void SetParameter();
        void Run();
        object GetResult();
    }
}
