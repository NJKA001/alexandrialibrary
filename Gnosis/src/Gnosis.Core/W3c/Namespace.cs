using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class Namespace
        : INamespace
    {
        public Namespace(Uri identifier)
            : this(identifier, null)
        {   
        }

        public Namespace(Uri identifier, string alias)
        {
            this.identifier = identifier;
            this.alias = alias;
        }

        private readonly Uri identifier;
        private readonly string alias;

        #region INamespace Members

        public Uri Identifier
        {
            get { return identifier; }
        }

        public string Alias
        {
            get { return alias; }
        }

        #endregion

        public override string ToString()
        {
            return !string.IsNullOrEmpty(alias) ? 
                string.Format("xmlns:{0}='{1}'", alias, identifier)
                : string.Format("xmlns='{0}'", identifier);
        }
    }
}
