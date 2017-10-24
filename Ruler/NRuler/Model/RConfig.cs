using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NRuler.Model
{
    internal class RConfig
    {
        public RConfig()
        {
            this.RuleDefinations = new Dictionary<string, Rule>();
        }

        public bool ThrowExceptionIfNotfoundRule { get; set; }
        public string RulefilesPath { get; set; }
        public Dictionary<string, Rule> RuleDefinations { get; set; }
    }
}
