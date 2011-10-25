using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IAudio
        : IMedia
    {
        void Load();
    }
}
