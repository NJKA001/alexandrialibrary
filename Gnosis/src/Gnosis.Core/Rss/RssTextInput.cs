using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssTextInput
        : IRssTextInput
    {
        public RssTextInput(string title, string description, string name, Uri link)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (description == null)
                throw new ArgumentNullException("description");
            if (name == null)
                throw new ArgumentNullException("name");
            if (link == null)
                throw new ArgumentNullException("link");

            this.title = title;
            this.description = description;
            this.name = name;
            this.link = link;
        }

        private readonly string title;
        private readonly string description;
        private readonly string name;
        private readonly Uri link;

        #region IRssTextInput Members

        public string Title
        {
            get { return title; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Name
        {
            get { return name; }
        }

        public Uri Link
        {
            get { return link; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendLine("<textInput>");
            xml.AppendLine("  <title>" + title.ToXmlEscapedString() + "</title>");
            xml.AppendLine("  <description>" + description.ToXmlEscapedString() + "</description>");
            xml.AppendLine("  <name>" + name.ToXmlEscapedString() + "</name>");
            xml.AppendLine("  <link>" + link.ToString().ToXmlEscapedString() + "</link>");
            xml.AppendLine("</textInput>");

            return xml.ToString();
        }
    }
}
