using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IDeck
    {
        IEnumerable<ICard> Cards { get; }

        ICard GetBottomCard();
        ICard GetTopCard();
        void RemoveBottomCard();
        void RemoveTopCard();
        void SetBottomCard();
        void SetTopCard();
        void Shuffle();
    }
}
