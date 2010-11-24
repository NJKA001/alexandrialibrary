﻿using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite
{
    public class SQLiteQueryMapper<T> : IQueryMapper<T>
        where T : IModel
    {
        public SQLiteQueryMapper(ISchema<T> schema, IFactory<ISelect> factory)
        {
            Schema = schema;
            Factory = factory;
        }

        #region Protected Members

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<ISelect> Factory;

        protected IStatement GetSelectAllBuilder()
        {
            return Factory.Create()
                .Distinct
                .AllColumns()
                .From(Schema.Name);
        }

        #endregion

        #region IQueryMapper Members

        public ICommand GetSelectOneCommand(object id)
        {
            var statement = Factory.Create()
                .Distinct
                .AllColumns()
                .From(Schema.Name)
                .Where<T>(x => x.Id)
                .IsEqualTo<T>(x => x.Id, id);
            return null;
        }

        public ICommand GetSelectAllCommand()
        {
            //return GetSelectAllBuilder()
                //.ToCommand();
            return null;
        }

        #endregion
    }
}