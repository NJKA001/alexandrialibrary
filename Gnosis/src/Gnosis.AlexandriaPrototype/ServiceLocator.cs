using System;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Messages;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Models.Repositories;
using Gnosis.Alexandria.Models.Schemas;
using Gnosis.Alexandria.Models.Stores;
using Gnosis.Alexandria.Views;
using Gnosis.Alexandria.Views.Interfaces;

using Gnosis.Babel;
using Gnosis.Babel.SQLite;
using Gnosis.Babel.SQLite.Persist;
using Gnosis.Babel.SQLite.Persist.Deleting;
using Gnosis.Babel.SQLite.Persist.Inserting;
using Gnosis.Babel.SQLite.Persist.Updating;
using Gnosis.Babel.SQLite.Query;
using Gnosis.Babel.SQLite.Schema;
using StructureMap;
using StructureMap.Pipeline;

namespace Gnosis.Alexandria
{
    public class CoreRegistry : StructureMap.Configuration.DSL.Registry
    {
        public CoreRegistry()
        {
        }
    }

    public static class ServiceLocator
    {
        static ServiceLocator()
        {
            try {

            ObjectFactory.Initialize(x =>
                {
                    x.For<IFactory<ICommand>>().Use<GenericFactory<ICommand, Command>>();
                    x.For<ISQLiteStatementFactory>().Use<SQLiteStatementFactory>();

                    //TODO: See if there is a way to get rid of this
                    x.For<IFactory<ISelect>>().Use<GenericFactory<ISelect, Select>>();
                    x.For<IFactory<IInsert>>().Use<GenericFactory<IInsert, Insert>>();
                    x.For<IFactory<IInsert<IArtist>>>().Use<GenericFactory<IInsert<IArtist>, Insert<IArtist>>>();
                    x.For<IFactory<IInsert<ICountry>>>().Use<GenericFactory<IInsert<ICountry>, Insert<ICountry>>>();
                    x.For<IFactory<IUpdate>>().Use<GenericFactory<IUpdate, Update>>();
                    x.For<IFactory<IUpdate<IArtist>>>().Use<GenericFactory<IUpdate<IArtist>, Update<IArtist>>>();
                    x.For<IFactory<IUpdate<ICountry>>>().Use<GenericFactory<IUpdate<ICountry>, Update<ICountry>>>();
                    x.For<IFactory<IDelete>>().Use<GenericFactory<IDelete, Delete>>();
                    x.For<IFactory<IDelete<IArtist>>>().Use<GenericFactory<IDelete<IArtist>, Delete<IArtist>>>();
                    x.For<IFactory<IDelete<ICountry>>>().Use<GenericFactory<IDelete<ICountry>, Delete<ICountry>>>();
                    x.For<IFactory<ICreate>>().Use<GenericFactory<ICreate, Create>>();
                    x.For<IFactory<ICreate<IArtist>>>().Use<GenericFactory<ICreate<IArtist>, Create<IArtist>>>();
                    x.For<IFactory<ICreate<ICountry>>>().Use<GenericFactory<ICreate<ICountry>, Create<ICountry>>>();
                    x.For<IFactory<IQuery<IArtist>>>().Use<GenericFactory<IQuery<IArtist>, Query<IArtist>>>();
                    x.For<IFactory<IQuery<ICountry>>>().Use<GenericFactory<IQuery<ICountry>, Query<ICountry>>>();
                    x.For<IFactory<IBatch>>().Use<GenericFactory<IBatch, Batch>>();

                    x.For<IFactory<IArtist>>().Use<GenericFactory<IArtist, Artist>>();
                    x.For<IFactory<ICountry>>().Use<GenericFactory<ICountry, Country>>();

                    x.For<ITabController>().Use<TabController>();
                    x.For<IRepositoryController>().Use<RepositoryController>();
                    x.For<IArtistRepositoryController>().Use<ArtistRepositoryController>();
                    x.For<ICountryRepositoryController>().Use<CountryRepositoryController>();
                    x.For<IHomeTabView>().Use<HomeTabView>();
                    x.For<ISearchTabView>().Use<SearchTabView>();
                    
                    x.For<IModelMapper<IArtist>>().Use<SQLiteModelMapper<IArtist>>();
                    x.For<IModelMapper<ICountry>>().Use<SQLiteModelMapper<ICountry>>();
                    x.For<IPersistMapper<ICountry>>().Use<SQLitePersistMapper<ICountry>>();
                    x.For<IPersistMapper<IArtist>>().Use<SQLitePersistMapper<IArtist>>();
                    x.For<IQueryMapper<ICountry>>().Use<SQLiteQueryMapper<ICountry>>();
                    x.For<IQueryMapper<IArtist>>().Use<SQLiteQueryMapper<IArtist>>();
                    x.For<ISchemaMapper<ICountry>>().Use<SQLiteSchemaMapper<ICountry>>();
                    x.For<ISchemaMapper<IArtist>>().Use<SQLiteSchemaMapper<IArtist>>();
                    x.For<ISchema<ICountry>>().Use<CountrySchema>();
                    x.For<ISchema<IArtist>>().Use<ArtistSchema>();
                    x.For<ICache<ICountry>>().Use<StaticCache<ICountry>>();
                    x.For<ICache<IArtist>>().Use<StaticCache<IArtist>>();
                    x.For<IStore>().Use<CatalogStore>();
                    x.For<ICountryRepository>().Use<CountryRepository>();
                    x.For<IArtistRepository>().Use<ArtistRepository>();
                    
                    x.For<INewHomeTabRequestedHandler>().Use<NewHomeTabRequestedHandler>();
                    x.For<INewSearchTabRequestedHandler>().Use<NewSearchTabRequestedHandler>();
                    x.For<ISearchRequestedHandler>().Use<SearchRequestedHandler>();
                    x.For<IInitializeRepositoriesHandler>().AlwaysUnique().Use<InitializeRepositoriesHandler>();
                    
                    x.For<INewHomeTabRequestedMessage>().Use<NewHomeTabRequestedMessage>();
                    x.For<INewSearchTabRequestedMessage>().Use<NewSearchTabRequestedMessage>();
                    x.For<ISearchRequestedMessage>().Use<SearchRequestedMessage>();
                    x.For<IInitializeRepositoriesMessage>().Use<InitializeRepositoriesMessage>();
                }
            );
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        public static T GetObject<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
