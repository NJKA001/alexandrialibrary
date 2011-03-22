using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class CountryRepositoryController : Controller, ICountryRepositoryController
    {
        public CountryRepositoryController(ICountryRepository countryRepository, IInitializeRepositoriesHandler initializeRepositoriesHandler)
        {
            _countryRepository = countryRepository;

            initializeRepositoriesHandler.Controller = this;

            AddHandler(initializeRepositoriesHandler);
        }

        private readonly ICountryRepository _countryRepository;

        public void Initialize()
        {
            _countryRepository.Initialize();
        }
    }
}
