﻿using BashSoft.Exceptions;
using SimpleJudge;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.IO.Commands
{
    public abstract class Command
    {
        private Tester judge;
        private StudentsRepository repository;
        private IOManager inputOutputManager;

        private string input;
        private string[] data;

        public Command(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            this.Input = input;
            this.Data = data;
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = InputOutputManager;
        }

        protected Tester Judge
        {
            get { return this.judge; }
        }
        protected StudentsRepository Repository
        {
            get { return this.repository; }
        }
        protected IOManager InputOutputManager
        {
            get { return this.inputOutputManager; }
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