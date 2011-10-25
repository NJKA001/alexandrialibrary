using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Culture;
using Gnosis.Document;
using Gnosis.Document.Xml;
using Gnosis.Document.Xml.Atom;

namespace Gnosis.Tests.Document.Xml
{
    [TestFixture]
    public class AtomDocuments
    {
        private void MakeAtomFeedAssertions(IXmlElement document)
        {
            #region Constants

            const string authorName = "David";
            const string authorUri = "http://www.blogger.com/profile/06751101786776663258";
            const string authorEmail = "noreply@blogger.com";
            const string cat1Term = "Blogs/News";
            const string cat1Scheme = "http://example.com/schemes/categories";
            const string cat1Label = "<Blogs & News>";
            const string contribName = "Bob Lawbla";
            const string contribUri = "http://www.blogger.com/profile/06751101786776661148";
            const string contribEmail = "bob@lawbla.com";
            const string genUri = "http://www.blogger.com/";
            const string genName = "Blogger";
            const string iconUri = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/PNG_transparency_demonstration_2.png/280px-PNG_transparency_demonstration_1.png";
            const string id = "tag:blogger.com,1999:blog-8677504";
            const string link1Rel = "http://schemas.google.com/g/2005#feed";
            var link1Type = MediaType.ApplicationAtomXml;
            const string logoUri = "http://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/PNG_transparency_demonstration_2.png/280px-PNG_transparency_demonstration_2.png";
            var feedLang = LanguageTag.Parse("en-GB");
            const string rightsBaseId = "http://example.co.uk/license";
            var rightsLang = LanguageTag.Parse("en-GB");
            var rightsType = AtomTextType.xhtml;
            //var rightsText = "\r\n    <xhtml:div xmlns:xhtml=\"http://www.w3.org/2000/xhtml\">Oy Guv&apos;na, no rights! \r\n      <xhtml:em> &lt; </xhtml:em>\r\n    </xhtml:div>";
            //var rightsTextEscaped = "\r\n    <xhtml:div xmlns:xhtml=\"http://www.w3.org/2000/xhtml\">Oy Guv&apos;na, no rights! \r\n      \r\n      <xhtml:em> &lt; </xhtml:em>\r\n    </xhtml:div>";
            
            //"<xhtml:div>Oy Guv'na, no rights! <xhtml:em> &lt; </xhtml:em></xhtml:div>";
            //"Oy, Guv'nor you ain't got NO right'a be readin' 'is!";
            const string subtitle = "Qué barbaridad!";
            var subtitleLang = LanguageTag.Parse("es-MX");
            const string title = "A Bear Librarians's Cave";
            var titleType = AtomTextType.text;
            var updated = new DateTime(2011, 6, 21, 9, 21, 56, 27); //2011-06-21T02:21:56.027-07:00
            //const string lastExtPrefix = "openSearch";
            //const string lastExtName = "itemsPerPage";
            //const string lastExtContent = ">25</";

            const string entryAuthorEmail = "noreply@blogger.com";
            //const string entryAuthorExtPrefix = "gd";
            //const string entryAuthorExtName = "extendedProperty";
            //const string entryAuthorExtContent = "<gd:extendedProperty xmlns:gd=\"http://schemas.google.com/g/2005\" name=\"OpenSocialUserId\" value=\"12777363245656199557\" />";
            const string entryCategoryTerm = "Comedy";
            const string entryContentText = "Ok, so here we go again.  I really like to write, but I just never seem to find the time to stay focused.  Right now I&apos;m typing this entry as one of my classes is taking a test.  I&apos;m working full-time as a librarian and teaching three classes in the evening.  It&apos;s still fun to teach, but it&apos;s also quite tiring.  I never knew how draining it was to perform like that three nights a week.  I can only imagine how it is for comedians and other performers.  I really do feel like what I do is part education and part entertainment.  I know how bored I was in school, so I like to keep my classes a bit more comical and on the entertaining side.  This is helpful, especially when teaching night classes!!&lt;br /&gt;&lt;br /&gt;Work still basically is a give and take proposition.  As I have stated before, I really like being a librarian, I just don&apos;t necessarily like where I am librarianing at! (Please pardon the grammatical diversion)  I have been interviewing for multiple jobs mainly to keep my feet wet and my intellect stimulated, but there are times I really want to stay where I am and finish the projects I&apos;ve started.  However, if I keep getting crappy emails from people at work, I&apos;m going to have to get going!  It rarely happens, and honestly this is really the first time I&apos;ve ever heard anyone complain.  However, they intimated that others were dissatisfied with the time it sometimes takes to get materials to them.  I am trying to rework the tracking of purchases at work to stay on top of things, but sometimes I have no control over timeframes.  One of the shipping departments at a specific location lost a box of five rather large books, but I didn&apos;t hear about it for 6 months.  Now I&apos;ve got someone complaining that I didn&apos;t get them some books, which I am sure that I ordered, in three weeks.  I am glad that they notified me, but they were pretty harsh with the wording.  I am doing my best with minimal support and understanding.  I mean, it&apos;s not like I&apos;m one person with two assistants trying to support the information needs of a 4000 employee company or anything.  Not to mention that I&apos;m trying to drag my portion of the information network into the 21st century all while the bloody IT department is trying to take over the company and decided who gets to do what.  For goddesses sake, they don&apos;t even have an OPAC!  I&apos;m just trying to make things better and all I get is, sorry, but your little library project isn&apos;t worthwhile, now go back and shelve some books.  I think that&apos;s the biggest problem I have.  Only a very few people respect what I do, and even fewer understand it.  The fact that I report to a pharmacist who only works part-time speaks volumes about how they value my position.  Don&apos;t get me wrong, I am happy with my salary, and I love my co-workers, but there is little or no validation of me as an employee.  No one truly seems to understand what I can do for the company, or what the library can do for that matter.&lt;br /&gt;&lt;br /&gt;That kind of shit really pisses me off, and no matter how many people I talk to, no matte how may emails I send out, and no matter how much I plan improvements, nothing seems to really matter.  Perhaps that is just the way it is at most jobs, but I can&apos;t help but feel that there is a job out there where I can feel at least moderately validated as a thinking, breathing human being.  I know that my job at the hospital back in Kansas City gave me a great sense of validation.  I was frequently asked to work on committees, and to share my knowledge and experience in new and interesting ways.  People frequently sought my insight on various problems, library-related or otherwise.  I truly felt like a valuable member of the team.  Now I just feel like a lump of flesh doing just enough to get by and get a raise every year.&lt;br /&gt;&lt;br /&gt;Thank goodness I do at least have the diversion of teaching, or I will as long as I go and get a bloody TB test this week.  My CC is nice, but the rules and attendance taking are growing a bit old.  I feel like a second grade teacher having to take roll every day.  If they don&apos;t want to come to class, it&apos;s bloody fine with me.  They are all over 18 and are paying their money.  If they think playing xbox is more important that school so be it.  I&apos;m not a bloody kindergarden teacher.  Maybe I&apos;ll quit taking roll and just start making that shit up!  Might make my gradesheets look more normal.  Hmmmmmm . . . .&lt;br /&gt;&lt;br /&gt;Ok, well enough rant for now.  Maybe my next post will be more of a rave.  I would hate for people to think that I&apos;m that negative.  I really am just a positive person having a bad day!!!!&lt;div class=&quot;blogger-post-footer&quot;&gt;&lt;img width=&apos;1&apos; height=&apos;1&apos; src=&apos;https://blogger.googleusercontent.com/tracker/8677504-112778942212704032?l=bearbrarian.blogspot.com&apos; alt=&apos;&apos; /&gt;&lt;/div&gt;";
            //"Ok, so here we go again.  I really like to write, but I just never seem to find the time to stay focused.  Right now I'm typing this entry as one of my classes is taking a test.  I'm working full-time as a librarian and teaching three classes in the evening.  It's still fun to teach, but it's also quite tiring.  I never knew how draining it was to perform like that three nights a week.  I can only imagine how it is for comedians and other performers.  I really do feel like what I do is part education and part entertainment.  I know how bored I was in school, so I like to keep my classes a bit more comical and on the entertaining side.  This is helpful, especially when teaching night classes!!<br /><br />Work still basically is a give and take proposition.  As I have stated before, I really like being a librarian, I just don't necessarily like where I am librarianing at! (Please pardon the grammatical diversion)  I have been interviewing for multiple jobs mainly to keep my feet wet and my intellect stimulated, but there are times I really want to stay where I am and finish the projects I've started.  However, if I keep getting crappy emails from people at work, I'm going to have to get going!  It rarely happens, and honestly this is really the first time I've ever heard anyone complain.  However, they intimated that others were dissatisfied with the time it sometimes takes to get materials to them.  I am trying to rework the tracking of purchases at work to stay on top of things, but sometimes I have no control over timeframes.  One of the shipping departments at a specific location lost a box of five rather large books, but I didn't hear about it for 6 months.  Now I've got someone complaining that I didn't get them some books, which I am sure that I ordered, in three weeks.  I am glad that they notified me, but they were pretty harsh with the wording.  I am doing my best with minimal support and understanding.  I mean, it's not like I'm one person with two assistants trying to support the information needs of a 4000 employee company or anything.  Not to mention that I'm trying to drag my portion of the information network into the 21st century all while the bloody IT department is trying to take over the company and decided who gets to do what.  For goddesses sake, they don't even have an OPAC!  I'm just trying to make things better and all I get is, sorry, but your little library project isn't worthwhile, now go back and shelve some books.  I think that's the biggest problem I have.  Only a very few people respect what I do, and even fewer understand it.  The fact that I report to a pharmacist who only works part-time speaks volumes about how they value my position.  Don't get me wrong, I am happy with my salary, and I love my co-workers, but there is little or no validation of me as an employee.  No one truly seems to understand what I can do for the company, or what the library can do for that matter.<br /><br />That kind of shit really pisses me off, and no matter how many people I talk to, no matte how may emails I send out, and no matter how much I plan improvements, nothing seems to really matter.  Perhaps that is just the way it is at most jobs, but I can't help but feel that there is a job out there where I can feel at least moderately validated as a thinking, breathing human being.  I know that my job at the hospital back in Kansas City gave me a great sense of validation.  I was frequently asked to work on committees, and to share my knowledge and experience in new and interesting ways.  People frequently sought my insight on various problems, library-related or otherwise.  I truly felt like a valuable member of the team.  Now I just feel like a lump of flesh doing just enough to get by and get a raise every year.<br /><br />Thank goodness I do at least have the diversion of teaching, or I will as long as I go and get a bloody TB test this week.  My CC is nice, but the rules and attendance taking are growing a bit old.  I feel like a second grade teacher having to take roll every day.  If they don't want to come to class, it's bloody fine with me.  They are all over 18 and are paying their money.  If they think playing xbox is more important that school so be it.  I'm not a bloody kindergarden teacher.  Maybe I'll quit taking roll and just start making that shit up!  Might make my gradesheets look more normal.  Hmmmmmm . . . .<br /><br />Ok, well enough rant for now.  Maybe my next post will be more of a rave.  I would hate for people to think that I'm that negative.  I really am just a positive person having a bad day!!!!<div class=\"blogger-post-footer\"><img width='1' height='1' src='https://blogger.googleusercontent.com/tracker/8677504-112778942212704032?l=bearbrarian.blogspot.com' alt='' /></div>";
            var entryContentType = AtomTextType.html;
            const string entryContrib3Name = "Curly";
            const string entryId = "tag:blogger.com,1999:blog-8677504.post-112778942212704032";
            const string entryLink5Rel = "alternate";
            const string entryLink5Href = "http://bearbrarian.blogspot.com/2005/09/ways-down-road.html";
            var entryLink5MediaType = MediaType.TextHtml;
            const string entryLink5Title = "A ways down the road...";
            var entryLink5HrefLang = LanguageTag.Parse("en-US");
            var entryPublished = new DateTime(2005, 9, 27, 2, 31, 0); //2005-09-26T19:31:00.000-07:00
            const string entryRights = "You have the right to remain silent.";

            const string entrySourceTitleText = "\r\n        <b xmlns=\"http://www.w3.org/2005/Atom\">The original article</b>";
            var entrySourceTitleType = AtomTextType.html;
            const string entrySummary = "road, what road?";
            const string entryTitle = "A ways down the road";
            var entryUpdated = new DateTime(2005, 9, 27, 2, 50, 22, 140); //2005-09-26T19:50:22.140-07:00
            //var entryExtension1Prefix = "thr";
            //var entryExtension1Name = "total";

            #endregion

            var feed = document.Root as IAtomFeed;

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Lang);

