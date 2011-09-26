using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public class StyleSheet
        : ProcessingInstruction, IStyleSheet
    {
        private StyleSheet(INode parent, IMediaType type, IStyleMedia media, Uri href)
            : base(parent, XmlStyleSheetTarget, GetContent(type, media, href))
        {
            this.type = type;
            this.media = media;
            this.href = href;
        }

        private readonly IMediaType type;
        private readonly IStyleMedia media;
        private readonly Uri href;

        public const string XmlStyleSheetTarget = "xml-stylesheet";

        private static string GetContent(IMediaType type, IStyleMedia media, Uri href)
        {
            var content = new StringBuilder();

            if (type != null)
                content.AppendFormat(" type=\"{0}\"", type);
            if (media != null)
                content.AppendFormat(" media=\"{0}\"", media);
            if (href != null)
                content.AppendFormat(" href=\"{0}\"", href);

            return content.ToString();
        }

        #region IXmlStyleSheet Members

        public IMediaType Type
        {
            get { return type; }
        }

        public IStyleMedia Media
        {
            get { return media; }
        }

        public Uri Href
        {
            get { return href; }
        }

        #endregion

        new public static IStyleSheet Parse(INode parent, string target, string content)
        {
            if (target != XmlStyleSheetTarget)
                throw new ArgumentException("target must be " + XmlStyleSheetTarget);

            IMediaType type = null;
            IStyleMedia media = null;
            Uri href = null;
            var fields = content.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields)
            {
                if (field.Contains('='))
                {
                    var tokens = field.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens != null && tokens.Length == 2 && tokens[0] != null && tokens[1] != null)
                    {
                        var name = tokens[0].Trim();
                        var value = tokens[1].Trim().RemoveQuotes();
                        
                        switch (name)
                        {
                            case "type":
                                type = MediaType.Parse(value);
                                break;
                            case "media":
                                media = StyleMedia.Parse(value);
                                break;
                            case "href":
                                UriExtensions.TryParse(value, out href);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return new StyleSheet(parent, type, media, href);
        }
    }
}
