using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruler
{
    internal interface IJavascriptGenerator
    {
        string GenerateJS(string jsCode);
    }
}