            Assert.AreEqual(feedLang.PrimaryLanguage, feed.Lang.PrimaryLanguage);
            Assert.AreEqual(feedLang.Country, feed.Lang.Country);
            Assert.AreEqual(4, feed.Namespaces.Count());
            Assert.IsNotNull(feed.Namespaces.Last());
            Assert.AreEqual(1, document.Where<IStyleSheet>(x => x != null).Count());
            Assert.IsNotNull(document.Where<IStyleSheet>(x => x != null).FirstOrDefault());

            Assert.AreEqual(1, feed.Authors.Count());
            Assert.AreEqual(authorName, feed.Authors.First().PersonName);
            Assert.AreEqual(authorUri, feed.Authors.First().Uri.ToString());
            Assert.AreEqual(authorEmail, feed.Authors.First().Email);

            Assert.AreEqual(2, feed.Categories.Count());
            Assert.IsNotNull(feed.Categories.First());
            Assert.AreEqual(cat1Term, feed.Categories.First().Term);
            Assert.AreEqual(cat1Scheme, feed.Categories.First().Scheme.ToString());
            Assert.AreEqual(cat1Label, feed.Categories.First().Label);

            Assert.AreEqual(2, feed.Contributors.Count());
            Assert.AreEqual(contribName, feed.Contributors.First().PersonName);
            Assert.AreEqual(contribUri, feed.Contributors.First().Uri.ToString());
            Assert.AreEqual(contribEmail, feed.Contributors.First().Email);

