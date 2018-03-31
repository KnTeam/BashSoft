using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts.IO
{
    public interface IDirectoryManager : IDirectoryChanger, IDirectoryCreator, IDirectoryTraverser
    {
    }
}
