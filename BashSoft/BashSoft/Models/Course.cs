using System.Collections.Generic;

namespace BashSoft.Models
{
    public class Course
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        private string _name;
        private Dictionary<string, Student> studentsByName;

        public Course(string name)
        {
            this.Name = name;
            this.StudentsByName = new Dictionary<string, Student>();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Dictionary<string, Student> StudentsByName
        {
            get { return studentsByName; }
            set { studentsByName = value; }
        }

        public void EnrollStudent(Student student)
        {
            if (this.StudentsByName.ContainsKey(student.UserName))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, student.UserName, this.Name));
                return;
            }

            this.StudentsByName.Add(student.UserName, student);
        }
    }
}
