using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class Drop : Statement, IDrop
    {
        public IStatement Index(string name)
        {
            throw new NotImplementedException();
        }

        public IStatement IndexIfExists(string name)
        {
            throw new NotImplementedException();
        }

        public IStatement Table(string name)
        {
            throw new NotImplementedException();
        }

        public IStatement TableIfExists(string name)
        {
            throw new NotImplementedException();
        }

        public IStatement Trigger(string name)
        {
            throw new NotImplementedException();
        }

        public IStatement TriggerIfExists(string name)
        {
            throw new NotImplementedException();
        }

        public IStatement View(string name)
        {
            throw new NotImplementedException();
        }

        public IStatement ViewIfExists(string name)
        {
            throw new NotImplementedException();
        }
    }
}
