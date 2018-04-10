namespace BashSoft.IO.Commands
{
    using System;
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Attributes;
    using BashSoft.Contracts.IO;

    [Alias("ls")]
    public class TraverseFoldersCommand : Command, IExecutable
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public TraverseFoldersCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 1)
            {
                this.inputOutputManager.TraverseDirectory(0);
            }
            else if (this.Data.Length == 2)
            {
                int depth = 0;
                if (int.TryParse(this.Data[1], out depth))
                {
                    this.inputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    throw new ArgumentException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
