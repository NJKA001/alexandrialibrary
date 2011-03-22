using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers.Interfaces
{
    public interface INewSearchTabRequestedHandler : IHandler<INewSearchTabRequestedMessage>
    {
        ITabController Controller { get; set; }
    }
}
