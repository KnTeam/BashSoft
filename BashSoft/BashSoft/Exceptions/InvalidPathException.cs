using System;

namespace BashSoft.Exceptions
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException()
            : base(ExceptionMessages.InvalidPath)
        {
        }

        public InvalidPathException(string message) : base(message)
        {
        }
    }
}
