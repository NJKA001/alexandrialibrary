using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Messages.Interfaces
{
    public interface ISearchRequestedMessage : ITargetedMessage
    {
        string Search { get; set; }
    }
}
