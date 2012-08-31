using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IKillableCard
        : ICard
    {
        byte Attack { get; }
        byte Defense { get; }
        byte HitPoints { get; }
    }
}
