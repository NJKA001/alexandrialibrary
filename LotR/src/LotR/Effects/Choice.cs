using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public class Choice<TSource>
        : ChoiceItemBase<TSource>, IChoice
        where TSource : class, ISource
    {
        public Choice(string text, TSource source, IQuestion question)
            : this(text, source, question, false)
        {
        }

        public Choice(string text, TSource source, IQuestion question, bool isOptional)
            : base(text, source)
        {
            if (question == null)
                throw new ArgumentNullException("question");

            this.question = question;
            this.isOptional = isOptional;
        }

        private readonly IQuestion question;
        private readonly bool isOptional;

        private bool isCancelled;

        public IEnumerable<IPlayer> Players
        {
            get { return Enumerable.Empty<IPlayer>(); }
        }

        public IQuestion Question
        {
            get { return question; }
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
            get { return isOptional; }
        }

        public bool IsValid(IGame game)
        {
            return true;
            //var chosen = 
        }
    }
}
