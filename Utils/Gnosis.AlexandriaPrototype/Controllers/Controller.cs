using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public abstract class Controller : Dispatcher, IController
    {
        protected Controller()
        {
        }

        private readonly IDictionary<Type, IList<IHandler>> _handlerMap = new Dictionary<Type, IList<IHandler>>();

        protected override void MessageReceived<T>(T message)
        {
            Process<T>(message);
        }

        protected virtual void BeforeProcessing<T>(T message)
            where T : IMessage
        {
        }

        protected virtual void AfterProcessing<T>(T message)
            where T : IMessage
        {        
        }

        public bool HasHandler(IHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            return (_handlerMap.ContainsKey(handler.Type))
                       ? _handlerMap[handler.Type].Contains(handler)
                       : false;
        }

        public void AddHandler(IHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (!_handlerMap.ContainsKey(handler.Type))
                _handlerMap[handler.Type] = new List<IHandler>();

            _handlerMap[handler.Type].Add(handler);
        }

        public void RemoveHandler(IHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (_handlerMap.ContainsKey(handler.Type) && _handlerMap[handler.Type].Contains(handler))
                _handlerMap[handler.Type].Remove(handler);
        }

        public void Process<T>(T message)
            where T : IMessage
        {
            if ((object)message == null)
                throw new ArgumentNullException("message");

            BeforeProcessing(message);

            if (_handlerMap.ContainsKey(typeof(T)))
            {
                foreach (var handler in _handlerMap[typeof(T)].Cast<IHandler<T>>())
                    handler.Handle(message);
            }

            AfterProcessing(message);
        }
    }
}
