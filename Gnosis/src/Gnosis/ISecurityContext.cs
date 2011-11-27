using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ISecurityContext
    {
        IUser CurrentUser { get; }

        void ChangeCurrentUser(IUser user);
    }
}
