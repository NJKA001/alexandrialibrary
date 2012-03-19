using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IWork
        : IEntity
    {
        WorkType Type { get; set; }
        IWork Parent { get; set; }
        IArtist Artist { get; set; }
        string Name { get; set; }
        short Year { get; set; }
        uint Number { get; set; }

        IEnumerable<IWork> Children { get; }

        void AddChild(IWork work);
        void RemoveChild(IWork work);
    }
}
