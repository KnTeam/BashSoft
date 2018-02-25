using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BashSoft
{
    public class StudentsRepository
    {
        //Dictionary<Course name, Dictionary<Username, Grades>>
        private Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;
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
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            this.studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
            this.isDataInitialized = false;
        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
                return;                
            }

            OutputWriter.WriteMessageOnNewLine("Reading data...");
            studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
            ReadData(fileName);
        }

        public void ReadData(string fileName)
        {
            string path = $"{SessionData.currentPath}\\{fileName}";
            if (File.Exists(path))
            {
                string pattern = @"(?<Course>[A-Z][a-z#+A-Z]*_[A-Z][a-z]{2}_[0-9]{4})\s+(?<Student>[A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(?<Mark>\d+)";

                Regex rgx = new Regex(pattern);

                string[] allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrWhiteSpace(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);

                        string courseName = currentMatch.Groups["Course"].Value;
                        string userName = currentMatch.Groups["Student"].Value;
                        int studentScoreOnTask;
                        bool hasParsedScore = int.TryParse(currentMatch.Groups["Mark"].Value, out studentScoreOnTask);

                        if (hasParsedScore && studentScoreOnTask >= 0 && studentScoreOnTask <= 100)
                        {
                            if (!studentsByCourse.ContainsKey(courseName))
                            {
                                studentsByCourse[courseName] = new Dictionary<string, List<int>>();
                            }

                            if (!studentsByCourse[courseName].ContainsKey(userName))
                            {
                                studentsByCourse[courseName][userName] = new List<int>();
                            }
                        }

                        studentsByCourse[courseName][userName].Add(studentScoreOnTask);
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
                if (studentsByCourse.ContainsKey(courseName))
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
            if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentUserName))
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
                OutputWriter.PrintStudent(new KeyValuePair<string, List<int>>(username, studentsByCourse[courseName][username]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");

                foreach (var studentMarkEntry in studentsByCourse[courseName])
                {
                    OutputWriter.PrintStudent(studentMarkEntry);
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
                    studentsToTake = studentsByCourse[courseName].Count;
                }

                this.filter.FilterAndTake(studentsByCourse[courseName], givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = studentsByCourse[courseName].Count;
                }
                
                this.sorter.OrderAndTake(studentsByCourse[courseName], comparison, studentsToTake.Value);
            }
        }
    }
}
