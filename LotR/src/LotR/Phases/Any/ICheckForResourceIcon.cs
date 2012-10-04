using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public interface ICheckForResourceIcon
    {
        void CheckForResourceIcon(ICheckForResourceIconStep step);
    }
}
