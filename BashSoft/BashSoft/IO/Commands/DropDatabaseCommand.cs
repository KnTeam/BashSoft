namespace BashSoft.IO.Commands
{
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Attributes;

    [Alias("dropdb")]
    public class DropDatabaseCommand :  Command, IExecutable
    {
        [Inject]
        private StudentsRepository repository;

        public DropDatabaseCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("");
        }
    }
}
