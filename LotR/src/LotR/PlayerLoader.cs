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

        private string GetPlayerCardKey(IPlayerCard card)
        {
            return string.Format("{0} ({1})", card.Title, card.SetName);
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
                
                playerCardMap[GetPlayerCardKey(playerCard)] = playerCard;
            }
        }

        private IPlayerCard LookupPlayerCard(string title)
        {
            var key = title;
            IPlayerCard card = null;
            
            if (playerCardMap.ContainsKey(key))
            {
                return playerCardMap[key]; 
            }
            else if (!title.Contains('('))
            {
                var coreKey = title + " (Core)";
                if (playerCardMap.ContainsKey(coreKey))
                    return playerCardMap[coreKey];

                var soMKey = title + " (SoM)";
                if (playerCardMap.ContainsKey(soMKey))
                    return playerCardMap[soMKey];
            }
            
            return card;
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
                                name = lineTokens[1].Trim();
                            }
                            else if (lineMode == LineMode.Heroes)
                            {
                                var heroTokens = lineTokens[1].Split(',');
                                foreach (var heroToken in heroTokens.Where(x => x != null))
                                {
                                    var heroTitle = heroToken.Trim();
                                    var hero = LookupPlayerCard(heroTitle) as IHeroCard;
                                    if (hero != null)
                                    {
                                        startingHeroes.Add(hero);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (lineMode == LineMode.Player)
                        {
                            name = line.Trim();
                        }
                        else if (lineMode == LineMode.Heroes)
                        {
                            var hero = LookupPlayerCard(line.Trim()) as IHeroCard;
                            if (hero != null)
                            {
                                startingHeroes.Add(hero);
                            }
                        }
                        else if (lineMode == LineMode.Cards)
                        {
                            var cardTokens = line.Split(new string[] { "\t", "  " }, StringSplitOptions.RemoveEmptyEntries);
                            if (cardTokens != null && cardTokens.Length > 0)
                            {
                                var number = 1;
                                if (cardTokens.Length > 1)
                                {
                                    int.TryParse(cardTokens[1], out number);
                                    if (number < 1 || number > 3)
                                    {
                                        number = 1;
                                    }
                                }

                                for (var i = 0; i < number; i++)
                                {
                                    var card = LookupPlayerCard(cardTokens[0].Trim());
                                    if (card != null)
                                    {
                                        if (i == 0)
                                        {
                                            cards.Add(card);
                                        }
                                        else if (i == 1)
                                        {
                                            cards.Insert(0, card);
                                        }
                                        else if (i == 3)
                                        {
                                            if (cards.Count > 2)
                                            {
                                                var index = (int)Math.Floor((decimal)cards.Count / 2);
                                                cards.Insert(index, card);
                                            }
                                            else
                                            {
                                                cards.Add(card);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new Player(name, startingHeroes, cards);
        }
    }
}
