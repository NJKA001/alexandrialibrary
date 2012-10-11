using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Effects.Phases;
using LotR.States;

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

        public override bool CanBeAttachedTo(IGameState state, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            if (cardInPlay != null && cardInPlay is IEnemyCard)
            {
                //foreach (var player in step.Phase.Round.Game.Players)
                //{
                    //if (player.EngagedEnemies.Any(x => x.CardId == cardInPlay.CardId))
                    //{
                    //    return true;
                    //}
                //}
            }

            return false;
        }
    }
}
