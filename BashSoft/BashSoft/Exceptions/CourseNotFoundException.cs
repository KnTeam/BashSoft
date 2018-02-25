using System;

namespace BashSoft.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException()
            : base(ExceptionMessages.NotEnrolledInCourse)
        {
        }

        public CourseNotFoundException(string message) : base(message)
        {
        }
    }
}
