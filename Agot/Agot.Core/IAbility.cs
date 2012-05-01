using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IAbility
    {
        IEnumerable<ICost> Costs { get; }
        AbilityType Type { get; }
        bool IsLimited { get; }
        bool IsImmunity { get; }
        string Text { get; }
    }
}
