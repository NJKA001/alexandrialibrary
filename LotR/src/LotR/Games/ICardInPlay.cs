using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Modifiers;

namespace LotR.Games
{
    public interface ICardInPlay
        : INotifyPropertyChanged
    {
        Guid CardId { get; }
        ICard Card { get; }

        IEnumerable<IAttachmentInPlay> Attachments { get; }
        void AddAttachment(IAttachmentInPlay attachment);
        void RemoveAttachment(IAttachmentInPlay attachment);

        byte Resources { get; }
        void AddResources(byte value);
        void RemoveResources(byte value);

        IEnumerable<IModifier> Modifiers { get; }
        void AddModifier(IModifier modifier);
        void RemoveModifier(IModifier modifier);
    }
}
