using SimpleJudge;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.IO.Commands
{
    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {

        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
