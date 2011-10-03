using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaSchema
    {
        Uri Identifier { get; }
        string Name { get; }
        string Description { get; }

        IMediaSchema Parent { get; }
        IEnumerable<IMediaSchema> Children { get; }

        IMediaSchema GetChild(string name);
        bool Validate(Uri location);
    }
}
