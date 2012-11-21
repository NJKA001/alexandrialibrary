using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public class Question<TSource>
        : ChoiceItemBase<TSource>, IQuestion
        where TSource : class, ISource
    {
        public Question(string text, TSource source, IPlayer player)
            : this(text, source, player, Enumerable.Empty<IAnswer>(), 1, 1)
        {
        }

        public Question(string text, TSource source, IPlayer player, uint minimumChosenAnswers, uint maximumChosenAnswers)
            : this(text, source, player, Enumerable.Empty<IAnswer>(), minimumChosenAnswers, maximumChosenAnswers)
        {
        }

        public Question(string text, TSource source, IPlayer player, IEnumerable<IAnswer> answers)
            : this(text, source, player, answers, 1, 1)
        {
        }

        public Question(string text, TSource source, IPlayer player, IEnumerable<IAnswer> answers, uint minimumChosenAnswers, uint maximumChosenAnswers)
            : base(text, source)
        {
            if (player == null)
                throw new ArgumentNullException("player");
            if (answers == null)
                throw new ArgumentNullException("answers");
            if (minimumChosenAnswers > maximumChosenAnswers)
                throw new ArgumentNullException("minimumChosenAnswers cannot be greater than maximumChosenAnswers");

            this.player = player;
            this.answers.AddRange(answers);
            this.minimumChosenAnswers = minimumChosenAnswers;
            this.maximumChosenAnswers = maximumChosenAnswers;
        }

        private readonly IPlayer player;
        private readonly List<IAnswer> answers = new List<IAnswer>();
        private readonly uint minimumChosenAnswers;
        private readonly uint maximumChosenAnswers;

        public IPlayer Player
        {
            get { return player; }
        }

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

        public void AddAnswer(IAnswer answer)
        {
            if (answer == null)
                throw new ArgumentNullException("answer");

            this.answers.Add(answer);
        }
    }
}
