using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;

namespace Gnosis.Alexandria.Models.Schemas
{
    public class CountrySchema : SQLiteSchema<ICountry>
    {
        public CountrySchema()
        {
            AddField(x => x.Name);
            AddField(x => x.NameHash);
            AddField(x => x.Abbreviation);
            AddField(x => x.Code);
            AddField(x => x.FromDate);
            AddField(x => x.ToDate);
            AddUniqueKey(Ascending(x => x.Name));
            AddUniqueKey(Ascending(x => x.NameHash));
            AddUniqueKey(Ascending(x => x.Code));
            AddKey(Ascending(x => x.FromDate));
        }
    }
}
