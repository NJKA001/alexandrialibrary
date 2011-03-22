using System;
using System.Collections.Generic;

namespace Gnosis.Babel
{
    public class Key<T> : IKey<T>
        where T : IModel
    {
        public Key(string name, KeyType keyType, IEnumerable<IIndexedField> fields)
        {
            _name = name;
            _keyType = keyType;
            _fields = fields;
        }

        private readonly string _name;
        private readonly KeyType _keyType;
        private readonly IEnumerable<IIndexedField> _fields;

        public string Name
        {
            get { return _name; }
        }

        public KeyType KeyType
        {
            get { return _keyType; }
        }

        public IEnumerable<IIndexedField> Fields
        {
            get { return _fields; }
        }
    }
}
