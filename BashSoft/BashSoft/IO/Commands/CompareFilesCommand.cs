namespace BashSoft.IO.Commands
{
    using SimpleJudge;
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Attributes;

    [Alias("cmp")]
    public class CompareFilesCommand : Command, IExecutable
    {
        [Inject]
        private Tester judge;

        public CompareFilesCommand(string input, string[] data)
            : base(input, data)
        { }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string firstFilePath = this.Data[1];
            string secondFilePath = this.Data[2];
            this.judge.CompareContent(firstFilePath, secondFilePath);
        }
    }
}
