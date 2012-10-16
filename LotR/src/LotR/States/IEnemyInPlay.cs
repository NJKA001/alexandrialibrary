using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;

namespace LotR.States
{
    public interface IEnemyInPlay
        : ICardInPlay<IEnemyCard>
    {
        byte Resources { get; set; }

        IEnumerable<IShadowInPlay> ShadowCards { get; }
        void AddShadowCard(IShadowInPlay shadow);
        void RemoveShadowCard(IShadowInPlay shadow);
    }
}
