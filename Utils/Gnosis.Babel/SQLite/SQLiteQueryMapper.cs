using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite
{
    public class SQLiteQueryMapper<T> : IQueryMapper<T>
        where T : IModel
    {
        public SQLiteQueryMapper(ISchema<T> schema, IFactory<ICommand> commandFactory, IFactory<ISelect> selectFactory, IFactory<IQuery<T>> queryFactory)
        {
            Schema = schema;
            CommandFactory = commandFactory;
            SelectFactory = selectFactory;
            QueryFactory = queryFactory;
        }

        #region Protected Members

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<ICommand> CommandFactory;
        protected readonly IFactory<ISelect> SelectFactory;
        protected readonly IFactory<IQuery<T>> QueryFactory;
        protected ISelect Select { get { return SelectFactory.Create(); } }

        protected ICommand GetSelectOneCommand(object id)
        {
            var command = CommandFactory.Create();

            command.AddStatement(
                Select
                .Distinct
                .AllColumns()
                .From(Schema.Name)
                .Where<T>(x => x.Id)
                .IsEqualTo<T>(x => x.Id, id)
            );

            return command;
        }

        protected ICommand GetSelectAllCommand()
        {
            var command = CommandFactory.Create();

            command.AddStatement(
                Select
                .Distinct
                .AllColumns()
                .From<T>()
            );

            return command;
        }

        #endregion

        #region IQueryMapper Members

        public IQuery<T> GetSelectOneQuery(object id)
        {
            var query = QueryFactory.Create();

            query.AddCommand(GetSelectOneCommand(id));

            return query;
        }

        public IQuery<T> GetSelectAllQuery()
        {
            var query = QueryFactory.Create();

            query.AddCommand(GetSelectAllCommand());

            return query;
        }

        #endregion
    }
}
