using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States
{
    public struct PlayerInfo
    {
        public PlayerInfo(string name, string deckPath)
        {
            this.name = name;
            this.deckPath = deckPath;
        }

        private readonly string name;
        private readonly string deckPath;

        public string Name { get { return name; } }
        public string DeckPath { get { return deckPath; } }
    }
}
