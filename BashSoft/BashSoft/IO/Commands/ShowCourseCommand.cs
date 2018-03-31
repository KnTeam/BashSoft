namespace BashSoft.IO.Commands
{
    using SimpleJudge;
    using Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.IO;

    public class ShowCourseCommand : Command, IExecutable
    {
        public ShowCourseCommand(string input, string[] data, Tester judge, StudentsRepository repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string courseName = this.Data[1];
                this.Repository.GetStudentsByCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = this.Data[1];
                string userName = this.Data[2];
                this.Repository.GetStudentMarkInCourse(courseName, userName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
