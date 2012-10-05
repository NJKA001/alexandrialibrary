using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IAllyInPlay
        : IExhaustableCard
    {
        new IAllyCard Card { get; }
    }
}
