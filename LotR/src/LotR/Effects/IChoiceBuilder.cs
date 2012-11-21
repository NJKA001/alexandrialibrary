using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public interface IChoiceBuilder
    {
        IChoiceBuilder Question(string text);
        IChoiceBuilder Question(string text, uint minimumChosenAnswers, uint maximumChosenAnswers);

        IChoiceBuilder Answer<TItem>(string text, TItem item);
        IChoiceBuilder Answer<TItem>(string text, TItem item, Action<IGame, IEffectHandle, TItem> executeFunction);
        IChoiceBuilder Answers<TItem>(IEnumerable<TItem> items, Func<TItem, string> getText, Action<IGame, IEffectHandle, TItem> executeFunction);

        IChoiceBuilder LastAnswer<TItem>(string text, TItem item);
        IChoiceBuilder LastAnswer<TItem>(string text, TItem item, Action<IGame, IEffectHandle, TItem> executeFunction);
        IChoiceBuilder LastAnswers<TItem>(IEnumerable<TItem> items, Func<TItem, string> getText, Action<IGame, IEffectHandle, TItem> executeFunction);

        IChoice ToChoice();
    }
}
