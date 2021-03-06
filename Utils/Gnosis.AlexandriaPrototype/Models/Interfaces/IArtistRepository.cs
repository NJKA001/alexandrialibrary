﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IArtistRepository : IRepository<IArtist>
    {
        ICollection<IArtist> GetArtistsWithNamesLike(string search);
    }
}
