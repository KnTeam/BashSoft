using System.IO;

namespace BashSoft
{
    using System;
    using System.Diagnostics;
    using SimpleJudge;
    using BashSoft.IO.Commands;
    using BashSoft.Exceptions;

    /// <summary>
    /// Class respossible for interpreting user commands
    /// </summary>
    public class CommandInterpreter
    {
        private Tester judge;
        private StudentsRepository repository;
        private IOManager inputOutputManager;

        public CommandInterpreter(Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        /// <summary>
        /// Interprets given input and executes the coresponding command. If the command or arguments are invalid, an user friendly exception message is given
        /// </summary>
        /// <param name="input">User input string with command and arguments (if any)</param>
        public void InterpredCommand(string input)
        {
            string[] data = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string commandName = data[0];

            try
            {
                var command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (DirectoryNotFoundException dNotFound)
            {
                OutputWriter.DisplayException(dNotFound.Message);
            }
            catch (ArgumentOutOfRangeException aOutRange)
            {
                OutputWriter.DisplayException(aOutRange.Message);
            }
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private Command ParseCommand(string input, string commandName, string[] data)
        {
            switch (commandName.ToLower())
            {
                case "open":
                    return new OpenFileCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "mkdir":
                    return new MakeDirectoryCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "ls":
                    return new TraverseFoldersCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cmp":
                    return new CompareFilesCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cdrel":
                    return new ChangeRelativePathCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cdabs":
                    return new ChangeAbsolutePathCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "readdb":
                    return new ReadDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "dropdb":
                    return new DropDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "help":
                    return new GetHelpCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "show":
                    return new ShowCourseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "order":
                    return new PrintOrderedStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                default:
                    throw new InvalidCommandException(commandName);
            }
        }
    }
}
