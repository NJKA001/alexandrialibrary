using System;
using System.Collections.Generic;
using System.Linq;

namespace Gnosis.Alexandria.Controllers
{
    public abstract class Dispatcher : IDispatcher
    {
        protected Dispatcher()
        {
        }

        private readonly Guid _id = Guid.NewGuid();
        private IDispatcher _parent;
        private readonly IDictionary<Guid,IDispatcher> _children = new Dictionary<Guid, IDispatcher>();

        protected abstract void MessageReceived<T>(T message)
            where T : IMessage;

        protected T CreateMessage<T>()
            where T : IMessage
        {
            return ServiceLocator.GetObject<T>();
        }

        public Guid Id
        {
            get { return _id; }
        }

        public IDispatcher Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null && value != null)
                    throw new InvalidOperationException("This dispatcher already has a parent assigned. You cannot change the parent of a dispatcher.");

                _parent = value;
            }
        }

        public IEnumerable<IDispatcher> Children
        {
            get { return _children.Values; }
        }

        public void AddChild(IDispatcher child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            child.Parent = this;
            _children.Add(child.Id, child);
        }

        public void RemoveChild(Guid id)
        {
            if (!_children.ContainsKey(id))
                return;

            _children[id].Parent = null;
            _children.Remove(id);
        }

        public void Dispatch<T>()
            where T : IMessage
        {
            var message = ServiceLocator.GetObject<T>();
            Dispatch(_id, message);
        }

        public void Dispatch<T>(T message)
            where T : IMessage
        {
            Dispatch(_id, message);
        }

        public void Dispatch<T>(Guid sender, T message)
            where T : IMessage
        {
            if ((object)message == null)
                throw new ArgumentNullException("message");

            MessageReceived(message);

            if (_parent != null && _parent.Id != sender)
                _parent.Dispatch(_id, message);

            foreach (var child in _children.Values.Where(x => x.Id != sender))
                child.Dispatch(_id, message);
        }
    }
}
