﻿namespace BashSoft.IO.Commands
{
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Attributes;

    [Alias("show")]
    public class ShowCourseCommand : Command, IExecutable
    {
        [Inject]
        private StudentsRepository repository;

        public ShowCourseCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string courseName = this.Data[1];
                this.repository.GetStudentsByCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = this.Data[1];
                string userName = this.Data[2];
                this.repository.GetStudentMarkInCourse(courseName, userName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
