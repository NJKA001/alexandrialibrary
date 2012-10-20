using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public abstract class AreaBase
        : StateBase, IArea
    {
        protected AreaBase(IGame game)
            : base(game)
        {
        }
    }
}
