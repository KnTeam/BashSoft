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

        // TODO: Change the message to something more useful
        public const string ExceptionMessage = "The shit hit the fan!";

        //Data is already Initialized Exception
        public static string DataAlreadyInitializedException = "Data is already initialized!";

        //Data is not Initialized Exception
        public static string DataNotInitializedExceptionMessage = "The data structure must be initialized first in order to make any operations with it.";

        //The course is not existing
        public static string InexistingCourseInDataBase = "The course you are trying to get does not exist in the data base!";
    }
}