using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class Choice<TSource>
        : ChoiceItemBase<TSource>, IChoice
        where TSource : class, ISource
    {
        public Choice(string text, TSource source, IEnumerable<IQuestion> questions)
            : this(text, source, questions, false)
        {
        }

        public Choice(string text, TSource source, IEnumerable<IQuestion> questions, bool isOptional)
            : base(text, source)
        {
            if (questions == null)
                throw new ArgumentNullException("questions");

            this.questions = questions;
            this.isOptional = isOptional;
        }

        private readonly IEnumerable<IQuestion> questions;
        private readonly bool isOptional;

        private bool isCancelled;

        public IEnumerable<IPlayer> Players
        {
            get { return Enumerable.Empty<IPlayer>(); }
        }

        public IEnumerable<IQuestion> Questions
        {
            get { return questions; }
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
