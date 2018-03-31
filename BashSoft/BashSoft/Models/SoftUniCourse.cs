using System;
using System.Collections.Generic;
using BashSoft.Exceptions;
using BashSoft.Contracts;
using BashSoft.Contracts.Models;

namespace BashSoft.Models
{
    public class SoftUniCourse : ICourse
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        private string _name;
        private Dictionary<string, IStudent> studentsByName;

        public SoftUniCourse(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                _name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName => studentsByName;

        /// <summary>
        /// Enrolling the current student in a certain course
        /// </summary>
        /// <param name="student">Given student</param>
        public void EnrollStudent(IStudent student)
        {
            if (this.StudentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName, this.Name);
            }

            this.studentsByName.Add(student.UserName, student);
        }
    }
}
