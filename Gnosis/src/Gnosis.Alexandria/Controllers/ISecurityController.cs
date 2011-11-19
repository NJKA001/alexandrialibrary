using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface ISecurityController
    {
        IUser CurrentUser { get; }

        void ChangeUser(IUser user);
    }
}
