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
        public ICardInPlay GetCardInPlay(Guid id)
        {
            return GetStates<ICardInPlay>().Where(x => x.StateId == id).FirstOrDefault();
        }
    }
}
