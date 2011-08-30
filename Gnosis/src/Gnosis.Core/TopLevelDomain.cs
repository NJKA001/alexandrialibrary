using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TopLevelDomain
        : ITopLevelDomain
    {
        private TopLevelDomain(string name, string description)
            : this(name, description, null, false, false, true)
        {
        }

        private TopLevelDomain(string name, string description, ICountry country, bool isGeneric, bool isInternational, bool isRecognized)
        {
            this.name = name;
            this.description = description;
            this.country = country;
            this.isGeneric = isGeneric;
            this.isInternational = isInternational;
            this.isRecognized = isRecognized;
        }

        private readonly string name;
        private readonly string description;
        private readonly ICountry country;
        private readonly bool isGeneric;
        private readonly bool isInternational;
        private readonly bool isRecognized;

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public ICountry Country
        {
            get { return country; }
        }

        public bool IsGeneric
        {
            get { return isGeneric; }
        }

        public bool IsInternational
        {
            get { return isInternational; }
        }

        public bool IsRecognized
        {
            get { return isRecognized; }
        }

        public override string ToString()
        {
            return name;
        }

        public static ITopLevelDomain Parse(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return new TopLevelDomain(name, "Unrecognized Top Level Domain", null, false, false, false);
        }
    }
}
