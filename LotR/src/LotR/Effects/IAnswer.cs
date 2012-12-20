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
        IQuestion Parent { get; }
        IQuestion FollowUp { get; }

        T GetObject<T>() where T : class;
        T GetPrimative<T>() where T : struct;

        void Execute(IGame game, IEffectHandle handle);
        void SetParent(IQuestion parent);
        void SetFollowUp(IQuestion followUp);
    }
}
