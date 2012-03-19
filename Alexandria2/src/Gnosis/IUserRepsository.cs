using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IUserRepsository
    {
        void Initialize();
        void Delete(IUser user);
        void Save(IUser user);

        IUser GetByLocation(Uri location);
        IEnumerable<IUser> GetAll();
    }
}
