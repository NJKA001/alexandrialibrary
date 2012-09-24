using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IEnemyInPlay
        : ICardInPlay
    {
        new IEnemyCard Card { get; }
    }
}
