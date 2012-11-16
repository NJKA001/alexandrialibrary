using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Player.Attachments
{
    public class BladeOfGondolin
        : AttachmentCardBase
    {
        public BladeOfGondolin()
            : base("Blade of Gondolin", CardSet.Core, 39, Sphere.Tactics, 1)
        {
            IsRestricted = true;

            AddTrait(Trait.Item);
            AddTrait(Trait.Weapon);

            AddEffect(new AddOneAttackWhenAttackingAnOrc(this));
            AddEffect(new AddOneProgressTokenAfterDefeatingAnEnemy(this));
        }

        public override bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }

        public class AddOneAttackWhenAttackingAnOrc
            : PassiveCardEffectBase, IDuringDetermineAttack
        {
            public AddOneAttackWhenAttackingAnOrc(BladeOfGondolin source)
                : base("Attached hero gets +1 Attack when attacking an Orc.", source)
            {
            }

            public void DuringDetermineAttack(IDetermineAttack determineAttack)
            {
                var enemy = determineAttack.Game.GetCardInPlay<IEnemyInPlay>(determineAttack.Defender.Card.Id);
                if (enemy == null)
                    return;

                if (!enemy.HasTrait(Trait.Orc))
                    return;

                determineAttack.Attack += 1;
            }
        }

        public class AddOneProgressTokenAfterDefeatingAnEnemy
            : ResponseCardEffectBase, IAfterEnemyDefeated
        {
            public AddOneProgressTokenAfterDefeatingAnEnemy(BladeOfGondolin source)
                : base("After attached hero attacks and destroys an enemy, place 1 progress token on the current quest", source)
            {
            }

            public void AfterEnemyDefeated(IEnemyDefeated state)
            {
                var attachment = state.Game.GetCardInPlay<IAttachmentInPlay>(source.Id);
                if (attachment == null)
                    return;

                var hero = attachment.AttachedTo.Card as IHeroCard;
                if (hero == null)
                    return;

                if (!state.Attackers.Any(x => x.Card.Id == hero.Id))
                    return;

                state.Game.AddEffect(this);
            }

            public override void Resolve(IGame game, IEffectHandle handle)
            {
                game.QuestArea.AddProgress(1);

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
