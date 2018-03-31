namespace BashSoft.IO.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Contracts.IO;
    using BashSoft.Exceptions;
    using SimpleJudge;
    using System.Diagnostics;

    public class OpenFileCommand : Command, IExecutable
    {
        public OpenFileCommand(string input, string[] data, Tester judge, StudentsRepository repository, IDirectoryManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {

        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
            string fileName = this.Data[1];

            Process.Start(SessionData.currentPath + "\\" + fileName);
        }
    }
}