            Assert.IsNotNull(feed.Generator);
            Assert.AreEqual(genUri, feed.Generator.Uri.ToString());
            Assert.AreEqual(genName, feed.Generator.GeneratorName);

            Assert.IsNotNull(feed.Icon);
            Assert.AreEqual(iconUri, feed.Icon.Uri.ToString());

            Assert.IsNotNull(feed.Id);
            Assert.AreEqual(id, feed.Id.Content.ToString());

            Assert.AreEqual(4, feed.Links.Count());
            Assert.AreEqual(link1Rel, feed.Links.First().Rel);
            Assert.AreEqual(link1Type, feed.Links.First().Type);

            Assert.IsNotNull(feed.Logo);
            Assert.AreEqual(logoUri, feed.Logo.Uri.ToString());

            Assert.IsNotNull(feed.Rights);
            Assert.AreEqual(rightsBaseId, feed.Rights.BaseId.ToString());
            Assert.AreEqual(rightsLang, feed.Rights.Lang);
            Assert.AreEqual(rightsType, feed.Rights.Type);
            //Assert.AreEqual(rightsText, feed.Rights.Text);            

            Assert.IsNotNull(feed.Subtitle);
            Assert.AreEqual(subtitle, feed.Subtitle.Text);
            Assert.AreEqual(subtitleLang, feed.Subtitle.Lang);

