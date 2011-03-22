using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers.Interfaces
{
    public interface ISearchRequestedHandler : IHandler<ISearchRequestedMessage>
    {
        IArtistRepositoryController Controller { get; set; }
    }
}
