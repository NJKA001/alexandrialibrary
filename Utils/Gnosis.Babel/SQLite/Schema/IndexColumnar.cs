using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class IndexColumnar : Statement, IIndexColumnar
    {
        private const string KeywordAsc = "asc";
        private const string KeywordDesc = "desc";

        public IIndexColumnar ColumnAsc(string name)
        {
            AppendParentheticalListItem(name);
            return AppendWord<IIndexColumnar, IndexColumnar>(KeywordAsc);
        }

        public IIndexColumnar ColumnDesc(string name)
        {
            AppendParentheticalListItem(name);
            return AppendWord<IIndexColumnar, IndexColumnar>(KeywordDesc);
        }

        public IIndexColumnar Columns(IEnumerable<IIndexedField> fields)
        {
            foreach (var field in fields)
            {
                AppendParentheticalListItem(field.Name);
                if (field.Ascending)
                    AppendWord(KeywordAsc);
                else
                    AppendWord(KeywordDesc);
            }

            return Transform<IIndexColumnar, IndexColumnar>();
        }
    }

    public class IndexColumnar<T> : Statement, IIndexColumnar<T>
    {
        private const string KeywordAsc = "asc";
        private const string KeywordDesc = "desc";

        public IIndexColumnar<T> ColumnAsc(Expression<Func<T, object>> expression)
        {
            AppendParentheticalListItem(expression.ToName());
            return AppendWord<IIndexColumnar<T>, IndexColumnar<T>>(KeywordAsc);
        }

        public IIndexColumnar<T> ColumnDesc(Expression<Func<T, object>> expression)
        {
            AppendParentheticalListItem(expression.ToName());
            return AppendWord<IIndexColumnar<T>, IndexColumnar<T>>(KeywordDesc);
        }

        public IIndexColumnar<T> Columns(IEnumerable<IIndexedField> fields)
        {
            foreach (var field in fields)
            {
                AppendParentheticalListItem(field.Name);
                if (field.Ascending)
                    AppendWord(KeywordAsc);
                else
                    AppendWord(KeywordDesc);
            }

            return Transform<IIndexColumnar<T>, IndexColumnar<T>>();
        }
    }
}
