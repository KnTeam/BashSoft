using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BashSoft
{
    using System.Text;

    /// <summary>
    /// Functionality for traversing the folders and other behaviors. 
    /// </summary>
    public static class IOManager
    {
        /// <summary>
        /// Traverse the folder of the project using a queue with BFS.
        /// </summary>
        /// <param name="path">Folder path to traverse</param>
        public static void TraverseDirectory(string path)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = path.Split('\\').Length;
            Queue<string> subFoldersQueue = new Queue<string>();
            subFoldersQueue.Enqueue(path);

            while (subFoldersQueue.Count != 0)
            {
                // Dequeue the folder at the start of the queue
                string currentPath = subFoldersQueue.Dequeue();
                int identation = currentPath.Split('\\').Length - initialIdentation;

                // Print the folder path
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));

                // Add all it's subfolders to the end of the queue
                foreach (string directoryPath in Directory.GetDirectories(currentPath))
                {
                    subFoldersQueue.Enqueue(directoryPath);
                }
            }
        }
    }
}