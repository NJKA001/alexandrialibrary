using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Models.Schemas;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IDispatcher, IProcessor
    {
        public MainView()
        {
            InitializeComponent();

            InitializeDispatchers();

            AddHomeTab();
        }

        private readonly Guid _id = Guid.NewGuid();
        private readonly IDictionary<Guid, IDispatcher> _children = new Dictionary<Guid, IDispatcher>();

        #region Initialization Methods

        private void InitializeDispatchers()
        {
            ObjectExtensions.ArtistRepository = ServiceLocator.GetObject<IArtistRepository>();
            ObjectExtensions.CountryRepository = ServiceLocator.GetObject<ICountryRepository>();

            AddTabController();
            AddRepositoryController();

            Dispatch<IInitializeRepositoriesMessage>();
        }

        private void AddTabController()
        {
            var tabController = ServiceLocator.GetObject<ITabController>();
            tabController.TabControl = tabControl;

            AddChild(tabController);
        }

        private void AddRepositoryController()
        {
            var repositoryController = ServiceLocator.GetObject<IRepositoryController>();

            AddChild(repositoryController);
        }

        #endregion

        #region IDispatcher Members

        public Guid Id
        {
            get { return _id; }
        }

        IDispatcher IDispatcher.Parent
        {
            get { return null; }
            set
            {
                throw new InvalidOperationException("This root dispatcher cannot have a parent.");
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

            foreach (var child in _children.Values.Where(x => x.Id != sender))
                child.Dispatch(_id, message);
        }

        #endregion

        #region IProcessor Members

        private readonly IDictionary<Type, IList<IHandler>> _handlerMap = new Dictionary<Type, IList<IHandler>>();

        protected virtual void MessageReceived<T>(T message)
            where T : IMessage
        {
            Process(message);
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

        private void AddHomeTab()
        {
            Dispatch<INewHomeTabRequestedMessage>();
        }

        private void AddTabButtonClicked(object sender, RoutedEventArgs e)
        {
            AddHomeTab();
        }
    }
}
