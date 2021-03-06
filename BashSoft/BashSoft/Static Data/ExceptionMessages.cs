﻿namespace BashSoft
{
    /// <summary>
    ///     Different constant messages to display in the whole project to the user.
    /// </summary>
    public static class ExceptionMessages
    {
        /// <summary>
        /// Invalid path message
        /// </summary>
        public static string InvalidPath = "The folder/file you are trying to access at the current address, does not exist.";

        /// <summary>
        /// Invalid path message
        /// </summary>
        public static string UnauthorizedAccessExceptionMessage = "The folder/file you are trying to get access needs a higher level of rights than you currently have.";

        /// <summary>
        /// Invalid path message
        /// </summary>
        public static string ForbiddenSymbolsContainedInName = "The given name contains symbols that are not allowed to be used in names of files and folders.";

        /// <summary>
        /// Accessing folder higher than the root folder message
        /// </summary>
        public static string UnableToGoHigherInPartitionHierarchy = "Can not go higher than the root folder of the current partition.";

        /// <summary>
        /// Invalid number message.
        /// </summary>
        public static string UnableToParseNumber = @"The sequence you've written is not a valid number.";

        /// <summary>
        /// Data is already Initialized Exception
        /// </summary>
        public static string DataAlreadyInitializedException = "Data is already initialized!";

        /// <summary>
        /// Data is not Initialized Exception
        /// </summary>
        public static string DataNotInitializedExceptionMessage = "The data structure must be initialized first in order to make any operations with it.";

        /// <summary>
        /// The course is not existing
        /// </summary>
        public static string InexistingCourseInDataBase = "The course you are trying to get does not exist in the data base!";

        /// <summary>
        /// Comparison between files with different size
        /// </summary>
        public static string ComparisonOfFilesWithDifferentSizes = "Files not of equal size, certain mismatch.";

        /// <summary>
        /// Given word does not match any of the categories
        /// </summary>
        public static string InvalidStudentFilter = "The given filter is not one of the following: excellent/average/poor";

        /// <summary>
        /// Comparison query is not valid
        /// </summary>
        /// <summary>
        /// Given score is not between 0 and 100
        /// </summary>
        public static string InvalidScore = "The number for the score you've entered is not in the range of 0 - 100";

        /// <summary>
        /// Given comparison is not valid
        /// </summary>
        public static string InvalidComparisonQuery = "The comparison query you want, does not exist in the context of the current program!";

        /// <summary>
        /// Given quantity is not in the proper format
        /// </summary>
        public static string InvalidTakeQuantityParameter = "The take command expected does not match the format wanted!";

        /// <summary>
        /// The given "Take" command is not valid
        /// </summary>
        public static string InvalidTakeCommand = "The command is not a valid take command!";

        /// <summary>
        /// Current student is in a certain course. 
        /// </summary>
        public static string DuplicateEntry = "The {0} already exists in {1}.";

        /// <summary>
        /// Current student is not in this course
        /// </summary>
        public static string NotEnrolledInCourse = "Student must be enrolled in a course before you set his mark.";

        /// <summary>
        /// The number of scores for the given course is greater than the possible.
        /// </summary>
        public static string InvalidNumberOfScores = "The number of scores for the given course is greater than the possible.";

        /// <summary>
        /// A null or empty value given exception
        /// </summary>
        public static string NullOrEmptyValue = "The value of the variable CANNOT be null or empty!";

        public static string DisplayInvalidCommandMessage = "The command {0} is invalid";
    }
}