namespace BashSoft.Contracts.Repositories
{
    using BashSoft.Contracts.Models;
    using System.Collections.Generic;

    public interface IRequester
    {
        void GetStudentMarkInCourse(string courseName, string username);

        void GetStudentsByCourse(string courseName);

        ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp);

        ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp);
    }
}
