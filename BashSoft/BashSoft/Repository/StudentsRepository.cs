using BashSoft.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BashSoft
{
    public class StudentsRepository
    {
        //Dictionary<Course name, Dictionary<Username, Grades>>
        private Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;

        private bool isDataInitialized;
        private RepositoryFilter filter;
        private RepositorySorter sorter;

        public StudentsRepository(RepositorySorter sorter, RepositoryFilter filter)
        {
            this.filter = filter;
            this.sorter = sorter;
            this.studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                throw new InvalidOperationException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            this.students = null;
            this.courses = null;
            this.isDataInitialized = false;
        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                throw new InvalidOperationException(ExceptionMessages.DataAlreadyInitializedException);             
            }

            OutputWriter.WriteMessageOnNewLine("Reading data...");
            students = new Dictionary<string, Student>();
            courses = new Dictionary<string, Course>();

            this.ReadData(fileName);
        }

        public void ReadData(string fileName)
        {
            string path = $"{SessionData.currentPath}\\{fileName}";
            if (File.Exists(path))
            {
                string pattern = @"(?<Course>[A-Z][a-z#+A-Z]*_[A-Z][a-z]{2}_\d{4})\s+(?<Student>[A-Za-z]+\d{2}_\d{2,4})\s(?<Mark>[\s0-9]+)";

                Regex rgx = new Regex(pattern);

                string[] allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrWhiteSpace(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);

                        string courseName = currentMatch.Groups["Course"].Value;
                        string userName = currentMatch.Groups["Student"].Value;
                        string scoresStr = currentMatch.Groups["Mark"].Value;

                        try
                        {
                            int[] scores = scoresStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                            
                            if(scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                            }
                            if(scores.Length > Course.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.students.ContainsKey(userName))
                            {
                                this.students.Add(userName, new Student(userName));
                            }
                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new Course(courseName));
                            }

                            Course course = this.courses[courseName];
                            Student student = this.students[userName];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, scores);

                            course.EnrollStudent(student);                            
                        }
                        catch (FormatException fex)
                        {
                            OutputWriter.DisplayException(fex.Message + $"at line : {line}");
                        }
                    }
                }

                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        public bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                }
                return false;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            return false;
        }

        public bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (this.IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentsByName.ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
            }
            return false;
        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, this.courses[courseName].StudentsByName[username].MarksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");

                foreach (var studentMarkEntry in this.courses[courseName].StudentsByName)
                {
                    this.GetStudentScoresFromCourse(courseName, studentMarkEntry.Key);
                }
            }
        }

        /// <summary>
        /// Bediator between the command interpreter and the filters/sorters. 
        /// </summary>
        /// <param name="courseName">Course name</param>
        /// <param name="givenFilter">Filter: excellent/average/poor</param>
        /// <param name="studentsToTake">Number of students to take (nullable)</param>
        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }
                Dictionary<string, double> marks = this.courses[courseName].StudentsByName.ToDictionary(x => x.Key, y => y.Value.MarksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }
                Dictionary<string, double> marks = this.courses[courseName].StudentsByName.ToDictionary(x => x.Key, y => y.Value.MarksByCourseName[courseName]);
                
                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }
    }
}
