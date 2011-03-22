using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public class IndexedField : IIndexedField
    {
        public IndexedField(string name, bool ascending)
        {
            _name = name;
            _ascending = ascending;
        }

        private readonly string _name;
        private readonly bool _ascending;

        public string Name
        {
            get { return _name; }
        }

        public bool Ascending
        {
            get { return _ascending; }
        }
    }
}
