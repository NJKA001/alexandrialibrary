using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Effects.Phases.Resource
{
    public interface IBeforeDrawingResourceCards
    {
        void BeforeDrawingResourceCards(IPlayersDrawingCards state);
    }
}
