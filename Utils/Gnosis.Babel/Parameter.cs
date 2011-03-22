using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    #region Parameter

    public class Parameter : IParameter
    {
        public Parameter(string name, IModel model, Expression<Func<IModel, object>> property)
        {
            _name = name;
            _model = model;
            _property = property;
        }

        private readonly string _name;
        private readonly IModel _model;
        private readonly Expression<Func<IModel, object>> _property;

        public string Name
        {
            get { return _name; }
        }

        public object GetValue()
        {
            return _property.GetValue(_model).AsPersistentValue();
        }
    }

    #endregion

    #region Parameter<T>

    public class Parameter<T> : IParameter
        where T : IModel
    {
        public Parameter(string name, T model, Expression<Func<T, object>> property)
        {
            _name = name;
            _model = model;
            _property = property;
        }

        private readonly string _name;
        private readonly T _model;
        private readonly Expression<Func<T, object>> _property;

        public string Name
        {
            get { return _name; }
        }

        public object GetValue()
        {
            return _property.GetValue(_model).AsPersistentValue();
        }
    }

    #endregion

    #region SimpleParameter

    public class SimpleParameter : IParameter
    {
        public SimpleParameter(string name, object value)
        {
            _name = name;
            _value = value;
        }

        private readonly string _name;
        private readonly object _value;

        public string Name
        {
            get { return _name; }
        }

        public object GetValue()
        {
            return _value.AsPersistentValue();
        }
    }

    #endregion
}
