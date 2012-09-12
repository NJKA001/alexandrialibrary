using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICardInPlay
        : INotifyPropertyChanged
    {
        Guid Id { get; }
        ICard Card { get; }

        IEnumerable<IAttachmentInPlay> Attachments { get; }
        void AddAttachment(IAttachmentInPlay attachment);
        void RemoveAttachment(IAttachmentInPlay attachment);

        byte Resources { get; }
        void AddResources(byte value);
        void RemoveResources(byte value);
    }
}
