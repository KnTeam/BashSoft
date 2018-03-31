namespace BashSoft.Contracts.Repositories
{
    public interface IRequester
    {
        void GetStudentMarkInCourse(string courseName, string username);

        void GetStudentsByCourse(string courseName);
    }
}
