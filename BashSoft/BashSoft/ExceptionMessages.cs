namespace BashSoft
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

        //Data is already Initialized Exception
        public static string DataAlreadyInitializedException = "Data is already initialized!";

        //Data is not Initialized Exception
        public static string DataNotInitializedExceptionMessage = "The data structure must be initialized first in order to make any operations with it.";

        //The course is not existing
        public static string InexistingCourseInDataBase = "The course you are trying to get does not exist in the data base!";

        //Comparison between files with different size
        public static string ComparisonOfFilesWithDifferentSizes = "Files not of equal size, certain mismatch.";
    }
}