namespace BashSoft.Exceptions
{
    using System;

    public class DuplicateEntryInStructureException : Exception
    {
        public DuplicateEntryInStructureException(string message)
            : base(message)
        {
        }
        public DuplicateEntryInStructureException(string entry, string structure)
            : base(string.Format(ExceptionMessages.DuplicateEntry, entry, structure))
        {
        }

    }
}
