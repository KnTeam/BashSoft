namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using BashSoft.Contracts.Repositories;

    public class RepositoryFilter : IDataFilter
    {
        /// <summary>
        /// Filter students from a given course by given criteria
        /// </summary>
        /// <param name="wantedData"></param>
        /// <param name="wantedFilter"></param>
        /// <param name="studentsToTake"></param>
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter,
            int studentsToTake)
        {
            if (wantedFilter == "excellent")
            {
                FilterAndTake(studentsWithMarks, x => x >= 5, studentsToTake);
            }
            else if (wantedFilter == "average")
            {
                FilterAndTake(studentsWithMarks, x => x < 5.00 && x >= 3.5, studentsToTake);
            }
            else if (wantedFilter == "poor")
            {
                FilterAndTake(studentsWithMarks, x => x < 3.5, studentsToTake);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
            }
        }

        /// <summary>
        /// Method which will actually do the filtration 
        /// </summary>
        /// <param name="studentsWithMarks">Dictionary that corresponds to the students with their scores from the seeked course.</param>
        /// <param name="givenFilter">Filter to use.</param>
        /// <param name="studentsToTake">The number of students to take.</param>
        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter,
            int studentsToTake)
        {
            int counterForPrinted = 0;
            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }
                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(new KeyValuePair<string, double>(studentMark.Key, studentMark.Value));
                    counterForPrinted++;
                }
            }
        }
    }
}
