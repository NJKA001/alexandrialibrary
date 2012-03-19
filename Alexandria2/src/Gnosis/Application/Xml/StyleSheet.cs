using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public class StyleSheet
        : ProcessingInstruction, IStyleSheet
    {
        private StyleSheet(INode parent, string type, IStyleMedia media, Uri href)
            : base(parent, XmlStyleSheetTarget, GetContent(type, media, href))
        {
            this.type = type;
            this.media = media;
            this.href = href;
        }

        private readonly string type;
        private readonly IStyleMedia media;
        private readonly Uri href;

        public const string XmlStyleSheetTarget = "xml-stylesheet";

        private static string GetContent(string type, IStyleMedia media, Uri href)
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

        public string Type
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

        public static IStyleSheet ParseStyleSheet(INode parent, string target, string content)
        {
            if (target != XmlStyleSheetTarget)
                throw new ArgumentException("target must be " + XmlStyleSheetTarget);

            string type = null;
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
                                type = value;
                                break;
                            case "media":
                                media = StyleMedia.Parse(value);
                                break;
                            case "href":
                                value.TryParseUri(out href);
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
