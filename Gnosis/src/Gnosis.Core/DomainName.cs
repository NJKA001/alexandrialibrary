using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class DomainName
        : IDomainName
    {
        public DomainName(string primaryDomain, ITopLevelDomain tld, IEnumerable<string> subdomains)
        {
            if (primaryDomain == null)
                throw new ArgumentNullException("primaryDomain");

            this.primaryDomain = primaryDomain;
            this.tld = tld;
            this.subdomains = subdomains;
        }

        private readonly ITopLevelDomain tld;
        private readonly string primaryDomain;
        private readonly IEnumerable<string> subdomains;

        public string PrimaryDomain
        {
            get { return primaryDomain; }
        }

        public ITopLevelDomain TopLevelDomain
        {
            get { return TopLevelDomain; }
        }

        public IEnumerable<string> Subdomains
        {
            get { return subdomains; }
        }

        public bool IsLocalhost
        {
            get { return primaryDomain.ToLower() == "localhost" && tld == null; }
        }

        public static IDomainName Parse(string name)
        {
            var tokens = name.Split('.');
            if (tokens == null || tokens.Length == 0)
                return null;
            else if (tokens.Length == 1)
                return new DomainName(tokens[0], null, new List<string>());
            else if (tokens.Length == 2)
                return new DomainName(tokens[0], Core.TopLevelDomain.Parse(tokens[1]), new List<string>());
            else
            {
                var list = tokens.ToList();
                list.Reverse();

                string primaryDomain = null;
                ITopLevelDomain tld = null;
                var subdomains = new List<string>();

                for (var i = 0; i < list.Count; i++)
                {
                    if (i == 0)
                        tld = Core.TopLevelDomain.Parse(list[i]);
                    else if (i == 1)
                        primaryDomain = list[i];
                    else
                        subdomains.Add(list[i]);
                }

                return new DomainName(primaryDomain, tld, subdomains);
            }
        }
    }
}
