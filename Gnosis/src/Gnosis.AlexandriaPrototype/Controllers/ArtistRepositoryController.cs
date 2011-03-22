using System.Collections.Generic;
using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class ArtistRepositoryController : Controller, IArtistRepositoryController
    {
        public ArtistRepositoryController(IArtistRepository artistRepository, ISearchRequestedHandler searchRequestedHandler, IInitializeRepositoriesHandler initializeRepositoriesHandler)
        {
            _artistRepository = artistRepository;
            searchRequestedHandler.Controller = this;
            initializeRepositoriesHandler.Controller = this;
            
            AddHandler(searchRequestedHandler);
            AddHandler(initializeRepositoriesHandler);
        }

        private readonly IArtistRepository _artistRepository;

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            return _artistRepository.GetArtistsWithNamesLike(search);
        }

        public void Initialize()
        {
            _artistRepository.Initialize();
        }
    }
}
