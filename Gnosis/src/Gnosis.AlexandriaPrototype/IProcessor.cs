using System.Collections.Generic;

namespace Gnosis.Alexandria
{
    public interface IProcessor
    {
        bool HasHandler(IHandler handler);
        void AddHandler(IHandler handler);
        void RemoveHandler(IHandler handler);

        void Process<T>(T message)
            where T : IMessage;
    }
}
