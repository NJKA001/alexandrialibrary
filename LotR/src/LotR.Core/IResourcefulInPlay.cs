using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IResourcefulInPlay
        : ICardInPlay
    {
        new IResourcefulCard Card { get; }

        bool HasResourceIcon(Sphere sphere);
    }
}