            Assert.IsNotNull(feed.Title);
            Assert.AreEqual(title, feed.Title.Text);
            Assert.AreEqual(titleType, feed.Title.Type);

            Assert.IsNotNull(feed.Updated);
            Assert.AreEqual(updated, feed.Updated.Date);

            //Assert.AreEqual(3, feed.Extensions.Count());
            //Assert.AreEqual(lastExtPrefix, feed.Extensions.Last().Prefix);
            //Assert.AreEqual(lastExtName, feed.Extensions.Last().Name);
            //Assert.IsTrue(feed.Extensions.Last().ToString().Contains(lastExtContent));

            Assert.AreEqual(6, feed.Entries.Count());
            Assert.IsNotNull(feed.Entries.First());

            Assert.AreEqual(1, feed.Entries.First().Authors.Count());
            Assert.AreEqual(entryAuthorEmail, feed.Entries.First().Authors.First().Email);
            //Assert.AreEqual(1, feed.Entries.First().Authors.First().Extensions.Count());
            //Assert.AreEqual(entryAuthorExtPrefix, feed.Entries.First().Authors.First().Extensions.First().Prefix);
            //Assert.AreEqual(entryAuthorExtName, feed.Entries.First().Authors.First().Extensions.First().Name);
            //Assert.AreEqual(entryAuthorExtContent, feed.Entries.First().Authors.First().Extensions.First().ToString());

