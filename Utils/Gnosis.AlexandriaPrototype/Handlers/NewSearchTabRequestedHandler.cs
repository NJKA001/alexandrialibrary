using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public class NewSearchTabRequestedHandler : Handler<INewSearchTabRequestedMessage>, INewSearchTabRequestedHandler
    {
        public NewSearchTabRequestedHandler()
        {
        }

        protected override void HandleMessage(INewSearchTabRequestedMessage message)
        {
            var view = ServiceLocator.GetObject<ISearchTabView>();
            view.Search = message.Search;
            Controller.AddTab(view);
        }

        public ITabController Controller { get; set; }
    }
}
