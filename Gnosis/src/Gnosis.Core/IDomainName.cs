using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IDomainName
        : IHost
    {
        string PrimaryDomain { get; }
        ITopLevelDomain TopLevelDomain { get; }
        IEnumerable<string> Subdomains { get; }
    }
}
