using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LotR
{
    public class PlayerLoader
        : IPlayerLoader
    {
        public PlayerLoader()
        {
            InitializeCards();
        }

        private readonly IDictionary<string, IPlayerCard> playerCardMap = new Dictionary<string, IPlayerCard>();

        private static IEnumerable<Type> GetPlayerCards()
        {
            return GetTypesImplementingInterface(typeof(IPlayerCard)).Where(x => x != null && IsRealClass(x));
        }

        /// <summary>
        /// Returns all types in the current AppDomain implementing the interface or inheriting the type. 
        /// </summary>
        private static IEnumerable<Type> GetTypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => desiredType.IsAssignableFrom(type));

        }

        private static bool IsRealClass(Type testType)
        {
            return testType.IsAbstract == false
                && testType.IsGenericTypeDefinition == false
                && testType.IsInterface == false;
        }

        private void InitializeCards()
        {
            foreach (var type in GetPlayerCards())
            {
                var ctor = type.GetConstructor(new Type[0]);
                if (ctor == null)
                    continue;

                var playerCard = ctor.Invoke(null) as IPlayerCard;
                if (playerCard == null)
                    continue;

                playerCardMap[playerCard.Title] = playerCard;
            }
        }

        private enum LineMode
        {
            None = 0,
            Player = 1,
            Heroes = 2,
            Cards = 3
        }

        public IEnumerable<IPlayerCard> PlayerCards
        {
            get { return playerCardMap.Values; }
        }

        public IPlayer Load(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            if (!File.Exists(path))
                throw new InvalidOperationException("File does not exist: " + path);

            var name = "Unknown Player";
            var startingHeroes = new List<IHeroCard>();
            var cards = new List<IPlayerCard>();

            var lineMode = LineMode.None;

            using (var reader = new StreamReader(path))
            {
                var line = string.Empty;
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;

                    if (line.Contains(':'))
                    {
                        var lineTokens = line.Split(':');
                        if (lineTokens.Length > 0 && lineTokens[0] != null)
                        {
                            var modeTokens = lineTokens[0].Split(' ');
                            if (modeTokens != null && modeTokens.Length > 0 && modeTokens[0] != null)
                            {
                                switch (modeTokens[0].ToUpper())
                                {
                                    case "PLAYER":
                                        lineMode = LineMode.Player;
                                        break;
                                    case "HEROES":
                                        lineMode = LineMode.Heroes;
                                        break;
                                    case "CARDS":
                                        lineMode = LineMode.Cards;
                                        break;
                                }
                            }
                        }

                        if (lineTokens.Length > 1 && lineTokens[1] != null)
                        {
                            if (lineMode == LineMode.Player)
                            {
                                name = lineTokens[1];
                            }
                            else if (lineMode == LineMode.Heroes)
                            {
                                var heroTokens = lineTokens[1].Split(',');
                            }
                        }
                    }
                    else
                    {
                    }
                }
            }

            return new Player(name, startingHeroes, cards);
        }
    }
}
