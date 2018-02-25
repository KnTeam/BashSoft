using System;

namespace BashSoft.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string entry) : base(string.Format(ExceptionMessages.DisplayInvalidCommandMessage, entry))
        {
        }
    }
}
