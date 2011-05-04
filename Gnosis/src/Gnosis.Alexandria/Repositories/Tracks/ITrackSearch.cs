using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public interface ITrackSearch
    {
        string GetWhereClause();
    }
}
