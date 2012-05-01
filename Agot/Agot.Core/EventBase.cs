using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public abstract class EventBase
        : CardBase, IEvent
    {
        protected EventBase(string title, CardSet set)
            : base(title, CardType.Event, set)
        {
        }
    }
}
