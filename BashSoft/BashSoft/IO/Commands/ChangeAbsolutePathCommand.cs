namespace BashSoft.IO.Commands
{
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.IO;
    using BashSoft.Attributes;

    [Alias("cdabs")]
    public class ChangeAbsolutePathCommand : Command, IExecutable
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public ChangeAbsolutePathCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string absolutePath = this.Data[1];
            this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
    }
}
