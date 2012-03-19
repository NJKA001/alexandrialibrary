using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IApplicationRunner
    {
        void Load(IApplication application);
    }
}
