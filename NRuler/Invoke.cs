using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NRuler.Command;

namespace NRuler
{
    class Invoke
    {
        private ICommand _command;

        public Invoke(ICommand command)
        {
            this._command = command;
        }

        public object ExecuteCommand()
        {
            return _command.Action();
        }

        public T ExecuteCommand<T>()
        {
            return _command.Action<T>();
        }
    }
}
