using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects
{
    public class ChoiceBase
        : ChoiceItemBase<ISource>, IChoice
    {
        protected ChoiceBase(string text, ISource source, IPlayer player)
            : this(text, source, new List<IPlayer> { player })
        {
        }

        protected ChoiceBase(string text, ISource source, IEnumerable<IPlayer> players)
            : base(text, source)
        {
            if (players == null)
                throw new ArgumentNullException("players");
            
            this.players = players;
        }

        private readonly IEnumerable<IPlayer> players;
        private bool isCancelled;

        public IEnumerable<IPlayer> Players
        {
            get { return players; }
        }

        public IQuestion Question
        {
            get { return null; }
        }

        public bool IsCancelled
        {
            get { return isCancelled; }
            set
            {
                if (isCancelled == value)
                    return;

                isCancelled = value;
                OnPropertyChanged("IsCancelled");
            }
        }

        public bool IsOptional
        {
            get;
            protected set;
        }

        public virtual bool IsValid(IGame game)
        {
            return true;
        }
    }
}
