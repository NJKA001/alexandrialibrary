using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IPile
    {
        IEnumerable<ICard> Cards { get; }

        void AddCard(ICard card);
        void RemoveCard(ICard card);
    }
}
