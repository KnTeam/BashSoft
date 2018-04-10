namespace BashSoft.Contracts.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDirectoryChanger
    {
        void ChangeCurrentDirectoryRelative(string relativePath);
        void ChangeCurrentDirectoryAbsolute(string absolutePath);
    }
}
