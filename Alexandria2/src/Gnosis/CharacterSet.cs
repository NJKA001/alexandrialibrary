using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class CharacterSet
        : ICharacterSet
    {
        protected internal CharacterSet(string name, bool isDefault, string description)
            : this(name, isDefault, description, null)
        {
        }

        protected internal CharacterSet(string name, bool isDefault, string description, byte[] byteOrderMark)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (description == null)
                throw new ArgumentNullException("description");

            this.name = name;
            this.isDefault = isDefault;
            this.description = description;
            this.byteOrderMark = byteOrderMark;
        }

        private readonly string name;
        private readonly bool isDefault;
        private readonly string description;
        private readonly byte[] byteOrderMark;

        #region ICharacterSet Members

        public string Name
        {
            get { return name; }
        }

        public bool IsDefault
        {
            get { return isDefault; }
        }

        public string Description
        {
            get { return description; }
        }

        public byte[] ByteOrderMark
        {
            get { return byteOrderMark; }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}