            Assert.AreEqual(1, feed.Entries.First().Categories.Count());
            Assert.AreEqual(entryCategoryTerm, feed.Entries.First().Categories.First().Term);

            Assert.IsNotNull(feed.Entries.First().Content);
            Assert.AreEqual(entryContentText, feed.Entries.First().Content.Text);
            Assert.AreEqual(entryContentType, feed.Entries.First().Content.Type);

            Assert.AreEqual(3, feed.Entries.First().Contributors.Count());
            Assert.AreEqual(entryContrib3Name, feed.Entries.First().Contributors.Last().PersonName);

            Assert.IsNotNull(feed.Entries.First().Id);
            Assert.AreEqual(entryId, feed.Entries.First().Id.Content.ToString());

            Assert.AreEqual(5, feed.Entries.First().Links.Count());
            Assert.AreEqual(entryLink5Href, feed.Entries.First().Links.Last().Href.ToString());
            Assert.AreEqual(entryLink5HrefLang, feed.Entries.First().Links.Last().HrefLang);
            Assert.AreEqual(entryLink5MediaType, feed.Entries.First().Links.Last().Type);
            Assert.AreEqual(entryLink5Title, feed.Entries.First().Links.Last().Title);
            Assert.AreEqual(entryLink5Rel, feed.Entries.First().Links.Last().Rel);

            Assert.IsNotNull(feed.Entries.First().Published);
            Assert.AreEqual(entryPublished, feed.Entries.First().Published.Date);

            Assert.IsNotNull(feed.Entries.First().Rights);
            Assert.AreEqual(entryRights, feed.Entries.First().Rights.Text);

            Assert.IsNotNull(feed.Entries.First().Source);
            Assert.IsNotNull(feed.Entries.First().Source.Title);
            Assert.AreEqual(entrySourceTitleText, feed.Entries.First().Source.Title.Text);
            Assert.AreEqual(entrySourceTitleType, feed.Entries.First().Source.Title.Type);

            Assert.IsNotNull(feed.Entries.First().Summary);
            Assert.AreEqual(entrySummary, feed.Entries.First().Summary.Text);

            Assert.IsNotNull(feed.Entries.First().Title);
            Assert.AreEqual(entryTitle, feed.Entries.First().Title.Text);

            Assert.IsNotNull(feed.Entries.First().Updated);
            Assert.AreEqual(entryUpdated, feed.Entries.First().Updated.Date);

            //Assert.AreEqual(1, feed.Entries.First().Extensions.Count());
            //Assert.AreEqual(entryExtension1Prefix, feed.Entries.First().Extensions.First().Prefix);
            //Assert.AreEqual(entryExtension1Name, feed.Entries.First().Extensions.First().Name);
        }

