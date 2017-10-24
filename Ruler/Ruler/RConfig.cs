using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using Ruler.Parsers;
=======
using Ruler.Parser;
>>>>>>> 93922520154069d35f4166fec1dcf120a5944033

namespace Ruler
{
    internal class RConfig
    {
        public RConfig()
        {
            this.RuleDefinations = new Dictionary<string, RuleDefination>();
        }

        public bool ThrowExceptionIfNotfoundRule { get; set; }
        public string RulefilesPath { get; set; }
        public Dictionary<string, RuleDefination> RuleDefinations { get; set; }
    }
}
