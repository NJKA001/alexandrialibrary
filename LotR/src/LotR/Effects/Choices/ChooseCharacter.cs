using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseCharacter
        : ChoiceBase, IChooseCharacter
    {
        public ChooseCharacter(ISource source, IPlayer player, IEnumerable<ICharacterInPlay> charactersToChooseFrom)
            : base("Choose a character", source, player)
        {
            if (charactersToChooseFrom == null)
                throw new ArgumentNullException("charactersToChooseFrom");

            this.CharactersToChooseFrom = charactersToChooseFrom;
        }

        private ICharacterInPlay chosenCharacter;

        public IEnumerable<ICharacterInPlay> CharactersToChooseFrom
        {
            get;
            private set;
        }

        public ICharacterInPlay ChosenCharacter
        {
            get { return chosenCharacter; }
            set
            {
                if (chosenCharacter == value)
                    return;

                chosenCharacter = value;
                OnPropertyChanged("ChosenCharacter");
            }
        }

        public override bool IsValid(IGame game)
        {
            return chosenCharacter != null && CharactersToChooseFrom.Contains(chosenCharacter);
        }
    }
}
