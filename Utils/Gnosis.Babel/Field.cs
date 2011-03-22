using System;
using System.Linq.Expressions;

namespace Gnosis.Babel
{
    public class Field<T> : IField<T>
        where T : IModel
    {
        public Field(string name, Expression<Func<T, object>> getter, Action<T, object> setter)
        {
            _name = name;
            _getter = getter;
            _setter = setter;
        }

        private readonly string _name;
        private readonly Expression<Func<T, object>> _getter;
        private readonly Action<T, object> _setter;

        public string Name
        {
            get { return _name; }
        }

        public Expression<Func<T, object>> Getter
        {
            get { return _getter; }
        }

        public Action<T, object> Setter
        {
            get { return _setter; }
        }
    }
}
