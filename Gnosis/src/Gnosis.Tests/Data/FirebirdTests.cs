using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Loggers;
using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Data.Firebird;

using NUnit.Framework;

namespace Gnosis.Tests.Data
{
    [TestFixture]
    public class FirebirdTests
    {
        public FirebirdTests()
        {
            tagRepository = new FirebirdTagRepository(logger);
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly FirebirdTagRepository tagRepository;
        //private readonly IConnectionFactory factory = new FirebirdConnectionFactory();
        //private const string databaseFile = @".\ALEXANDRIA.FDB";
        //private const string connectionString = @"Database=.\ALEXANDRIA.FDB;ServerType=1;User='';Password='';Charset=UTF8";

        /*
        [Test]
        public void TestConnection()
        {

            var file = new System.IO.FileInfo(databaseFile);
            if (!file.Exists)
            {
                System.Diagnostics.Debug.WriteLine("Creating Firebird database: " + file.FullName);
                factory.CreateDatabase(connectionString);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Database already exists");
            }

            try
            {
                using (var connection = factory.Create(connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                        @"EXECUTE BLOCK AS BEGIN
if (not exists(select 1 from rdb$relations where rdb$relation_name = 'FOO')) then 
execute statement 'create table Foo (Id integer not null primary key, Name varchar(100) not null unique, Score decimal, Born date);';
END";
                    command.ExecuteNonQuery();

                    var query = connection.CreateCommand();
                    query.CommandText = "select count(*) from Foo;";
                    var result = query.ExecuteScalar();
                    Assert.IsNotNull(result);
                    System.Diagnostics.Debug.WriteLine(result.ToString());

                    if (Convert.ToInt32(result.ToString()) == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("table is empty");

                        var insert = connection.CreateCommand();
                        insert.CommandText = "insert into Foo (Id, Name, Score) values (1, 'Bob Smith', 3.7);";
                        insert.ExecuteNonQuery();
                    }

                    var result2 = query.ExecuteScalar();
                    Assert.IsNotNull(result2);
                    Assert.AreEqual(1, Convert.ToInt32(result2.ToString()));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
        */

        [Test]
        public void TagRepositoryInitializeTest()
        {
            tagRepository.Initialize();
        }

        [Test]
        public void TagRepositorySaveTest()
        {
            var tag1 = new Tag(new Uri("http://arstechnica.com/index.ars"), Algorithm.Default, TagType.GeneralTagType, "Sample Tag #1");
            var tag2 = new Tag(new Uri(@"C:\Users\dpoage\Music\Queen\bicycle.mp3"), Algorithm.Default, TagType.Id3v2ArtistTagType, "Queen");

            tagRepository.Save(new List<ITag> { tag1, tag2 });
        }

        [Test]
        public void TagRepositoryCountTest()
        {
            var count = tagRepository.Count();
            Assert.IsTrue(count > 0);

            var tags = tagRepository.Search();
            Assert.IsNotNull(tags);
            Assert.IsTrue(tags.Count() > 0);
            Assert.IsTrue(tags.All(x => x.Id > 0));
        }
    }
}
