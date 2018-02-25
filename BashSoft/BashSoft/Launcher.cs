using SimpleJudge;

namespace BashSoft
{
    using System;

    /// <summary>
    ///     Entry point for the project
    /// </summary>
    public static class Launcher
    {
        /// <summary>
        ///     Program entry point
        /// </summary>
        public static void Main()
        {
            var tester = new Tester();
            var ioManager = new IOManager();
            var repo = new StudentsRepository(new RepositorySorter(), new RepositoryFilter());

            var currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            var reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}