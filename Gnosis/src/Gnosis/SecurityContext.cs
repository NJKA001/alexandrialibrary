using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis
{
    public static class SecurityContext
    {
        private static IUser currentUser = GnosisUser.Administrator;

        public static IUser CurrentUser
        {
            get { return currentUser; }
        }

        public static void ChangeCurrentUser(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            currentUser = user;
        }
    }
}
