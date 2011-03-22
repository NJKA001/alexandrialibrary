using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IForeignKeyColumnar
    {
        IForeignKeyColumn Column(string name);
    }

    public interface IForeignKeyColumnar<T>
    {
        IForeignKeyColumn<T> Column(Expression<Func<T, object>> expression);
    }
}
