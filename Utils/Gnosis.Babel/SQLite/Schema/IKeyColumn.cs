using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IKeyColumn : IStatement, IKeyColumnar, ITableConstrained
    {

    }

    public interface IKeyColumn<T> : IStatement, IKeyColumnar<T>, ITableConstrained<T>
    {

    }
}
