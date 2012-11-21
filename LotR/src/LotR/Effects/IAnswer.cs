using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public interface IAnswer
        : IChoiceItem
    {
        Type ItemType { get; }
        IQuestion FollowUp { get; }

        bool IsChosen { get; set; }

        T GetObject<T>() where T : class;
        T GetPrimative<T>() where T : struct;

        void Execute(IGame game, IEffectHandle handle);
    }
}
