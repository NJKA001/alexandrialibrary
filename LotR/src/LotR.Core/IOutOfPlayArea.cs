﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IOutOfPlayArea
    {
        IEnumerable<ICard> Cards { get; }

        void RemoveCardFromPlay(ICard card);
        void ReturnCardToPlay(ICard card);
    }
}
