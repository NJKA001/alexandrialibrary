using System.Collections.Generic;
using System.Linq;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;
using Gnosis.Babel.SQLite.Persist.Deleting;
using Gnosis.Babel.SQLite.Persist.Inserting;
using Gnosis.Babel.SQLite.Persist.Updating;
using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Alexandria.Models.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IModel
    {
        protected RepositoryBase(IStore store, ICache<T> cache, IFactory<T> factory, ISchema<T> schema, ISchemaMapper<T> schemaMapper, IModelMapper<T> modelMapper, IPersistMapper<T> persistMapper, IQueryMapper<T> queryMapper, IFactory<ICommand> commandFactory, ISQLiteStatementFactory statementFactory, IFactory<IBatch> batchFactory, IFactory<IQuery<T>> queryFactory)
        {
            Store = store;
            Cache = cache;
            Factory = factory;
            Schema = schema;
            SchemaMapper = schemaMapper;
            ModelMapper = modelMapper;
            PersistMapper = persistMapper;
            QueryMapper = queryMapper;
            CommandFactory = commandFactory;
            StatementFactory = statementFactory;
            BatchFactory = batchFactory;
            QueryFactory = queryFactory;
        }

        #region Protected Members

        protected readonly IStore Store;
        protected readonly ICache<T> Cache;
        protected readonly IFactory<T> Factory;
        protected readonly ISchema<T> Schema;
        protected readonly ISchemaMapper<T> SchemaMapper;
        protected readonly IModelMapper<T> ModelMapper;
        protected readonly IPersistMapper<T> PersistMapper;
        protected readonly IQueryMapper<T> QueryMapper;
        protected readonly IFactory<ICommand> CommandFactory;
        protected readonly ISQLiteStatementFactory StatementFactory;
        protected readonly IFactory<IBatch> BatchFactory;
        protected readonly IFactory<IQuery<T>> QueryFactory;

        protected IInsert<T> Insert { get { return StatementFactory.Insert<T>(); } }
        protected IUpdate<T> Update { get { return StatementFactory.Update<T>(); } }
        protected IDelete<T> Delete { get { return StatementFactory.Delete<T>(); } }
        protected ISelect Select { get { return StatementFactory.Select(); } }

        #endregion

        #region IRepository Members

        public virtual void Initialize()
        {
            var batch = SchemaMapper.GetInitializeBatch();

            Store.Execute(batch);
        }

        public virtual void Persist(T model)
        {
            Persist(new List<T> { model });
        }

        public virtual void Persist(IEnumerable<T> models)
        {
            var batches = new List<IBatch>();

            models.Each(x => batches.Add(PersistMapper.GetPersistBatch(x)));

            Store.Execute(batches);
        }
        
        public virtual T GetOne(object id)
        {
            var query = QueryMapper.GetSelectOneQuery(id);

            var many = GetMany(query);

            return many.FirstOrDefault();
        }

        public virtual ICollection<T> GetMany(IQuery<T> query)
        {
            return Store.Execute<T>(query);
        }

        public virtual ICollection<T> GetAll()
        {
            var query = QueryMapper.GetSelectAllQuery();

            return GetMany(query);
        }

        #endregion
    }
}
