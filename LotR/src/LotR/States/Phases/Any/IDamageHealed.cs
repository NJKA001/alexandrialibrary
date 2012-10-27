using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States.Phases.Any
{
    public interface IDamageHealed
        : IState
    {
        IDamagableInPlay Target { get; }

        byte Damage { get; set; }
        bool IsDamageHealed { get; set; }
    }
}
