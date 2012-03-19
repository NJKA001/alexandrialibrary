using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IBatch<T>
        where T : IEntity
    {
        void Start();
        void Delete(T entity);
        void Save(T entity);
        void Finish();
        void Cancel();
        void Close();

        object Execute(IStep command);
    }
}
