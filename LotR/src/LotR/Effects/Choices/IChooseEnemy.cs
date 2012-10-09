using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Choices
{
    public interface IChooseEnemy
        : IChoice
    {
        IEnemyInPlay Enemy { get; set; }
    }
}
