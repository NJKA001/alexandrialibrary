using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Gnosis.Alexandria.Views
{
    public class ControlView : UserControl, IView
    {
        protected ControlView()
        {
        }

        private readonly Guid _id = Guid.NewGuid();
        private IDispatcher _parent;
        private readonly IDictionary<Guid,IDispatcher> _children = new Dictionary<Guid, IDispatcher>();

        #region IDispatcher Members

        public Guid Id
        {
            get { return _id; }
        }

        IDispatcher IDispatcher.Parent
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

            _children.Add(child.Id, child);
        }

        public void RemoveChild(Guid childId)
        {
            if (_children.ContainsKey(childId))
                _children.Remove(childId);
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

        #endregion

        #region IProcessor Members

        private readonly IDictionary<Type, IList<IHandler>> _handlerMap = new Dictionary<Type, IList<IHandler>>();

        protected virtual void MessageReceived<T>(T message)
            where T : IMessage
        {
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

        #endregion

        #region IView Members

        string IView.Title
        {
            get;
            set;
        }

        public virtual void SetFocus()
        {
        }

        #endregion
    }
}
