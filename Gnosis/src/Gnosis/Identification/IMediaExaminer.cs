using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public interface IMediaExaminer
    {
        bool CanExamine(IMediaInfo info);
        IMediaInfo Examine(IMediaInfo info);

        void AddChild(IMediaExaminer child);
        IEnumerable<IMediaExaminer> Children { get; }
    }
}
