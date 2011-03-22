using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public class SearchRequestedHandler : Handler<ISearchRequestedMessage>, ISearchRequestedHandler
    {
        public SearchRequestedHandler()
        {
        }

        protected override void HandleMessage(ISearchRequestedMessage message)
        {
            var entities = Controller.GetArtistsWithNamesLike(message.Search);
            var x = entities.Count;
            //TODO: put the results into a message
        }

        public IArtistRepositoryController Controller { get; set; }
    }
}
