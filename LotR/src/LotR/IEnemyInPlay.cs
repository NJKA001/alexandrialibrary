using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IEnemyInPlay
        : ICardInPlay, IDamageableInPlay
    {
        new IEnemyCard Card { get; }
    }
}
