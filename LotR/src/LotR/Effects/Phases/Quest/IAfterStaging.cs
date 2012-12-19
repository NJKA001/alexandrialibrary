﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Quest;

namespace LotR.Effects.Phases.Quest
{
    public interface IAfterStaging
        : IEffect
    {
        void AfterStaging(IQuestOutcome state);
    }
}
