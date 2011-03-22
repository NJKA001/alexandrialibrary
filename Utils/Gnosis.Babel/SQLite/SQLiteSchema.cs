using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public abstract class SQLiteSchema<T> : ISchema<T>
        where T : IModel
    {
        protected SQLiteSchema()
            : this(typeof(T).AsSchemaName())
        {
        }

        protected SQLiteSchema(string name)
        {
            _name = name;
            AddField(x => x.Id, (x, y) => x.Initialize(y));
        }

        private readonly string _name;
        private readonly IDictionary<string, IField<T>> _fields = new Dictionary<string, IField<T>>();
        private readonly IDictionary<string, IKey<T>> _keys = new Dictionary<string, IKey<T>>();

        private void AddKey(KeyType keyType, IEnumerable<IIndexedField> keyFields)
        {
            //var indexedFields = keyFields.Select<Tuple<string, bool>, IIndexedField>(x => new IndexedField(x.Item1, x.Item2));
            var keyName = _name + keyFields.Aggregate(string.Empty, (x, y) => x += "_" + y.Name) + "_key";
            _keys[keyName] = new Key<T>(keyName, keyType, keyFields);
        }

        #region Protected Methods

        protected static IIndexedField Ascending(Expression<Func<T, object>> getter)
        {
            var name = getter.ToName();
            return Ascending(name);
        }

        protected static IIndexedField Ascending(string name)
        {
            return new IndexedField(name, true);
        }

        protected static IIndexedField Descending(Expression<Func<T, object>> getter)
        {
            var name = getter.ToName();
            return Descending(name);
        }

        protected static IIndexedField Descending(string name)
        {
            return new IndexedField(name, false);
        }

        protected void AddField(Expression<Func<T, object>> getter)
        {
            AddField(getter, null);
        }

        protected void AddField(Expression<Func<T, object>> getter, Action<T, object> setter)
        {
            var name = getter.ToName();
            AddField(getter, setter, name);
        }

        protected void AddField(Expression<Func<T, object>> getter, Action<T, object> setter, string name)
        {
            _fields[name] = new Field<T>(name, getter, setter);
        }

        protected void AddUniqueKey(params IIndexedField[] keyFields)
        {
            AddKey(KeyType.UniqueKey, keyFields);
        }

        protected void AddKey(params IIndexedField[] keyFields)
        {
            AddKey(KeyType.Key, keyFields);
        }

        protected IField<T> GetField(string name)
        {
            return (_fields.ContainsKey(name))
                       ? _fields[name]
                       : null;
        }

        protected IField<T> GetField(Expression<Func<T, object>> expression)
        {
            return GetField(expression.ToName());
        }

        protected IKey<T> GetKey(string name)
        {
            return (_keys.ContainsKey(name))
                       ? _keys[name]
                       : null;
        }

        #endregion

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<IField<T>> Fields
        {
            get { return _fields.Values; }
        }

        public IEnumerable<IField<T>> NonPrimaryFields
        {
            get { return _fields.Values.Except(GetField(x => x.Id)); }
        }

        public IField<T> PrimaryField
        {
            get { return GetField(x => x.Id); }
        }

        public IEnumerable<IKey<T>> Keys
        {
            get { return _keys.Values; }
        }
    }
}
