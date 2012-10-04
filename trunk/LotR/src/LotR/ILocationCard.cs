﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface ILocationCard
        : IEncounterCard, IProgressableCard, IThreateningCard
    {
        ICardEffect Travel { get; }
    }
}