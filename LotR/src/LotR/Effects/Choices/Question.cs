using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Choices
{
    public class Question
        : ChoiceItemBase, IQuestion
    {
        public Question(string text, IEnumerable<IAnswer> answers)
            : this(text, answers, 1, 1)
        {
        }

        public Question(string text, IEnumerable<IAnswer> answers, uint minimumChosenAnswers, uint maximumChosenAnswers)
            : base(text)
        {
            if (answers == null)
                throw new ArgumentNullException("answers");
            if (minimumChosenAnswers > maximumChosenAnswers)
                throw new ArgumentNullException("minimumChosenAnswers cannot be greater than maximumChosenAnswers");

            this.answers = answers;
            this.minimumChosenAnswers = minimumChosenAnswers;
            this.maximumChosenAnswers = maximumChosenAnswers;
        }

        private readonly IEnumerable<IAnswer> answers;
        private readonly uint minimumChosenAnswers;
        private readonly uint maximumChosenAnswers;

        public IEnumerable<IAnswer> Answers
        {
            get { return answers; }
        }

        public uint MinimumChosenAnswers
        {
            get { return minimumChosenAnswers; }
        }

        public uint MaximumChosenAnswers
        {
            get { return maximumChosenAnswers; }
        }
    }
}
