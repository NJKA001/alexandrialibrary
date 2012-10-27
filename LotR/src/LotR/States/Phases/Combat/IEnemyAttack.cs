using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface IEnemyAttack
        : IState
    {
        IPlayer DefendingPlayer { get; }
        IEnemyInPlay Enemy { get; }
        IEnumerable<IDefendingInPlay> Defenders { get; }
        
        byte NumberOfShadowCardsToDeal { get; set; }
        IEnumerable<IShadowInPlay> ShadowCards { get; }

        byte Attack { get; set; }
        bool IsAttacking { get; set; }
        bool IsUndefended { get; set; }

        void AddDefender(IDefendingInPlay defender);
        void RemoveDefender(IDefendingInPlay defender);

        void AddShadowCard(IShadowInPlay shadow);
        void RemoveShadowCard(IShadowInPlay shadow);
    }
}
