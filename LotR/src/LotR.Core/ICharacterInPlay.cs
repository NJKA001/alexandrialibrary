﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICharacterInPlay
        : ICardInPlay, IExhaustableCard, IDamageableInPlay
    {
        new ICharacterCard Card { get; }
    }
}
