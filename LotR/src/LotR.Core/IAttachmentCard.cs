using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IAttachmentCard
        : IPlayerCard, IAttachableCard
    {
        byte Cost { get; }
    }
}
