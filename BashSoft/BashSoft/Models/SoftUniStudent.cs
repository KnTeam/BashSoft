using System;
using System.Collections.Generic;
using System.Linq;
using BashSoft.Exceptions;
using BashSoft.Contracts;
using BashSoft.Contracts.Models;

namespace BashSoft.Models
{
    public class SoftUniStudent : IStudent
    {
        private string userName;
        private Dictionary<string, ICourse> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public SoftUniStudent(string userName)
        {
            this.UserName = userName;
            this.enrolledCourses = new Dictionary<string, ICourse>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get => userName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                userName = value;
            }
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses => enrolledCourses;

        public IReadOnlyDictionary<string, double> MarksByCourseName => marksByCourseName;

        /// <summary>
        /// Used for enrolling the current student in a certain course
        /// </summary>
        /// <param name="course">Course to be enrolled in</param>
        public void EnrollInCourse(ICourse course)
        {
            if (this.EnrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(this.UserName, course.Name);
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
                throw new CourseNotFoundException();
            }

            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
            {
                throw new ArgumentException(ExceptionMessages.InvalidNumberOfScores);
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
                scores.Sum() / (double)(SoftUniCourse.NumberOfTasksOnExam * SoftUniCourse.MaxScoreOnExamTask);
            double mark = percentageOfSolvedExams * 4 + 2;
            return mark;
        }
    }
}
