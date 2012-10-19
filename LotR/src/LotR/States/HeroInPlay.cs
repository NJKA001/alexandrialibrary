using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;
using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public class HeroInPlay
        : PlayerCardInPlay<IHeroCard>, IHeroInPlay, ICharacterInPlay, IAttachmentHostInPlay
    {
        public HeroInPlay(IGame game, IHeroCard card, IPlayer owner)
            : base(game, card, owner)
        {
        }

        private readonly IDictionary<Guid, IAttachableInPlay> attachments = new Dictionary<Guid, IAttachableInPlay>();
        private byte resources;

        ICharacterCard ICardInPlay<ICharacterCard>.Card
        {
            get { return Card as ICharacterCard; }
        }

        IHeroCard ICardInPlay<IHeroCard>.Card
        {
            get { return Card as IHeroCard; }
        }

        IResourcefulCard ICardInPlay<IResourcefulCard>.Card
        {
            get { return Card as IResourcefulCard; }
        }

        IAttachmentHostCard ICardInPlay<IAttachmentHostCard>.Card
        {
            get { return Card as IAttachmentHostCard; }
        }

        public IEnumerable<IAttachableInPlay> Attachments
        {
            get { return attachments.Values; }
        }

        public byte Resources
        {
            get { return resources; }
            set
            {
                if (resources != value)
                {
                    resources = value;
                    OnPropertyChanged("Resources");
                }
            }
        }

        public void AddAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (attachments.ContainsKey(attachment.StateId))
                return;

            attachments.Add(attachment.StateId, attachment);
        }

        public void RemoveAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (!attachments.ContainsKey(attachment.StateId))
                return;

            attachments.Remove(attachment.StateId);
        }

        public bool HasResourceIcon(ICostlyCard costlyCard, Sphere sphere)
        {
            if (Card.PrintedSphere == sphere)
                return true;

            var check = new CheckForResourceIcon(game, costlyCard, this, sphere);

            foreach (var attachment in attachments.Values.Where(x => x.HasEffect<IDuringCheckForResourceIcon>()))
            {
                foreach (var effect in attachment.Card.Text.Effects.OfType<IDuringCheckForResourceIcon>())
                {
                    effect.DuringCheckForResourceIcon(check);
                }
            }

            return check.HasResourceIcon;
        }

        public override bool HasTrait(Trait trait)
        {
            if (base.HasTrait(trait))
                return true;

            var check = new CheckForTrait(game, this, trait);

            foreach (var attachment in attachments.Values.Where(x => x.Card.HasEffect<IDuringCheckForTrait>()))
            {
                foreach (var effect in attachment.Card.Text.Effects.OfType<IDuringCheckForTrait>())
                {
                    effect.DuringCheckForTrait(check);
                }
            }

            return check.HasTrait;
        }
    }
}
