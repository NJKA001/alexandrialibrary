using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IArtist
        : IEntity
    {
        ArtistType Type { get; set; }
        string Name { get; set; }
        short Year { get; set; }

        IEnumerable<IWork> Works { get; }

        void AddWork(IWork work);
        void RemoveWork(IWork work);
    }
}
