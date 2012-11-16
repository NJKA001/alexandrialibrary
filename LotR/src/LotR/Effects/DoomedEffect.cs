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
            : base(GetType(doomed), GetText(doomed), source)
        {
            this.Doomed = doomed;
        }

        public static string GetType(byte doomed)
        {
            return string.Format("Doomed {0}", doomed);
        }

        private static string GetText(byte doomed)
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
            return string.Format("Doomed {0}: {1}", Doomed, text);
        }
    }
}
