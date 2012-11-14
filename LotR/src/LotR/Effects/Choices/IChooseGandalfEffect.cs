using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseGandalfEffect
        : IChoice
    {
        IEnumerable<IEnemyInPlay> EnemiesToChoose { get; }

        IEnemyInPlay EnemyToDamage { get; set; }
        bool DrawCards { get; set; }
        bool ReduceYourThreat { get; set; }
    }
}
