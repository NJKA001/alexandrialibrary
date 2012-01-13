using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Metadata;

namespace Gnosis
{
    public interface ISecurityContext
    {
        IUser CurrentUser { get; }
        UserInfo CurrentUserInfo { get; }

        void ChangeCurrentUser(IUser user);
    }
}
