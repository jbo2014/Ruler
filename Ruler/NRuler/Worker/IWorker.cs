using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NRuler.Worker
{
    public interface IWorker
    {
        List<ParamInfo> SetParam(string paramName, object paramValue);
        List<ParamInfo> SetParams(params ParamInfo[] paramList);
        object Run(string externalCode, params ParamInfo[] parameters);
        T GetResult<T>();
        object GetResult();
    }
}
