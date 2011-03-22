using System;
using System.Collections.Generic;

namespace Gnosis.Alexandria
{
    public interface IDispatcher
    {
        Guid Id { get; }
        IDispatcher Parent { get; set; }
        IEnumerable<IDispatcher> Children { get; }

        void AddChild(IDispatcher child);
        void RemoveChild(Guid childId);

        void Dispatch<T>() where T : IMessage;
        void Dispatch<T>(T message) where T : IMessage;
        void Dispatch<T>(Guid sender, T message) where T : IMessage;
    }
}
