using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.CharacterAbilities
{
    public abstract class CharacterAbilityBase
        : ICharacterAbility
    {
        public string Name
        {
            get;
            protected set;
        }

        public ICard Source
        {
            get { throw new NotImplementedException(); }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public string SetName
        {
            get { throw new NotImplementedException(); }
        }

        public uint SetNumber
        {
            get { throw new NotImplementedException(); }
        }

        public ICardText Text
        {
            get { throw new NotImplementedException(); }
        }

        public object Image
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasTrait(Trait trait)
        {
            throw new NotImplementedException();
        }

        public bool IsUnique
        {
            get { throw new NotImplementedException(); }
        }
    }
}
