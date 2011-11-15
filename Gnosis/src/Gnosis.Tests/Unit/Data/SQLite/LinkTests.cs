using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Links;
using Gnosis.Links.Html;
using Gnosis.Utilities;
using Gnosis.Data;
using Gnosis.Data.SQLite;

namespace Gnosis.Tests.Unit.Data.SQLite
{
    public abstract class LinkTestBase
    {
        protected LinkTestBase()
        {
            connection = new SQLiteConnectionFactory().Create("Data Source=:memory:;Version=3;");
            connection.Open();
            repository = new SQLiteLinkRepository(logger, typeFactory, connection);
            repository.Initialize();

            link1 = new Link(source1, target1, type5, name1);
            link2 = new Link(source1, target2, type2, name2);
            link3 = new Link(source3, target3, type3, name3);
            link4 = new Link(source4, target4, type4, name4);
            link5 = new Link(source5, target2, type5, name5);
            link6 = new Link(source1, target6, type6, name6);
            link7 = new Link(source7, target7, type7, name7);
            link8 = new Link(source5, target2, type5, name8);
            link9 = new Link(source5, target2, type9, name9);
            link10 = new Link(source5, target10, type10, name10);
            link11 = new Link(source11, target11, type11, name11);
            link12 = new Link(source12, target2, type5, name12);
            link13 = new Link(source13, target13, type13, name13);
            link14 = new Link(source14, target14, type14, name14);
            link15 = new Link(source15, target15, type15, name15);

            links.Add(link1);
            links.Add(link2);
            links.Add(link3);
            links.Add(link4);
            links.Add(link5);
            links.Add(link6);
            links.Add(link7);
            links.Add(link8);
            links.Add(link9);
            links.Add(link10);
            links.Add(link11);
            links.Add(link12);
            links.Add(link13);
            links.Add(link14);
            links.Add(link15);
        }

        private readonly IDbConnection connection;
        private readonly ILinkTypeFactory typeFactory = new LinkTypeFactory();
        protected readonly ILogger logger = new DebugLogger();
        protected readonly ILinkRepository repository;

        #region Test Values

        protected readonly Uri source1 = new Uri("http://example.com/sources/1");
        protected readonly Uri source2 = new Uri("http://example.com/sources/2");
        protected readonly Uri source3 = new Uri("http://example.com/sources/3");
        protected readonly Uri source4 = new Uri("http://example.com/sources/4");
        protected readonly Uri source5 = new Uri("http://example.com/sources/5");
        protected readonly Uri source6 = new Uri("http://example.com/sources/6");
        protected readonly Uri source7 = new Uri("http://example.com/sources/7");
        protected readonly Uri source8 = new Uri("http://example.com/sources/8");
        protected readonly Uri source9 = new Uri("http://example.com/sources/9");
        protected readonly Uri source10 = new Uri("http://example.com/sources/10");
        protected readonly Uri source11 = new Uri("http://example.com/sources/11");
        protected readonly Uri source12 = new Uri("http://example.com/sources/12");
        protected readonly Uri source13 = new Uri("http://example.com/sources/13");
        protected readonly Uri source14 = new Uri("http://example.com/sources/14");
        protected readonly Uri source15 = new Uri("http://example.com/sources/15");

        protected readonly Uri target1 = new Uri("http://example.com/targets/1");
        protected readonly Uri target2 = new Uri("http://example.com/targets/2");
        protected readonly Uri target3 = new Uri("http://example.com/targets/3");
        protected readonly Uri target4 = new Uri("http://example.com/targets/4");
        protected readonly Uri target5 = new Uri("http://example.com/targets/5");
        protected readonly Uri target6 = new Uri("http://example.com/targets/6");
        protected readonly Uri target7 = new Uri("http://example.com/targets/7");
        protected readonly Uri target8 = new Uri("http://example.com/targets/8");
        protected readonly Uri target9 = new Uri("http://example.com/targets/9");
        protected readonly Uri target10 = new Uri("http://example.com/targets/10");
        protected readonly Uri target11 = new Uri("http://example.com/targets/11");
        protected readonly Uri target12 = new Uri("http://example.com/targets/12");
        protected readonly Uri target13 = new Uri("http://example.com/targets/13");
        protected readonly Uri target14 = new Uri("http://example.com/targets/14");
        protected readonly Uri target15 = new Uri("http://example.com/targets/15");

        protected readonly ILinkType type1 = LinkType.Default;
        protected readonly ILinkType type2 = LinkType.AlbumThumbnail;
        protected readonly ILinkType type3 = LinkType.Default;
        protected readonly ILinkType type4 = LinkType.AlbumThumbnail;
        protected readonly ILinkType type5 = LinkType.AlbumThumbnail;
        protected readonly ILinkType type6 = LinkType.AlbumThumbnail;
        protected readonly ILinkType type7 = LinkType.AlbumThumbnail;
        protected readonly ILinkType type8 = LinkType.AlbumThumbnail;
        protected readonly ILinkType type9 = LinkType.Default;
        protected readonly ILinkType type10 = LinkType.AlbumThumbnail;
        protected readonly ILinkType type11 = HtmlLinkType.Alternate;
        protected readonly ILinkType type12 = HtmlLinkType.NoFollow;
        protected readonly ILinkType type13 = HtmlLinkType.Chapter;
        protected readonly ILinkType type14 = HtmlLinkType.Chapter;
        protected readonly ILinkType type15 = HtmlLinkType.Chapter;

