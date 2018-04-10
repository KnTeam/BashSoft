namespace BashSoft.Exceptions
{
    using System;

    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException()
            : base(ExceptionMessages.NotEnrolledInCourse)
        { }

        public CourseNotFoundException(string message) : base(message)
        {
        }
    }
}
