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
            IOManager.CreateDirectoryInCurrentFolder("gosho");
            IOManager.TraverseDirectory(0);
            Console.WriteLine("Press any key to be happy!");
            Console.ReadKey();
        }
    }
}