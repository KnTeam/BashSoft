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
                this.ParseCommand(input, commandName, data);
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
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryCreateDirectory(input, data);
                    break;
                case "ls":
                    TryTraverseFolders(input, data);
                    break;
                case "cmp":
                    TryCompareFiles(input, data);
                    break;
                case "cdrel":
                    TryChangePathRelatively(input, data);
                    break;
                case "cdabs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readdb":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "dropdb":
                    TryDropDb(input, data);
                    break;
                case "help":
                    TryGetHelp(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                case "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
                    break;
                case "decorder":
                    // TODO: implement after functionality is implemented
                    break;
                case "download":
                    // TODO: implement after functionality is implemented
                    break;
                case "downloadasynch":
                    // TODO: implement after functionality is implemented
                    break;
                default:
                    throw new InvalidCommandException(commandName);
            }
        }

        /// <summary>
        /// Method that displays an invalid command message. 
        /// </summary>
        /// <param name="input">The invalid input commad</param>
        private void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.DisplayException($"The command '{input}' is invalid");
        }

        /// <summary>
        /// Tries to open a file in explorer.
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, file name</param>
        private void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        /// <summary>
        /// Tries to make a directory at given place
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, folder name</param>
        private void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string folderName = data[1];
                this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        /// <summary>
        /// Tries to traverse the folders with given depth. Default depth is 0 (current folder)
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, depth (0 if not given)</param>
        private void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1)
            {
                this.inputOutputManager.TraverseDirectory(0);
            }
            else if (data.Length == 2)
            {
                int depth = 0;
                if (int.TryParse(data[1], out depth))
                {
                    this.inputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        /// <summary>
        /// Tries to compare 2 files
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, first file absolute path, second file absolute path</param>
        private void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                string firstFilePath = data[1];
                string secondFilePath = data[2];
                this.judge.CompareContent(firstFilePath, secondFilePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        /// <summary>
        /// Tries to change current path to the relative path given
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, new relative path</param>
        private void TryChangePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string relPath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        /// <summary>
        /// Tries to change current path to the absolute path given
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, new absolute path</param>
        private void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string absolutePath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        /// <summary>
        /// Tries to read the database from a file
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, input file name</param>
        private void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                this.repository.LoadData(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        /// <summary>
        /// Tries to read the database from a file
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command, input file name</param>
        private void TryDropDb(string input, string[] data)
        {
            if (data.Length != 1)
            {
                this.DisplayInvalidCommandMessage(input);
            }

            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("");
        }

        /// <summary>
        /// Lists all available commands
        /// </summary>
        /// <param name="input">Current command</param>
        /// <param name="data">Parameters collection: command</param>
        private void TryGetHelp(string input, string[] data)
        {
            if (data.Length == 1)
            {
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
                OutputWriter.WriteEmptyLine();
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string courseName = data[1];
                this.repository.GetAllStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                string courseName = data[1];
                string userName = data[2];
                this.repository.GetStudentScoresFromCourse(courseName, userName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }
        private void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string comparison = data[2].ToLower();
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, comparison, null);
                }
                else
                {
                    int studentsToTake;
                    var hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, comparison, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
            }
        }

        private void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string filter = data[2].ToLower();
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
            }
        }
    }
}
