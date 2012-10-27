using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IDetermineHitPoints
        : IState
    {
        IDamagableInPlay Damageable { get; }

        byte HitPoints { get; set; }
    }
}
