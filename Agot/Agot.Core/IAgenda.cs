using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IAgenda
        : ICard
    {
        
        AgendaType AgendaType { get; }

        IEnumerable<ICard> Attachments { get; }

        void AddAttachment(ICard card);
        void RemoveAttachment(ICard card);
    }
}
