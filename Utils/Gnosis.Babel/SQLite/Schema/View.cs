using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Schema
{
    public class View : Statement, IView
    {
        private const string KeywordAsSelectAll = "as select all";
        private const string KeywordAsSelectDistinct = "as select distinct";

        public ISelect AsSelectAll
        {
            get { return AppendClause<ISelect, Select>(KeywordAsSelectAll); }
        }

        public ISelect AsSelectDistinct
        {
            get { return AppendClause<ISelect, Select>(KeywordAsSelectDistinct); }
        }
    }

    public class View<T> : Statement, IView<T>
    {
        private const string KeywordAsSelectAll = "as select all";
        private const string KeywordAsSelectDistinct = "as select distinct";

        public ISelect AsSelectAll
        {
            get { return AppendClause<ISelect, Select>(KeywordAsSelectAll); }
        }

        public ISelect AsSelectDistinct
        {
            get { return AppendClause<ISelect, Select>(KeywordAsSelectDistinct); }
        }
    }
}
