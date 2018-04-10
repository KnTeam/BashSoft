namespace BashSoft.Exceptions
{
    using System;

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
