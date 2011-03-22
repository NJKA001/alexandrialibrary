using System.Collections.Generic;
using System.Data;

namespace Gnosis.Babel.SQLite
{
    public class SQLiteModelMapper<T> : IModelMapper<T>
        where T : IModel
    {
        public SQLiteModelMapper(IFactory<T> factory, ISchema<T> schema)
        {
            _factory = factory;
            _schema = schema;
        }

        private readonly IFactory<T> _factory;
        private readonly ISchema<T> _schema;

        public object GetId(IDataRecord record)
        {
            return record[_schema.PrimaryField.Name];
        }

        public T GetModel(IDataRecord record)
        {
            var model = _factory.Create();

            if (model is IImmutable)
            {
                var data = new Dictionary<string, object>();
                _schema.NonPrimaryFields.Each(x => data.Add(x.Name, record[x.Name]));
                ((IImmutable)model).Populate(data);
            }
            else
            {
                _schema.NonPrimaryFields.Each(x => x.Setter(model, record[x.Name]));
            }

            model.Initialize(record[_schema.PrimaryField.Name]);

            return model;
        }
    }
}
