﻿namespace BashSoft
{
    using System;

    /// <summary>
    /// An interpreter that calls the functionalities. 
    /// </summary>
    public class InputReader
    {
        private const string endCommand = "quit";
        private CommandInterpreter interpreter;

        public InputReader(CommandInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        /// <summary>
        /// Starts to listen for commands and executes them if the syntax is correct. 
        /// </summary>
        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            string input = Console.ReadLine();
            input = input.Trim();

            // TODO: change with do-while ??? avoiding repetitions of code
            while (!input.Equals(endCommand))
            {
                this.interpreter.InterpredCommand(input);
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine();
                input = input.Trim();
            }
        }
    }
}
