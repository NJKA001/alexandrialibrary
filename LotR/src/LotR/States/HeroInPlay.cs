using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.States
{
    public class HeroInPlay
        : PlayerCardInPlay<IHeroCard>, IHeroInPlay, ICharacterInPlay, IAttachmentHostInPlay
    {
        public HeroInPlay(IGame game, IHeroCard card, IPlayer owner)
            : base(game, card, owner)
        {
        }

        ICharacterCard ICardInPlay<ICharacterCard>.Card
        {
            get { return Card as ICharacterCard; }
        }

        IHeroCard ICardInPlay<IHeroCard>.Card
        {
            get { return Card as IHeroCard; }
        }

        IAttachmentHostCard ICardInPlay<IAttachmentHostCard>.Card
        {
            get { return Card as IAttachmentHostCard; }
        }

        public IEnumerable<IAttachableInPlay> Attachments
        {
            get { return GetStates<IAttachableInPlay>(); }
        }

        public void AddAttachment(IAttachableInPlay attachment)
        {
            AddState(attachment);
        }

        public void RemoveAttachment(IAttachableInPlay attachment)
        {
            AddState(attachment);
        }
    }
}
