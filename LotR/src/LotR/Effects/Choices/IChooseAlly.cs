using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseAlly
        : IChoice
    {
        ICardInPlay<IAllyCard> Ally { get; set; }
    }
}
