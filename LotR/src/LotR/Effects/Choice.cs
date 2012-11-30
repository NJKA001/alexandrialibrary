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

        private bool AnswersAreValid(IQuestion question)
        {
            if (question.MinimumChosenAnswers == 0)
                return true;

            var numberChosen = question.Answers.Where(x => x.IsChosen).Count();
            if (numberChosen < question.MinimumChosenAnswers || numberChosen > question.MaximumChosenAnswers)
                return false;

            foreach (var answer in question.Answers.Where(x => x.FollowUp != null))
            {
                if (!AnswersAreValid(answer.FollowUp))
                    return false;
            }

            return true;
        }

        public bool IsValid(IGame game)
        {
            return AnswersAreValid(question);
        }
    }
}
