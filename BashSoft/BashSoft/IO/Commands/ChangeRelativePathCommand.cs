﻿namespace BashSoft.IO.Commands
{
    using SimpleJudge;
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.IO;

    public class ChangeRelativePathCommand : Command, IExecutable
    {
        public ChangeRelativePathCommand(string input, string[] data, Tester judge, StudentsRepository repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string relPath = this.Data[1];
            this.InputOutputManager.ChangeCurrentDirectoryRelative(relPath);
        }
    }
}
