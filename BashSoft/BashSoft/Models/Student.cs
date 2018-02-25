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
            this.EnrolledCourses = new Dictionary<string, Course>();
            this.MarksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get => userName;
            set { userName = value; }
        }

        public Dictionary<string, Course> EnrolledCourses
        {
            get => enrolledCourses;
            set { enrolledCourses = value; }
        }

        public Dictionary<string, double> MarksByCourseName
        {
            get => marksByCourseName;
            set { marksByCourseName = value; }
        }

        /// <summary>
        /// Used for enrolling the current student in a certain course
        /// </summary>
        /// <param name="course">Course to be enrolled in</param>
        public void EnrollInCourse(Course course)
        {
            if (this.EnrolledCourses.ContainsKey(course.Name))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, this.UserName, course.Name));
                return;
            }

            this.EnrolledCourses.Add(course.Name, course);
        }

        /// <summary>
        /// Used for setting the current students' average mark in a certain course
        /// </summary>
        /// <param name="course"></param>
        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!this.EnrolledCourses.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.NotEnrolledInCourse);
                return;
            }

            if (scores.Length > EnrolledCourses.Count)
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
            }

            this.MarksByCourseName.Add(courseName, CalculateMarks(scores));
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
