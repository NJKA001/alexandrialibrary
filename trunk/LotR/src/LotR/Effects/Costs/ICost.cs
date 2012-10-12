﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Payments;

namespace LotR.Effects.Costs
{
    public interface ICost
    {
        ISource Source { get; }
        string Description { get; }

        bool IsMetBy(IPayment payment);
    }
}