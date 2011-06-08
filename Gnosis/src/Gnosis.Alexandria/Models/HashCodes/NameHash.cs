using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.HashCodes
{
    public class NameHash
        : ValueBase, IHashCode
    {
        public NameHash()
        {
            AddInitializer("Value", x => this.value = x.ToString());
        }

        private NameHash(Guid parent, string value)
        {
            AddInitializer("Value", x => this.value = value);

            Initialize(parent);
        }

        private string value;

        public Uri Scheme
        {
            get { return Namespace; }
        }

        public string Value
        {
            get { return value; }
        }

        public static Uri Namespace = new Uri("http://alxlib.com/domain/1/hash-schemes/double-metaphone/1");

        public static IHashCode Create(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            return new NameHash(parent, originalString.AsNameHash());
        }
    }
}
