using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IArtist : IMutable, IDeletable
    {
        string Name { get; set; }
        string NameHash { get; }
        string Abbreviation { get; set; }
        ICountry Nationality { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string Note { get; set; }
    }
}
