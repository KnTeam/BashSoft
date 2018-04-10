namespace BashSoft
{
    using System.IO;
    using System;
    using SimpleJudge;
    using BashSoft.IO.Commands;
    using BashSoft.Contracts;
    using BashSoft.Contracts.IO;
    using System.Reflection;
    using System.Linq;
    using BashSoft.Attributes;
    using System.Globalization;

    /// <summary>
    /// Class respossible for interpreting user commands
    /// </summary>
    public class CommandInterpreter : IInterpreter
    {
        private Tester judge;
        private StudentsRepository repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(Tester judge, StudentsRepository repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        /// <summary>
        /// Interprets given input and executes the coresponding command. If the command or arguments are invalid, an user friendly exception message is given
        /// </summary>
        /// <param name="input">User input string with command and arguments (if any)</param>
        public void InterpretCommand(string input)
        {
            string[] data = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string commandName = data[0];

            try
            {
                IExecutable command = this.ParseCommand(input, commandName.ToLower(), data);
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

        private IExecutable ParseCommand(string input, string commandName, string[] data)
        {
            object[] constructorParameters = new object[] { input, data };

            Type commandType =
                Assembly.GetExecutingAssembly()
                .GetTypes()
                .First(type => type.GetCustomAttributes(typeof(AliasAttribute))
                .Where(att => att.Equals(commandName))
                .ToArray().Length > 0);

            Type interpreterType = typeof(CommandInterpreter);

            IExecutable command = (Command)Activator.CreateInstance(commandType, constructorParameters);

            FieldInfo[] commandFields = commandType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            FieldInfo[] interpreterFields = interpreterType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var fieldOfCommand in commandFields)
            {
                Attribute injectAttribute = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));

                if(injectAttribute != null)
                {
                    if(interpreterFields.Any(x => x.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(command,
                            interpreterFields.First(x => x.FieldType == fieldOfCommand.FieldType).GetValue(this));
                    }
                }
            }

            return command;
        }
    }
}