        protected readonly string name1 = "Abe";
        protected readonly string name2 = "Betty";
        protected readonly string name3 = "Carl";
        protected readonly string name4 = "Daria";
        protected readonly string name5 = "Edgar";
        protected readonly string name6 = "Francine";
        protected readonly string name7 = "George";
        protected readonly string name8 = "Harriet";
        protected readonly string name9 = "Irwin";
        protected readonly string name10 = "Jenny";
        protected readonly string name11 = "Kurt";
        protected readonly string name12 = "Laura";
        protected readonly string name13 = "Morris";
        protected readonly string name14 = "Naomi";
        protected readonly string name15 = "Orson";

        protected readonly ILink link1;
        protected readonly ILink link2;
        protected readonly ILink link3;
        protected readonly ILink link4;
        protected readonly ILink link5;
        protected readonly ILink link6;
        protected readonly ILink link7;
        protected readonly ILink link8;
        protected readonly ILink link9;
        protected readonly ILink link10;
        protected readonly ILink link11;
        protected readonly ILink link12;
        protected readonly ILink link13;
        protected readonly ILink link14;
        protected readonly ILink link15;

        protected readonly IList<ILink> links = new List<ILink>(); 

        #endregion

        protected void Cleanup()
        {
            connection.Close();
        }
    }

    [TestFixture]
    public class SavedLinks : LinkTestBase
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            repository.Save(links);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Cleanup();
        }

        [Test]
        public void CanBeReadById()
        {
            var byId = repository.GetById(1);
            Assert.IsNotNull(byId);
            Assert.AreEqual(1, byId.Id);
            Assert.AreEqual(link1.Source.ToString(), byId.Source.ToString());
            Assert.AreEqual(link1.Target.ToString(), byId.Target.ToString());
            Assert.AreEqual(link1.Type, byId.Type);
            Assert.AreEqual(link1.Name, byId.Name);
        }

        [Test]
        public void CanBeOverwrittenById()
        {
            const string name = "This link name is different";
            const long id = 15;
            var byId = repository.GetById(id);
            Assert.IsNotNull(byId);
            Assert.AreEqual(id, byId.Id);
            Assert.AreNotEqual(HtmlLinkType.Glossary, byId.Type);
            Assert.AreNotEqual(name, byId.Name);
            var different = new Link(byId.Source, byId.Target, HtmlLinkType.Glossary, name, id);
            repository.Save(new List<ILink> { different });
            var check = repository.GetById(id);
            Assert.IsNotNull(check);
            Assert.AreEqual(id, check.Id);
            Assert.AreEqual(name, check.Name);
            Assert.AreEqual(HtmlLinkType.Glossary, check.Type);
        }

        [Test]
        public void CanBeDeleted()
        {
            const long id1 = 13;
            const long id2 = 14;
            var byId1 = repository.GetById(id1);
            Assert.IsNotNull(byId1);
            var byId2 = repository.GetById(id2);
            Assert.IsNotNull(byId2);

            repository.Delete(new List<long> { id1, id2 });

            var check1 = repository.GetById(id1);
            Assert.IsNull(check1);
            var check2 = repository.GetById(id2);
            Assert.IsNull(check2);
        }

        [Test]
        public void CanBeReadBySource()
        {
            var bySource = repository.GetBySource(source1);
            Assert.IsNotNull(bySource);
            Assert.AreEqual(links.Where(x => x.Source.ToString() == source1.ToString()).Count(), bySource.Count());
        }

        [Test]
        public void CanBeReadBySourceAndType()
        {
            var bySource = repository.GetBySource(source1, type6);
            Assert.IsNotNull(bySource);
            Assert.AreEqual(links.Where(x => x.Source.ToString() == source1.ToString() && x.Type == type6).Count(), bySource.Count());
        }

        [Test]
        public void CanBeReadByTarget()
        {
            var byTarget = repository.GetByTarget(target2);
            Assert.IsNotNull(byTarget);
            Assert.AreEqual(links.Where(x => x.Target.ToString() == target2.ToString()).Count(), byTarget.Count());
        }

        [Test]
        public void CanBeReadByTargetAndType()
        {
            var byTarget = repository.GetByTarget(target2, type5);
            Assert.IsNotNull(byTarget);
            Assert.AreEqual(links.Where(x => x.Target.ToString() == target2.ToString() && x.Type == type5).Count(), byTarget.Count());
        }

        [Test]
        public void CanBeReadBySourceAndTarget()
        {
            var bySourceAndTarget = repository.GetBySourceAndTarget(source5, target2);
            Assert.IsNotNull(bySourceAndTarget);
            Assert.AreEqual(links.Where(x => x.Source.ToString() == source5.ToString() && x.Target.ToString() == target2.ToString()).Count(), bySourceAndTarget.Count());
        }

        [Test]
        public void CanBeReadBySourceAndTargetAndType()
        {
            var bySourceAndTarget = repository.GetBySourceAndTarget(source5, target2, type5);
            Assert.IsNotNull(bySourceAndTarget);
            Assert.AreEqual(links.Where(x => x.Source.ToString() == source5.ToString() && x.Target.ToString() == target2.ToString() && x.Type == type5).Count(), bySourceAndTarget.Count());
        }
    }
}
