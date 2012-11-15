using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseAttachmentHost
        : ChoiceBase, IChooseAttachmentHost
    {
        public ChooseAttachmentHost(ISource source, IPlayer player, IAttachableCard attachment)
            : base(GetDescription(attachment), source, player)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            this.Attachment = attachment;
        }

        private static string GetDescription(IAttachableCard attachment)
        {
            return string.Format("Choose which card you want to attach '{0}' to", attachment.Title);
        }

        private IAttachmentHostInPlay chosenAttachmentHost;

        public IAttachableCard Attachment
        {
            get;
            private set;
        }

        public IAttachmentHostInPlay ChosenAttachmentHost
        {
            get { return chosenAttachmentHost; }
            set
            {
                if (chosenAttachmentHost == value)
                    return;

                chosenAttachmentHost = value;
                OnPropertyChanged("ChosenAttachmentHost");
            }
        }

        public override bool IsValid(IGame game)
        {
            if (ChosenAttachmentHost == null)
                return false;

            if (!ChosenAttachmentHost.Card.IsValidAttachment(Attachment))
                return false;

            if (!Attachment.CanBeAttachedTo(game, ChosenAttachmentHost.Card))
                return false;

            return true;
        }
    }
}
