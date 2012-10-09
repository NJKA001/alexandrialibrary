using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Payments
{
    public interface IChooseEnemyPayment
        : IPayment
    {
        IEnemyInPlay Enemy { get; }
    }
}
