using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NRuler.Command;

namespace NRuler
{
    public class Invoke
    {
        public ICommand _command;

        public Invoke(ICommand command)
        {
            this._command = command;
        }

        public void ExecuteCommand()
        {
            _command.Action();
        }
    }
}
