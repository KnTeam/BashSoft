﻿using BashSoft.Contracts;
using BashSoft.Contracts.IO;
using BashSoft.Exceptions;
using SimpleJudge;
using System;

namespace BashSoft.IO.Commands
{
    public class MakeDirectoryCommand : Command, IExecutable
    {
        public MakeDirectoryCommand(string input, string[] data, Tester judge, StudentsRepository repository, IDirectoryManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {

        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
            string folderName = this.Data[1];
            this.InputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
