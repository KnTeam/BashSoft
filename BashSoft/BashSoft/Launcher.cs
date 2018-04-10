namespace BashSoft
{
    using SimpleJudge;
    using BashSoft.Contracts.IO;

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

            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}