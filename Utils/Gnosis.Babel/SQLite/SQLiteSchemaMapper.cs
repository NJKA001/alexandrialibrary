using System;
using System.Collections.Generic;

using Gnosis.Babel.SQLite.Schema;

namespace Gnosis.Babel.SQLite
{
    public class SQLiteSchemaMapper<T> : ISchemaMapper<T>
        where T : IModel
    {
        public SQLiteSchemaMapper(ISchema<T> schema, IFactory<T> modelFactory, IFactory<ICommand> commandFactory, IFactory<ICreate<T>> createStatementFactory, IFactory<IBatch> batchFactory)
        {
            Schema = schema;
            ModelFactory = modelFactory;
            CommandFactory = commandFactory;
            CreateStatementFactory = createStatementFactory;
            BatchFactory = batchFactory;
        }

        #region Protected Members

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<T> ModelFactory;
        protected readonly IFactory<ICommand> CommandFactory;
        protected readonly IFactory<ICreate<T>> CreateStatementFactory;
        protected readonly IFactory<IBatch> BatchFactory;

        protected virtual IStatement GetCreateTableStatment()
        {
            var model = ModelFactory.Create();

            var createTable = CreateStatementFactory.Create()
                .TableIfNotExists(Schema.Name)
                .Column(Schema.PrimaryField.Getter).Integer.NotNull.PrimaryKeyAsc;

            foreach (var field in Schema.NonPrimaryFields)
            {
                var value = field.Getter.GetValue(model);

                createTable = createTable
                    .Column(field.Getter, value.AsAffinity())
                    .NotNull
                    .Default(value);
            }

            return createTable;
        }

        protected IStatement GetCreateSecondaryKey(IKey<T> key)
        {
            return CreateStatementFactory.Create()
                .IndexIfNotExists(key.Name)
                .On(Schema.Name)
                .Columns(key.Fields);
        }

        protected IStatement GetCreateUniqueKey(IKey<T> key)
        {
            var statement = CreateStatementFactory.Create()
                .UniqueIndexIfNotExists(key.Name)
                .On(Schema.Name)
                .Columns(key.Fields);

            return statement;
        }

        protected virtual IStatement GetCreateIndexStatement(IKey<T> key)
        {
            switch (key.KeyType)
            {
                case KeyType.Key:
                    return GetCreateSecondaryKey(key);
                case KeyType.UniqueKey:
                    return GetCreateUniqueKey(key);
                default:
                    throw new InvalidOperationException("Could not generate create index statement for key: invalid key type");
            }
        }

        protected IEnumerable<ICommand> GetInitializeCommands()
        {
            var commands = new List<ICommand>();

            var command = CommandFactory.Create();
            command.AddStatement(GetCreateTableStatment());

            foreach (var key in Schema.Keys)
                command.AddStatement(GetCreateIndexStatement(key));

            commands.Add(command);

            return commands;
        }

        #endregion

        public IBatch GetInitializeBatch()
        {
            var batch = BatchFactory.Create();

            GetInitializeCommands().Each(x => batch.AddCommand(x));

            return batch;
        }
    }
}
