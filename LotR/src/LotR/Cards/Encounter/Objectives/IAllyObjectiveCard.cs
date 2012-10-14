using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;

namespace LotR.Cards.Encounter.Objectives
{
    public interface IAllyObjectiveCard
        : IObjectiveCard, IAllyCard
    {
    }
}
