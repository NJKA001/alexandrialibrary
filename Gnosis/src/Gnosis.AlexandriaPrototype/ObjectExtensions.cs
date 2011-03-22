using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;

namespace Gnosis.Alexandria
{
    public static class ObjectExtensions
    {
        public static IArtistRepository ArtistRepository;
        public static ICountryRepository CountryRepository;

        public static IArtist AsArtist(this object value)
        {
            return (value.IsDefined()) ? ArtistRepository.GetOne(value) : null;
        }

        public static ICountry AsCountry(this object value)
        {
            return (value.IsDefined()) ? CountryRepository.GetOne(value) : null;
        }
    }
}
