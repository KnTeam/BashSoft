namespace BashSoft.IO.Commands
{
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Attributes;

    [Alias("readdb")]
    public class ReadDatabaseCommand : Command, IExecutable
    {
        [Inject]
        private StudentsRepository repository;

        public ReadDatabaseCommand(string input, string[] data)
            : base(input, data)
        { }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileName = this.Data[1];
            this.repository.LoadData(fileName);
        }
    }
}
