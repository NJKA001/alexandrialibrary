using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class ResourceUserInfo
        : IResourceUserInfo
    {
        public ResourceUserInfo(string userName, string password)
        {
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (password == null)
                throw new ArgumentNullException("password");

            this.userName = userName;
            this.password = password;
        }

        private readonly string userName;
        private readonly string password;

        public string UserName
        {
            get { return userName; }
        }

        public string Password
        {
            get { return password; }
        }
    }
}
