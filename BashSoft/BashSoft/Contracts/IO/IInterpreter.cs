using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts.IO
{
    public interface IInterpreter
    {
        void InterpretCommand(string command);
    }
}
