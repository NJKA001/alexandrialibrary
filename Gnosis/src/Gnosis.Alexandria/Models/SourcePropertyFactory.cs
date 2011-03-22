using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class SourcePropertyFactory : ISourcePropertyFactory
    {
        #region ISourcePropertyFactory Members

        public ISourceProperty Create(ISource source, string name)
        {
            return Create(Guid.NewGuid(), source, name);
        }

        public ISourceProperty Create(Guid id, ISource source, string name)
        {
            switch (name)
            {
                case "Location":
                    return new LocationProperty(id, source);
                default:
                    return null;
            }
        }

        #endregion
    }
}
