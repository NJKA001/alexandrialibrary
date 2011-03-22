using Gnosis.Babel.SQLite;

namespace Gnosis.Alexandria.Models.Stores
{
    public class CatalogStore : SQLiteStore
    {
        public CatalogStore()
            : base("Catalog")
        {
        }
    }
}
