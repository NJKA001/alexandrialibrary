using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public interface ISource
    {
        Guid Id { get; }
        string Title { get; }
    }
}
