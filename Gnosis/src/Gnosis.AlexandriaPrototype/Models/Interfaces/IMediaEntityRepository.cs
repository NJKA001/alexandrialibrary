using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IMediaEntityRepository : IRepository<IMediaEntity>
    {
        ICollection<IMediaEntity> GetMediaEntitiesWithNamesLike(string search);
    }
}
