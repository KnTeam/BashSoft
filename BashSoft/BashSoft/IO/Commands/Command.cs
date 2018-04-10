namespace BashSoft.IO.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using System;

    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;

        public Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
        }

        protected string[] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                if(value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                this.data = value;
            }
        }
        protected string Input
        {
            get
            {
                return this.input;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.input = value;
            }
        }

        public abstract void Execute();
    }
}
