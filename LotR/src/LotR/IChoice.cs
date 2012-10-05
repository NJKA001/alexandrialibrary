using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IChoice
    {
        string Description { get; }
        ICard Source { get; }
    }
}
