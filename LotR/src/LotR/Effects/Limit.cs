using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public class Limit
        : ILimit
    {
        public Limit(PlayerScope playerScope, TimeScope timeScope, byte value)
        {
            this.PlayerScope = playerScope;
            this.TimeScope = timeScope;
            this.Value = value;
        }

        public PlayerScope PlayerScope
        {
            get;
            private set;
        }

        public TimeScope TimeScope
        {
            get;
            private set;
        }

        public byte Value
        {
            get;
            private set;
        }
    }
}
