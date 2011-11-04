using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IText
        : IMedia
    {
        string Body { get; }

        void Load();
    }
}
