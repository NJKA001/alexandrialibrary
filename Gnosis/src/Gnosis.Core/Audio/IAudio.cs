using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Audio
{
    public interface IAudio
        : IMedia
    {
        void Load();
    }
}
