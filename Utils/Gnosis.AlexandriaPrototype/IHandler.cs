using System;

namespace Gnosis.Alexandria
{
    public interface IHandler
    {
        Type Type { get; }
    }

    public interface IHandler<in T> : IHandler
        where T : IMessage
    {
        void Handle(T message);
    }
}
