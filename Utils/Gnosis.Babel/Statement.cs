using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public abstract class Statement : IStatement
    {
        protected Statement()
        {
            _text = new StringBuilder();
        }

        protected Statement(string value)
        {
            _text = new StringBuilder(value);
        }

        protected Statement(string value, IEnumerable<IParameter> parameters)
        {
            _text = new StringBuilder(value);

            AddParameters(parameters);
        }

        private readonly StringBuilder _text;
        private readonly IDictionary<string, IParameter> _parameters = new Dictionary<string, IParameter>();
        private bool _listMode = false;
        private bool _subListMode = false;
        private bool _hasParentheses = false;
        private bool _hasSubParentheses = false;

        private static string GetParameterName(string name)
        {
            if (!name.StartsWith("@"))
                return string.Format("@{0}", name);

            return name;
        }

        private void AddSimpleParameter(string name, object value)
        {
            AddParameter(new SimpleParameter(name, value));
        }

        private void AddParameter(string name, IModel model, Expression<Func<IModel, object>> property)
        {
            AddParameter(new Parameter(name, model, property));
        }

        private void AddParameter<T>(string name, T model, Expression<Func<T, object>> property)
            where T : IModel
        {
            AddParameter(new Parameter<T>(name, model, property));
        }

        private void AddParameter(IParameter parameter)
        {
            if (!_parameters.ContainsKey(parameter.Name))
                _parameters.Add(parameter.Name, parameter);
        }

        private void AddParameters(IEnumerable<IParameter> parameters)
        {
            parameters.Each(x => AddParameter(x));
        }

        private Tuple<int, int> GetNumberOfOpenAndClosedParentheses()
        {
            var openCount = 0;
            var closedCount = 0;
            foreach (var character in _text.ToString().ToCharArray())
            {
                if (character == '(') openCount++;
                else if (character == ')') closedCount++;
            }

            return new Tuple<int, int>(openCount, closedCount);
        }

        private bool HasOpenParentheses
        {
            get
            {
                var count = GetNumberOfOpenAndClosedParentheses();
                return (count.Item1 > count.Item2);
            }
        }

        #region Fluent Append Methods

        protected TInterface Transform<TInterface, TConcrete>()
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();

            //TODO: Refactor this - transfering object state like this is *UGLY*
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete._listMode = _listMode;
            concrete._hasParentheses = _hasParentheses;
            concrete._subListMode = _subListMode;
            concrete._hasSubParentheses = _hasSubParentheses;

            return concrete;
        }

        protected TInterface AppendWord<TInterface, TConcrete>(string word)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendWord(word);

            return concrete;
        }

        protected TInterface AppendClause<TInterface, TConcrete>(string clause)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendClause(clause);

            return concrete;
        }

        protected TInterface AppendListItem<TInterface, TConcrete>(string item)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendListItem(item);

            return concrete;
        }

        protected TInterface AppendParentheticalListItem<TInterface, TConcrete>(string item)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParentheticalListItem(item);

            return concrete;
        }

        protected TInterface AppendParentheticalListItem<TInterface, TConcrete, TModel>(string name, TModel model, Expression<Func<TModel, object>> property)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
            where TModel : IModel
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParentheticalListItem<TModel>(name, model, property);

            return concrete;
        }

        protected TInterface AppendParentheticalSubListItem<TInterface, TConcrete>(string item)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParentheticalSubListItem(item);

            return concrete;
        }

        protected TInterface AppendSimpleParameter<TInterface, TConcrete>(string name, object value)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendSimpleParameter(name, value);

            return concrete;
        }

        protected TInterface AppendParameter<TInterface, TConcrete, TModel>(string name, TModel model, Expression<Func<TModel, object>> property)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
            where TModel : IModel
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters.Values);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParameter<TModel>(name, model, property);

            return concrete;
        }

        #endregion

        #region Regular Append Methods

        protected void AppendWord(string word)
        {
            if (_subListMode && _hasSubParentheses)
                _text.Append(")");

            if (_text.Length > 0)
                _text.AppendFormat(" {0}", word);
            else _text.Append(word);

            _subListMode = false;
            _hasSubParentheses = false;
        }

        protected void AppendListItem(string item)
        {
            if (_listMode)
                _text.AppendFormat(", {0}", item);
            else if (_text.Length > 0)
                _text.AppendFormat(" {0}", item);
            else
                _text.Append(item);

            _listMode = true;
            _hasParentheses = false;
        }

        protected void AppendClause(string clause)
        {
            if (_subListMode && _hasSubParentheses)
                _text.Append(")");

            if (_listMode && _hasParentheses)
                _text.Append(")");

            if (_text.Length > 0)
                _text.AppendFormat(" {0}", clause);
            else
                _text.Append(clause);

            _listMode = false;
            _hasParentheses = false;
            _subListMode = false;
            _hasSubParentheses = false;
        }

        protected void AppendSimpleParameter(string name, object value)
        {
            var parameterName = GetParameterName(name);

            AppendWord(parameterName);
            AddSimpleParameter(parameterName, value);
        }

        protected void AppendParameter(string name, IModel model, Expression<Func<IModel, object>> property)
        {
            var parameterName = GetParameterName(name);

            AppendWord(parameterName);
            AddParameter(parameterName, model, property);
        }

        protected void AppendParameter<T>(string name, T model, Expression<Func<T, object>> property)
            where T : IModel
        {
            var parameterName = GetParameterName(name);

            AppendWord(parameterName);
            AddParameter<T>(parameterName, model, property);
        }

        protected void AppendParentheticalListItem(string item)
        {
            if (_subListMode)
            {
                _text.Append(")");
                _subListMode = false;
                _hasSubParentheses = false;
            }

            if (_listMode || HasOpenParentheses)
                _text.AppendFormat(", {0}", item);
            else
                _text.AppendFormat(" ({0}", item);

            _listMode = true;
            _hasParentheses = true;
        }

        protected void AppendParentheticalListItem<TModel>(string name, TModel model, Expression<Func<TModel, object>> property)
            where TModel : IModel
        {
            if (_subListMode)
            {
                _text.Append(")");
                _subListMode = false;
                _hasSubParentheses = false;
            }

            if (_listMode || HasOpenParentheses)
                _text.Append(", ");
            else
                _text.Append(" (");

            AppendParameter<TModel>(name, model, property);

            _listMode = true;
            _hasParentheses = true;
        }

        protected void AppendParentheticalSubListItem(string item)
        {
            if (_subListMode)
                _text.AppendFormat(", {0}", item);
            else
                _text.AppendFormat(" ({0}", item);

            _subListMode = true;
            _hasSubParentheses = true;
        }

        #endregion

        public IEnumerable<IParameter> Parameters
        {
            get { return _parameters.Values; }
        }

        public override string ToString()
        {
            var suffix = string.Empty;
            var count = GetNumberOfOpenAndClosedParentheses();
            var closed = 0;

            while (count.Item1 > count.Item2 + closed)
            {
                suffix += ")";
                closed++;
            }

            return _text.ToString() + suffix + ";";
        }
    }
}
