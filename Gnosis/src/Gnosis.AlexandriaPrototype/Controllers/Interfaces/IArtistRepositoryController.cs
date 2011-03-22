using System.Collections.Generic;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers.Interfaces
{
    public interface IArtistRepositoryController : IRepositoryController
    {
        ICollection<IArtist> GetArtistsWithNamesLike(string search);
    }
}
