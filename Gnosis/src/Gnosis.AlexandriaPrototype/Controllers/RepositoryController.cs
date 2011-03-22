using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class RepositoryController : Controller, IRepositoryController
    {
        public RepositoryController(IArtistRepositoryController artistRepositoryController, ICountryRepositoryController countryRepositoryController)
        {
            AddChild(artistRepositoryController);
            AddChild(countryRepositoryController);
        }

        public ICollection<IMediaEntity> GetMediaEntities(string search)
        {
            return new Collection<IMediaEntity>();
        }

        public void Initialize()
        {
        }
    }
}
