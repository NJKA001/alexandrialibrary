using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Babel
{
    public class Command : ICommand
    {
        public Command()
        {
        }

        private readonly Guid _id = Guid.NewGuid();
        private readonly IDictionary<string, IParameter> _parameters = new Dictionary<string, IParameter>();
        private readonly IList<IStatement> _statements = new List<IStatement>();
        private Action<IModel, object> _callback;
        private IModel _model;

        private void AddParameter(IParameter parameter)
        {
            if (!_parameters.ContainsKey(parameter.Name))
                _parameters.Add(parameter.Name, parameter);
        }

        private void AddParameters(IEnumerable<IParameter> parameters)
        {
            parameters.Each(x => AddParameter(x));
        }

        public Guid Id
        {
            get { return _id; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { return _parameters.Values; }
        }

        public void AddStatement(IStatement statement)
        {
            _statements.Add(statement);
            AddParameters(statement.Parameters);
        }

        public void SetCallback(Action<IModel, object> callback, IModel model)
        {
            _callback = callback;
            _model = model;
        }

        public void InvokeCallback(object value)
        {
            if (_callback != null && _model != null)
                _callback(_model, value);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            _statements.Each(x => builder.Append(x.ToString() + Environment.NewLine));

            return builder.ToString();
        }
    }
}
