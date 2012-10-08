using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IPlayerLoader
    {
        IEnumerable<IPlayerCard> PlayerCards { get; }

        IPlayer Load(string path);
    }
}
