using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseCharacterWithTrait
        : ChoiceBase, IChooseCharacterWithTrait
    {
        public ChooseCharacterWithTrait(ISource source, IPlayer player, Trait trait, IEnumerable<ICharacterInPlay> charactersToChooseFrom)
            : base(GetDescription(trait), source, player)
        {
            if (charactersToChooseFrom == null)
                throw new ArgumentNullException("charactersToChooseFrom");

            this.CharactersToChooseFrom = charactersToChooseFrom;
            this.Trait = trait;
        }

        private static string GetDescription(Trait trait)
        {
            return string.Format("Choose a '{0}' character", trait);
        }

        private ICharacterInPlay chosenCharacter;

        public IEnumerable<ICharacterInPlay> CharactersToChooseFrom
        {
            get;
            private set;
        }

        public Trait Trait
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

        public override bool IsValid(LotR.States.IGame game)
        {
            return chosenCharacter != null && CharactersToChooseFrom.Contains(chosenCharacter);
        }
    }
}
