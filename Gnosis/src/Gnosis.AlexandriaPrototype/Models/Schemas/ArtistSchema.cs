using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;

namespace Gnosis.Alexandria.Models.Schemas
{
    public class ArtistSchema : SQLiteSchema<IArtist>
    {
        public ArtistSchema()
        {
            AddField(x => x.Name, (x,y) => x.Name = y.AsString());
            AddField(x => x.NameHash);
            AddField(x => x.Abbreviation, (x,y) => x.Abbreviation = y.AsString());
            AddField(x => x.Nationality, (x,y) => x.Nationality = y.AsCountry());
            AddField(x => x.FromDate, (x,y) => x.FromDate = y.AsDateTime());
            AddField(x => x.ToDate, (x, y) => x.ToDate = y.AsDateTime());
            AddField(x=> x.Note, (x,y) => x.Note = y.AsString());
            AddUniqueKey(Ascending(x => x.Nationality), Ascending(x => x.Name), Ascending(x => x.FromDate));
            AddKey(Ascending(x => x.Name));
            AddKey(Ascending(x => x.NameHash));
            AddKey(Ascending(x => x.FromDate));
        }
    }
}
