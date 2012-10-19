using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;

namespace LotR.States
{
    public class AllyInPlay
        : CharacterInPlay<IAllyCard>, IAllyInPlay, ICharacterInPlay, IAttachmentHostInPlay
    {
        public AllyInPlay(IGame game, IAllyCard card, IPlayer owner)
            : base(game, card, owner)
        {
        }
    }
}
