using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts.IO
{
    public interface IDirectoryCreator
    {
        void CreateDirectoryInCurrentFolder(string name);
    }
}
