﻿using System;
using System.Collections.Generic;
using BashSoft.Exceptions;

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
            this.studentsByName = new Dictionary<string, Student>();
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

        public IReadOnlyDictionary<string, Student> StudentsByName => studentsByName;

        /// <summary>
        /// Enrolling the current student in a certain course
        /// </summary>
        /// <param name="student">Given student</param>
        public void EnrollStudent(Student student)
        {
            if (this.StudentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName, this.Name);
            }

            this.studentsByName.Add(student.UserName, student);
        }
    }
}