        [Test]
        public void CanBeCreatedFromXmlOutput()
        {
            const string path = @".\Files\bearbrarian.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = ContentType.GetContentType(location);
            Assert.AreEqual(MediaType.ApplicationAtomXml, contentType.Type);

            var original = location.ToXmlDocument();
            var xmlString = original.ToString();
            Assert.IsNotNull(xmlString);

            var document = XmlElement.Parse(xmlString);
            //var xml = new XmlDocument();
            //xml.LoadXml(xmlString);
            //IAtomFeed feed = null;
            //var encoding = CharacterSet.Utf8;
            //var styleSheets = new List<IXmlStyleSheet>();

            //foreach (var child in xml.ChildNodes.Cast<XmlNode>().Where(node => node != null))
            //{
            //    switch (child.NodeType)
            //    {
            //        case XmlNodeType.XmlDeclaration:
            //            encoding = child.ToEncoding();
            //            break;
            //        case XmlNodeType.ProcessingInstruction:
            //            styleSheets.AddIfNotNull(child.ToXmlStyleSheet());
            //            break;
            //        case XmlNodeType.Element:
            //            if (child.Name != "feed")
            //                break;

            //            feed = child.ToAtomFeed(encoding, styleSheets);
            //            break;
            //        default:
            //            break;
            //    }
            //}

            MakeAtomFeedAssertions(document);
        }

        [Test]
        public void CanBeCreatedFromLocalXml()
        {
            const string path = @".\Files\bearbrarian.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = ContentType.GetContentType(location);
            Assert.AreEqual(MediaType.ApplicationAtomXml, contentType.Type);

            var document = location.ToXmlDocument();

            MakeAtomFeedAssertions(document);
        }

        [Test]
        public void CanBeCreatedFromRemoteFeedBurnerSource()
        {
            #region Constants

            var location = new Uri("http://feeds2.feedburner.com/oreilly/radar/atom");
            var document = location.ToXmlDocument();
            Assert.IsNotNull(document);

            var feed = document.Root as IAtomFeed;
            const string generatorName = "Movable Type Pro 4.21-en";
            const string generatorUri = "http://www.sixapart.com/movabletype/";
            
            var entry1ContentLang = LanguageTag.Parse("en");
            var entry1ContentBase = "http://radar.oreilly.com/";
            var entry1ContentType = AtomTextType.html;

            #endregion

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Id);
            Assert.IsNotNull(feed.Title);

            Assert.IsNotNull(feed.Generator);
            Assert.AreEqual(generatorName, feed.Generator.GeneratorName);
            Assert.AreEqual(generatorUri, feed.Generator.Uri.ToString());

            Assert.IsTrue(feed.Entries.Count() > 0);
            Assert.IsTrue(feed.Entries.First().Categories.Count() > 0);
            Assert.IsNotNull(feed.Entries.First().Content);
            Assert.AreEqual(entry1ContentLang, feed.Entries.First().Content.Lang);
            Assert.AreEqual(entry1ContentBase, feed.Entries.First().Content.BaseId.ToString());
            Assert.AreEqual(entry1ContentType, feed.Entries.First().Content.Type);
            Assert.IsTrue(feed.Links.Count() > 0);
            //Assert.IsTrue(feed.Extensions.Count() > 0);
        }

        [Test]
        public void CanBeCreatedFromRemoteBlogEdSource()
        {
            const string generatorName = "BlogEd 008";
            const string generatorUri = "https://bloged.dev.java.net/";

            var location = new Uri("http://bblfish.net/blog/blog.atom");
            //System.Diagnostics.Debug.WriteLine("before ToAtomFeed");
            var document = location.ToXmlDocument();
            var feed = document.Root as IAtomFeed;
            //System.Diagnostics.Debug.WriteLine("after ToAtomFeed");

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Generator);
            Assert.AreEqual(generatorName, feed.Generator.GeneratorName);
            Assert.AreEqual(generatorUri, feed.Generator.Uri.ToString());
        }
    }
}
