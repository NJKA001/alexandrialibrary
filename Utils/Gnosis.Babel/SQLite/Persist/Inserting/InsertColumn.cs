using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public class InsertColumn : InsertColumnar, IInsertColumn
    {
        private const string KeywordSelectAll = "select all";
        private const string KeywordSelectDistinct = "select distinct";
        private const string KeywordValues = "values";

        public ISelect SelectAll
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectAll); }
        }

        public ISelect SelectDistinct
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectDistinct); }
        }

        public IInsertValue Values()
        {
            return AppendClause<IInsertValue, InsertValue>(KeywordValues);
        }

        public IStatement Values<TModel>(TModel model, params Expression<Func<TModel, object>>[] properties)
            where TModel : IModel
        {
            AppendClause(KeywordValues);

            foreach (var property in properties)
                AppendParentheticalListItem(property.ToName(), model, property);

            return this;
        }
    }

    public class InsertColumn<T> : InsertColumnar<T>, IInsertColumn<T>
        where T : IModel
    {
        private const string KeywordSelectAll = "select all";
        private const string KeywordSelectDistinct = "select distinct";
        private const string KeywordValues = "values";

        public ISelect SelectAll
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectAll); }
        }

        public ISelect SelectDistinct
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectDistinct); }
        }

        public IInsertValue<T> Values()
        {
            return AppendClause<IInsertValue<T>, InsertValue<T>>(KeywordValues);
        }

        public IStatement Values(T model, IEnumerable<Expression<Func<T, object>>> properties)
        {
            AppendClause(KeywordValues);

            foreach (var property in properties)
                AppendParentheticalListItem<T>(property.ToName(), model, property);

            return this;
        }
    }
}
