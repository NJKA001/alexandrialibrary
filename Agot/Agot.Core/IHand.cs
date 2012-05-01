using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IHand
    {
        IEnumerable<ICardInHand> Cards { get; }

        void AddCard(ICardInHand card);
        void RemoveCard(ICardInHand card);
    }
}
