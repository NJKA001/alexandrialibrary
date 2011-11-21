using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public static class SecurityContext
    {
        private static IUser currentUser;

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
