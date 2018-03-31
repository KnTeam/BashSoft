using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts.Models
{
    public interface ICourse
    {
        string Name { get; }

        IReadOnlyDictionary<string, IStudent> StudentsByName { get; }

        void EnrollStudent(IStudent student);
    }
}
