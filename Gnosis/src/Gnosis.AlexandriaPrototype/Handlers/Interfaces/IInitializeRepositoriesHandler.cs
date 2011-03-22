using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers.Interfaces
{
    public interface IInitializeRepositoriesHandler : IHandler<IInitializeRepositoriesMessage>
    {
        IRepositoryController Controller { get; set; }
    }
}
