using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IDomainName
        : IHost
    {
        ITopLevelDomain TopLevelDomain { get; }
        string PrimaryDomain { get; }
        IEnumerable<string> Subdomains { get; }
    }
}
