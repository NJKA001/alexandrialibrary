using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface ICreateViewStatement<T> : IStatement
        where T: IModel
    {
        ICreateViewStatement<T> Temporary { get; }
        ICreateViewStatement<T> IfNotExists { get; }
        ICreateViewStatement<T> As(ISelectStatement<T> select);
    }
}
