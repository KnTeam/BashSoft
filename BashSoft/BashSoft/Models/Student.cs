using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft.Models
{
    public class Student
    {
        private string userName;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public Student(string userName)
        {
            this.UserName = userName;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get => userName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(this.userName), ExceptionMessages.NullOrEmptyValue);
                }

                userName = value;
            }
        }

        public IReadOnlyDictionary<string, Course> EnrolledCourses => enrolledCourses;

        public IReadOnlyDictionary<string, double> MarksByCourseName => marksByCourseName;

        /// <summary>
        /// Used for enrolling the current student in a certain course
        /// </summary>
        /// <param name="course">Course to be enrolled in</param>
        public void EnrollInCourse(Course course)
        {
            if (this.EnrolledCourses.ContainsKey(course.Name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, this.UserName, course.Name));
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        /// <summary>
        /// Used for setting the current students' average mark in a certain course
        /// </summary>
        /// <param name="course"></param>
        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.EnrolledCourses.ContainsKey(courseName))
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnrolledInCourse);
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidNumberOfScores);
            }

            this.marksByCourseName.Add(courseName, CalculateMarks(scores));
        }

        /// <summary>
        /// Helper method to calculate the average mark from all the scores 
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        private double CalculateMarks(int[] scores)
        {
            double percentageOfSolvedExams =
                scores.Sum() / (double)(Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);
            double mark = percentageOfSolvedExams * 4 + 2;
            return mark;
        }
    }
}
