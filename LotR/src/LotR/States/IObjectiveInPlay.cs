using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Objectives;

namespace LotR.States
{
    public interface IObjectiveInPlay
        : ICardInPlay<IObjectiveCard>
    {
    }
}
