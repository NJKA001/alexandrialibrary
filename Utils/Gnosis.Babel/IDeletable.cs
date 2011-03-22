using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IDeletable : IModel
    {
        bool IsDeleted { get; }
        void Delete();
    }
}
