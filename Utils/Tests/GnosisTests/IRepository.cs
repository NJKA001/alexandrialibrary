using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests
{
    public interface IRepository<T>
        where T : IEntity
    {
        IEnumerable<T> Entities { get; }
        
        T Get(uint id);

        void Initialize();
        void Create(T entity);
        void Create(IEnumerable<T> entities);
        void Update(T entity, string field, string value);
        void Update(IEnumerable<T> entities, string field, string value);
        void Delete(uint id);
        void Delete(IEnumerable<uint> ids);
        void Compact();
    }
}
