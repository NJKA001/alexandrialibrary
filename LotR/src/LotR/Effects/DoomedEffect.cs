using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.Effects
{
    public class DoomedEffect
        : CardEffectBase, IDoomedEffect
    {
        public DoomedEffect(IEncounterCard source, byte doomed)
            : base(GetName(doomed), GetDescription(doomed), source)
        {
            this.Doomed = doomed;
        }

        private static string GetName(byte doomed)
        {
            return string.Format("Doomed {0}", doomed);
        }

        private static string GetDescription(byte doomed)
        {
            return string.Format("Each player must raise their threat by {0}", doomed);
        }

        public byte Doomed
        {
            get;
            protected set;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
