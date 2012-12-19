using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases.Quest;
using LotR.States;
using LotR.States.Phases.Quest;

namespace LotR.Cards.Player.Attachments
{
    public class LightOfValinor
        : AttachmentCardBase
    {
        public LightOfValinor()
            : base("Light of Valinor", CardSet.Dw, 107, Sphere.Spirit, 1)
        {
            AddEffect(new AttachedHeroDoesNotExhaustToQuest(this));
        }

        public override bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            if (!cardInPlay.IsValidAttachment(this))
                return false;

            var hero = cardInPlay as IHeroInPlay;
            if (hero == null)
                return false;

            if (!hero.HasTrait(Trait.Noble) && !hero.HasTrait(Trait.Silvan))
                return false;

            return true;
        }

        private class AttachedHeroDoesNotExhaustToQuest
            : PassiveCardEffectBase, IDuringCommittingToQuest
        {
            public AttachedHeroDoesNotExhaustToQuest(LightOfValinor source)
                : base("Attach to a Noldor or Silvan Hero. Attached hero does not exhaust to commit to a quest.", source)
            {
            }

            public void DuringCommittingToQuest(IGame game)
            {
                var attachmentInPlay = game.GetCardInPlay<IAttachmentInPlay>(CardSource.Id);
                if (attachmentInPlay == null || attachmentInPlay.AttachedTo == null)
                    return;

                var character = attachmentInPlay.AttachedTo as IWillpowerfulInPlay;
                if (character == null)
                    return;

                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return;

                if (!questPhase.IsCommittedToQuest(character.Card.Id))
                    return;

                questPhase.CharacterDoesNotExhaustToQuest(character.Card.Id);
            }
        }
    }
}
