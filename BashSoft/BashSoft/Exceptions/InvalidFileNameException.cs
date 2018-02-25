using System;

namespace BashSoft.Exceptions
{
    public class InvalidFileNameException : Exception
    {
        public InvalidFileNameException()
            : base(ExceptionMessages.ForbiddenSymbolsContainedInName)
        {
        }

        public InvalidFileNameException(string message) : base(message)
        {
        }
    }
}
