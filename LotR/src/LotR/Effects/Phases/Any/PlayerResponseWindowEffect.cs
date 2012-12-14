using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayerResponseWindowEffect
        : FrameworkEffectBase
    {
        public PlayerResponseWindowEffect(IGame game, IPlayer player, IResponseEffect responseEffect)
            : base("Player Response Window", string.Format("{0} has an opportunity to respond to a game effect", player.Name), game)
        {
            this.player = player;
            this.responseEffect = responseEffect;
        }

        private readonly IPlayer player;
        private readonly IResponseEffect responseEffect;

        private void TriggerResponse(IGame game, IEffectHandle handle)
        {
            var responseHandle = responseEffect.GetHandle(game);
            game.TriggerEffect(responseHandle);

            handle.Resolve(string.Format("{0} decided to trigger '{1}'", player.Name, responseEffect));
        }

        private void DoNotTriggerResponse(IEffectHandle handle)
        {
            handle.Cancel(string.Format("{0} decided not to trigger '{1}'", player.Name, responseEffect));
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var builder =
                new ChoiceBuilder(string.Format("{0} has an opportunity to respond to a game effect", player.Name), game, player)
                    .Question(string.Format("{0}, do you want to trigger the response '{1}'?", player.Name, responseEffect))
                        .Answer("Yes, I want to trigger this response", true, (source, handle, item) => TriggerResponse(source, handle))
                        .LastAnswer("No, I do not want to trigger this response", false, (sourcr, handle, item) => DoNotTriggerResponse(handle));


            return new EffectHandle(this, builder.ToChoice());
        }
    }
}
