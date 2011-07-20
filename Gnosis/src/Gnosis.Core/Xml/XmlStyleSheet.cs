using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public class XmlStyleSheet
        : XmlProcessingInstruction, IXmlStyleSheet
    {
        private XmlStyleSheet(IMediaType type, IMedia media, Uri href)
            : base(XmlStyleSheetTarget, GetContent(type, media, href))
        {
            this.type = type;
            this.media = media;
            this.href = href;
        }

        private readonly IMediaType type;
        private readonly IMedia media;
        private readonly Uri href;

        public const string XmlStyleSheetTarget = "xml-stylesheet";

        private static string GetContent(IMediaType type, IMedia media, Uri href)
        {
            var content = new StringBuilder();
            return content.ToString();
        }

        #region IXmlStyleSheet Members

        public IMediaType Type
        {
            get { return type; }
        }

        public IMedia Media
        {
            get { return media; }
        }

        public Uri Href
        {
            get { return href; }
        }

        #endregion

        new public static IXmlStyleSheet Parse(string target, string content)
        {
            if (target != XmlStyleSheetTarget)
                throw new ArgumentException("target must be " + XmlStyleSheetTarget);
            if (content == null)
                throw new ArgumentNullException("content");

            IMediaType type = null;
            IMedia media = null;
            Uri href = null;
            var fields = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields)
            {
                if (field.Contains('='))
                {
                    var tokens = field.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens != null && tokens.Length == 2 && tokens[0] != null && tokens[1] != null)
                    {
                        var value = tokens[1].Trim().RemoveQuotes();

                        switch (tokens[0].Trim())
                        {
                            case "type":
                                type = MediaType.Parse(value);
                                break;
                            case "media":
                                media = W3c.Media.Parse(value);
                                break;
                            case "href":
                                href = new Uri(value);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return new XmlStyleSheet(type, media, href);
        }
    }
}
