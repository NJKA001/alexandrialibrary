using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Cards.Player.Events
{
    public class SneakAttack
        : EventCardBase
    {
        public SneakAttack()
            : base("Sneak Attack", CardSet.Core, 23, Sphere.Leadership, 1)
        {
            AddEffect(new PutOneAllyIntoPlay(this));
        }

        private class PutOneAllyIntoPlay
            : ActionCardEffectBase, IPlayerActionEffect
        {
            public PutOneAllyIntoPlay(SneakAttack source)
                : base("Put 1 ally card into play from your hand. At the end of the phase, if that ally is still in play, return it to your hand.", source)
            {
            }

            public bool CanBePlayed(IGame game)
            {
                switch (game.CurrentPhase.StepCode)
                {
                    case PhaseStep.Combat_Player_Actions_Before_Chosing_An_Attacking_Enemy:
                    case PhaseStep.Combat_Player_Actions_Before_Declaring_Defenders:
                    case PhaseStep.Combat_Player_Actions_Before_Declaring_Target_Enemy:
                    case PhaseStep.Combat_Player_Actions_Before_Determine_Enemy_Combat_Damage:
                    case PhaseStep.Combat_Player_Actions_Before_Determining_Character_Attack_Strength:
                    case PhaseStep.Combat_Player_Actions_Before_Determining_Character_Combat_Damage:
                    case PhaseStep.Combat_Player_Actions_Before_End:
                    case PhaseStep.Combat_Player_Actions_Before_Resolve_Shadow_Effects:
                    case PhaseStep.Encounter_Player_Actions_Before_End:
                    case PhaseStep.Encounter_Player_Actions_Before_Engagement_Checks:
                    case PhaseStep.Encounter_Player_Actions_Before_Optional_Engagement:
                    case PhaseStep.Planning_Play_Allies_and_Attachments:
                    case PhaseStep.Planning_Player_Actions_Before_End:
                    case PhaseStep.Quest_Player_Actions_Before_Commit_Characters:
                    case PhaseStep.Quest_Player_Actions_Before_End:
                    case PhaseStep.Quest_Player_Actions_Before_Quest_Resolution:
                    case PhaseStep.Quest_Player_Actions_Before_Staging:
                    case PhaseStep.Refresh_Player_Actions_Before_End:
                    case PhaseStep.Resource_Player_Actions_Before_End:
                    case PhaseStep.Travel_Player_Actions_Before_End:
                    case PhaseStep.Travel_Player_Actions_Before_Traveling:
                        return true;
                    default:
                        return false;
                }
            }

            public override IChoice GetChoice(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return null;

                var alliesInHand = controller.Hand.Cards.OfType<IAllyCard>().ToList();

                return new ChooseCardInHand<IAllyCard>("Choose 1 ally card in your hand", Source, controller, alliesInHand);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var chooseAlly = choice as IChooseCardInHand<IAllyCard>;
                if (chooseAlly == null || chooseAlly.ChosenCard == null)
                    return;

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return;

                controller.AddCardInPlay(new AllyInPlay(game, chooseAlly.ChosenCard));
                game.AddEffect(new AtEndOfPhaseReturnAllyToYourHand(CardSource, chooseAlly.ChosenCard.Id));
            }
        }

        private class AtEndOfPhaseReturnAllyToYourHand
            : PassiveCardEffectBase
        {
            public AtEndOfPhaseReturnAllyToYourHand(ICard cardSource, Guid allyId)
                : base("At the end of the phase, if that ally is still in play, return it to your hand.", cardSource)
            {
                this.allyId = allyId;
            }

            private readonly Guid allyId;

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var allyInPlay = game.GetCardInPlay<IAllyInPlay>(allyId);
                if (allyInPlay == null)
                    return;

                var allyController = game.GetController(allyId);
                if (allyController == null)
                    return;

                allyController.RemoveCardInPlay(allyInPlay);

                var eventController = game.GetController(CardSource.Id);
                if (eventController == null)
                    return;

                eventController.Hand.AddCards(new List<IPlayerCard> { allyInPlay.Card });
            }
        }
    }
}
