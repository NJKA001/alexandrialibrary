using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis
{
    public class SecurityContext
        : ISecurityContext
    {
        private IUser currentUser = GnosisUser.Administrator;

        public IUser CurrentUser
        {
            get { return currentUser; }
        }

        public void ChangeCurrentUser(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            currentUser = user;
        }
    }
}
