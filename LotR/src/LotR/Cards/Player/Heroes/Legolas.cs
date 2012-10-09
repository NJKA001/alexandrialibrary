using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Games;
using LotR.Games.Phases;
using LotR.Games.Phases.Combat;

namespace LotR.Cards.Player.Heroes
{
    public class Legolas
        : HeroCardBase
    {
        public Legolas()
            : base("Legolas", CardSet.Core, 5, Sphere.Tactics, 9, 1, 3, 1, 4)
        {
            AddTrait(Trait.Noble);
            AddTrait(Trait.Silvan);
            AddTrait(Trait.Warrior);

            AddEffect(new RangedAbility(this));
            AddEffect(new AddTwoProgressTokensAfterDefeatingAnEnemy(this));
        }

        public class AddTwoProgressTokensAfterDefeatingAnEnemy
            : ResponseCharacterAbilityBase, IAfterEnemyDefeated
        {
            public AddTwoProgressTokensAfterDefeatingAnEnemy(Legolas source)
                : base("After Legolas participates in an attack that destroys an enemy, place 2 progress tokens on the current quest.", source)
            {
            }

            public void AfterEnemyDefeated(IEnemyDefeatedStep step)
            {
                var attachment = step.GetCardInPlay(Source.Id) as IAttachmentInPlay;
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var hero = attachment.AttachedTo as IHeroCard;
                if (hero == null)
                    return;

                if (step.Attackers.Any(x => x.Id == hero.Id))
                {
                    step.AddEffect(this);
                }
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                step.AddProgressToCurrentQuest(2);
            }
        }
    }
}
