using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;
using LotR.States.Areas;

namespace LotR.Effects.Choices
{
    public class EachPlayerChoosesReadyCharacters
        : PlayersChooseCharacters
    {
        public EachPlayerChoosesReadyCharacters(ISource source, IPlayer player, byte numberOfCharacters)
            : this(source, player, numberOfCharacters, false)
        {
        }

        public EachPlayerChoosesReadyCharacters(ISource source, IPlayer player, byte numberOfCharacters, bool heroesOnly)
            : base(GetDescription(player, numberOfCharacters, heroesOnly), source, new List<IPlayer> { player }, numberOfCharacters, GetAvailableCharacters(player, heroesOnly))
        {
            this.heroesOnly = heroesOnly;
        }

        public EachPlayerChoosesReadyCharacters(ISource source, IGame game, byte numberOfCharacters)
            : this(source, game, numberOfCharacters, false)
        {
        }

        public EachPlayerChoosesReadyCharacters(ISource source, IGame game, byte numberOfCharacters, bool heroesOnly)
            : base(GetDescription(game, numberOfCharacters, heroesOnly), source, GetPlayers(game), numberOfCharacters, GetAvailableCharacters(game, heroesOnly))
        {
            this.heroesOnly = heroesOnly;
        }

        private bool heroesOnly = false;

        private static IEnumerable<IPlayer> GetPlayers(IGame game)
        {
            return game.GetStates<IPlayer>();
        }

        private static string GetDescription(IPlayer player, byte numberOfCharacters, bool heroesOnly)
        {
            if (numberOfCharacters == 1)
            {
                var type = heroesOnly ? "hero" : "character";
                return string.Format("{0} chooses 1 ready {1} they control", player.Name, type);
            }
            else
            {
                var type = heroesOnly ? "heroes" : "characters";
                return string.Format("{0} chooses {1} ready {2} that they control", player.Name, numberOfCharacters, type);
            }
        }

        private static string GetDescription(IGame game, byte numberOfCharacters, bool heroesOnly)
        {
            var players = GetPlayers(game);
            if (players.Count() == 1)
            {
                return GetDescription(players.First(), numberOfCharacters, heroesOnly);
            }
            else
            {
                if (numberOfCharacters == 1)
                {
                    var type = heroesOnly ? "hero" : "character";
                    return string.Format("{0} each choose 1 ready {1} that they control", string.Join(" and ", players), type);
                }
                else
                {
                    var type = heroesOnly ? "heroes" : "characters";
                    return string.Format("{0} each choose {1} ready {2} that they control", string.Join(" and ", players), numberOfCharacters, type);
                }
            }
        }

        private static void AddReadyCharacters(IPlayer player, IDictionary<Guid, IList<ICharacterInPlay>> availableCharacters, bool heroesOnly)
        {
            var playerArea = player.GetStates<IPlayerArea>().FirstOrDefault();
            if (playerArea == null)
                return;

            foreach (var exhaustable in playerArea.GetStates<ICharacterInPlay>().OfType<IExhaustableInPlay>())
            {
                if (exhaustable.IsExhausted)
                    continue;

                if (heroesOnly && (!(exhaustable is IHeroInPlay)))
                    continue;

                if (!availableCharacters.ContainsKey(player.StateId))
                    availableCharacters[player.StateId] = new List<ICharacterInPlay> { exhaustable as ICharacterInPlay };
                else
                    availableCharacters[player.StateId].Add(exhaustable as ICharacterInPlay);
                
            }
        }

        private static IDictionary<Guid, IList<ICharacterInPlay>> GetAvailableCharacters(IGame game, bool heroesOnly)
        {
            var availableCharacters = new Dictionary<Guid, IList<ICharacterInPlay>>();

            foreach (var player in GetPlayers(game))
            {
                AddReadyCharacters(player, availableCharacters, heroesOnly);
            }

            return availableCharacters;
        }

        private static IDictionary<Guid, IList<ICharacterInPlay>> GetAvailableCharacters(IPlayer player, bool heroesOnly)
        {
            var availableCharacters = new Dictionary<Guid, IList<ICharacterInPlay>>();

            AddReadyCharacters(player, availableCharacters, heroesOnly);

            return availableCharacters;
        }
    }
}
