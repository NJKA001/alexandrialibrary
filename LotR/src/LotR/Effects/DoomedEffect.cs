using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public class DoomedEffect
        : CardEffectBase, IDoomedEffect
    {
        public DoomedEffect(IEncounterCard source, byte doomed)
            : base(GetDoomedDescription(doomed), source)
        {
            this.Doomed = doomed;
        }

        public static string GetDoomedDescription(byte doomed)
        {
            return string.Format("Doomed {0}", doomed);
        }

        public byte Doomed
        {
            get;
            protected set;
        }
    }
}
