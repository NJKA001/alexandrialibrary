using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public class NewHomeTabRequestedHandler : Handler<INewHomeTabRequestedMessage>, INewHomeTabRequestedHandler
    {
        public NewHomeTabRequestedHandler()
        {
        }

        protected override void HandleMessage(INewHomeTabRequestedMessage message)
        {
            var view = ServiceLocator.GetObject<IHomeTabView>();
            Controller.AddTab(view);
        }

        public ITabController Controller { get; set; }
    }
}
