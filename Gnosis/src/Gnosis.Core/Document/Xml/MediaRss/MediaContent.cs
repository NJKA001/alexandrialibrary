using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.MediaRss
{
    public class MediaContent
        : Element, IMediaContent
    {
        public MediaContent(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Url
        {
            get { return GetAttributeUri("url"); }
        }

        public long FileSize
        {
            get { return GetAttributeInt64("fileSize"); }
        }

        public IMediaType Type
        {
            get { return MediaType.Parse(GetAttributeString("type")); }
        }

        public MediaRssMedium Medium
        {
            get { return GetAttributeEnum<MediaRssMedium>("medium", MediaRssMedium.Unspecified); }
        }

        public bool IsDefault
        {
            get { return GetAttributeBoolean("isDefault", false); }
        }

        public MediaRssExpression Expression
        {
            get { return GetAttributeEnum<MediaRssExpression>("expression", MediaRssExpression.Full); }
        }

        public int BitRate
        {
            get { return GetAttributeInt32("bitrate"); }
        }

        public int SamplingRate
        {
            get { return GetAttributeInt32("samplingrate"); }
        }

        public int Channels
        {
            get { return GetAttributeInt32("channels"); }
        }

        public int Duration
        {
            get { return GetAttributeInt32("duration"); }
        }

        public int Height
        {
            get { return GetAttributeInt32("height"); }
        }

        public int Width
        {
            get { return GetAttributeInt32("width"); }
        }

        public ILanguageTag Lang
        {
            get { return LanguageTag.Parse(GetAttributeString("lang")); }
        }
    }
}
