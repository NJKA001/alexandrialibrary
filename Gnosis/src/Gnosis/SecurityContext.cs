using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis
{
    public class SecurityContext
        : ISecurityContext
    {
        private IUser currentUser = User.Administrator;
        private UserInfo currentUserInfo = UserInfo.Default;

        public IUser CurrentUser
        {
            get { return currentUser; }
        }

        public UserInfo CurrentUserInfo
        {
            get { return currentUserInfo; }
        }

        public void ChangeCurrentUser(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            currentUser = user;
            currentUserInfo = new UserInfo(user.Location, user.Name);
        }
    }
}
