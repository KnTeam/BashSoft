namespace BashSoft.IO.Commands
{
    using SimpleJudge;
    using Exceptions;

    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        { }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string firstFilePath = this.Data[1];
            string secondFilePath = this.Data[2];
            this.Judge.CompareContent(firstFilePath, secondFilePath);
        }
    }
}
