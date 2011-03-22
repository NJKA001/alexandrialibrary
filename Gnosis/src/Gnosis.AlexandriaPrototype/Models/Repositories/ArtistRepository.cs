using System.Collections.Generic;
using System.Linq;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;
using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Repositories
{
    public class ArtistRepository : RepositoryBase<IArtist>, IArtistRepository
    {
        public ArtistRepository(IStore store, ICache<IArtist> cache, IFactory<IArtist> factory, ISchema<IArtist> schema, ISchemaMapper<IArtist> schemaMapper, IModelMapper<IArtist> modelMapper, IPersistMapper<IArtist> persistMapper, IQueryMapper<IArtist> queryMapper, IFactory<ICommand> commandFactory, ISQLiteStatementFactory statementFactory, IFactory<IBatch> batchFactory, IFactory<IQuery<IArtist>> queryFactory)
            : base(store, cache, factory, schema, schemaMapper, modelMapper, persistMapper, queryMapper, commandFactory, statementFactory, batchFactory, queryFactory)
        {
        }

        #region Cache Methods

        private void AddArtistToCache(IArtist artist)
        {
            Cache.Put(artist.Id, artist);
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            AddArtistToCache(Artist.Unknown);
            AddArtistToCache(Artist.Various);

            var batch = BatchFactory.Create();

            var commands = new List<ICommand>();
            foreach (var artist in Cache.GetAll())
            {
                var command = CommandFactory.Create();

                command.AddStatement(
                    Insert
                    .OrReplace
                    .Into(Schema.Name)
                    .Columns(Schema.Fields.Select(x => x.Getter))
                    .Values(artist, Schema.Fields.Select(x => x.Getter))
                    );

                commands.Add(command);
            }

            commands.Each(x => batch.AddCommand(x));

            Store.Execute(batch);
        }

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            if (string.IsNullOrEmpty(search))
                return GetAll();

            var query = QueryFactory.Create();

            var command = CommandFactory.Create();
            command.AddStatement(
                    Select
                    .Distinct
                    .AllColumns()
                    .From<IArtist>()
                    .Where<IArtist>(x => x.Name).IsLike("@Name", string.Format("%{0}%", search))
                    .Or<IArtist>(x => x.NameHash).IsLike("@NameHash", string.Format("%{0}%", search.AsNameHash()))
                );

            query.AddCommand(command);

            return GetMany(query);
        }
    }
}
