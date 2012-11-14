using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseGandalfEffect
        : ChoiceBase, IChooseGandalfEffect
    {
        public ChooseGandalfEffect(ISource source, IPlayer player, IEnumerable<IEnemyInPlay> enemiesToChoose)
            : base("Draw three cards, deal 4 damage to 1 enemy in play, or reduce your threat by 5.", source, player)
        {
            if (enemiesToChoose == null)
                throw new ArgumentNullException("enemiesToChoose");

            this.enemiesToChoose = enemiesToChoose;
        }

        private readonly IEnumerable<IEnemyInPlay> enemiesToChoose;
        private IEnemyInPlay enemyToDamage;
        private bool drawCards;
        private bool reduceYourThreat;

        public IEnumerable<IEnemyInPlay> EnemiesToChoose
        {
            get { return enemiesToChoose; }
        }

        public IEnemyInPlay EnemyToDamage
        {
            get { return enemyToDamage; }
            set
            {
                enemyToDamage = value;

                if (enemyToDamage != null)
                {
                    DrawCards = false;
                    ReduceYourThreat = false;
                }
            }
        }

        public bool DrawCards
        {
            get { return drawCards; }
            set
            {
                drawCards = value;

                if (drawCards)
                {
                    enemyToDamage = null;
                    reduceYourThreat = false;
                }
            }
        }

        public bool ReduceYourThreat
        {
            get { return reduceYourThreat; }
            set
            {
                reduceYourThreat = value;

                if (reduceYourThreat)
                {
                    enemyToDamage = null;
                    drawCards = false;
                }
            }
        }

        public override bool IsValid(IGame game)
        {
            return ((EnemyToDamage != null && EnemiesToChoose.Contains(EnemyToDamage)) || DrawCards || ReduceYourThreat);
        }
    }
}
