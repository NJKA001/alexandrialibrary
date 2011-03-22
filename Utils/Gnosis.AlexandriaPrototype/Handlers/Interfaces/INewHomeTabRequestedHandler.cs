using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers.Interfaces
{
    public interface INewHomeTabRequestedHandler : IHandler<INewHomeTabRequestedMessage>
    {
        ITabController Controller { get; set; }
    }
}
