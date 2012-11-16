﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IAnswer
        : IChoiceItem
    {
        Type ItemType { get; }
        IQuestion FollowUp { get; }

        bool IsChosen { get; set; }

        T GetItem<T>() where T : class;

        void Execute(IGame game);
    }
}