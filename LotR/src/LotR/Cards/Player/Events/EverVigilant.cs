using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Cards.Player.Events
{
    public class EverVigilant
        : EventCardBase
    {
        public EverVigilant()
            : base("Ever Vigilant", CardSet.Core, 20, Sphere.Leadership, 1)
        {
        }

        public class ReadyOneAlly
            : ActionCardEffectBase
        {
            public ReadyOneAlly(EverVigilant source)
                : base("Choose and ready 1 ally card.", source)
            {
            }

            private IEnumerable<IExhaustableInPlay> GetExhausedAlliesInPlay(IGame game)
            {
                var allies = new List<IExhaustableInPlay>();

                foreach (var player in game.Players)
                {
                    allies.AddRange(player.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card is IAllyCard && x.IsExhausted));
                }

                return allies;
            }

            private void ReadyExhaustedAlly(IGame game, IEffectHandle handle, IPlayer player, IExhaustableInPlay ally)
            {
                var controller = game.GetController(ally.Card.Id);

                ally.Ready();

                if (player == controller)
                {
                    handle.Resolve(string.Format("{0} chose to ready '{1}'", player.Name, ally.Title));
                }
                else
                {
                    handle.Resolve(string.Format("{0} chose to ready '{1}' controlled by {2}", player.Name, ally.Title, controller.Name));
                }
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var card = source as IPlayerCard;
                if (card == null)
                    throw new InvalidOperationException();

                var player = card.Owner;
                if (player == null)
                    throw new InvalidOperationException();

                var allies = GetExhausedAlliesInPlay(game);
                if (allies.Count() == 0)
                    return new EffectHandle(this);

                var builder =
                    new ChoiceBuilder("Choose an ally to ready", game, player)
                        .Question("Which exhausted ally will you ready?")
                            .LastAnswers(allies, item => string.Format("'{0}' controlled by {1}", item.Title, game.GetController(item.Card.Id).Name), (src, handle, ally) => ReadyExhaustedAlly(src, handle, player, ally));

                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
