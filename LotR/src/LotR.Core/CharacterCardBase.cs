using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public class CharacterCardBase
        : PlayerCardBase, ICharacterCard
    {
        public byte Willpower
        {
            get;
            protected set;
        }

        public byte Attack
        {
            get;
            protected set;
        }

        public byte Defense
        {
            get;
            protected set;
        }

        public byte HitPoints
        {
            get;
            protected set;
        }
    }
}
