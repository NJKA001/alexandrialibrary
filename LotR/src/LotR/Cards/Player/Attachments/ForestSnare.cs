using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;
using LotR.Games.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class ForestSnare
        : AttachmentCardBase
    {
        public ForestSnare()
            : base("Forest Snare", CardSet.Core, 69, Sphere.Lore, 3, false, false)
        {
            AddTrait(Trait.Item);
            AddTrait(Trait.Trap);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            if (cardInPlay != null && cardInPlay is IEnemyInPlay)
            {
                foreach (var player in step.Phase.Round.Game.Players)
                {
                    if (player.EngagedEnemies.Any(x => x.CardId == cardInPlay.CardId))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
