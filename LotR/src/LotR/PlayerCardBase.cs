using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public abstract class PlayerCardBase
        : CardBase, IPlayerCard
    {
        protected PlayerCardBase(string title, string setName, uint setNumber)
            : base(title, setName, setNumber)
        {
        }

        private readonly List<Sphere> spheresOfInfluence = new List<Sphere>();

        protected void AddSphereOfInfluence(Sphere sphere)
        {
            spheresOfInfluence.Add(sphere);
        }

        public IEnumerable<Sphere> SpheresOfInfluence
        {
            get { return spheresOfInfluence; }
        }
    }
}
