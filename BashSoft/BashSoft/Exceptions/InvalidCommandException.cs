namespace BashSoft.Exceptions
{
    using System;

    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string entry) : base(string.Format(ExceptionMessages.DisplayInvalidCommandMessage, entry))
        {
        }
    }
}
