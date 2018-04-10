namespace BashSoft.IO.Commands
{
    using System;
    using SimpleJudge;
    using BashSoft.Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.IO;
    using BashSoft.Attributes;

    [Alias("order")]
    public class PrintOrderedStudentsCommand : Command, IExecutable
    {
        [Inject]
        private StudentsRepository repository;

        public PrintOrderedStudentsCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 5)
            {
                throw new InvalidCommandException(this.Input);
            }
            string courseName = this.Data[1];
            string comparison = this.Data[2].ToLower();
            string takeCommand = this.Data[3].ToLower();
            string takeQuantity = this.Data[4].ToLower();

            this.TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
        }
        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, comparison, null);
                }
                else
                {
                    int studentsToTake;
                    var hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, comparison, studentsToTake);
                    }
                    else
                    {
                        throw new ArgumentException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
