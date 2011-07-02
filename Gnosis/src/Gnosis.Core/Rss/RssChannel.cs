using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssChannel
        : IRssChannel
    {
        #region IRssChannel Members

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public Uri Link
        {
            get { throw new NotImplementedException(); }
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public Ietf.ILanguageTag Language
        {
            get { throw new NotImplementedException(); }
        }

        public string Copyright
        {
            get { throw new NotImplementedException(); }
        }

        public string ManagingEditor
        {
            get { throw new NotImplementedException(); }
        }

        public string WebMaster
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime PubDate
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime LastBuildDate
        {
            get { throw new NotImplementedException(); }
        }

        public string Generator
        {
            get { throw new NotImplementedException(); }
        }

        public Uri Docs
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRssCloud Cloud
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TimeSpan Ttl
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRssImage Image
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public W3c.IPicsRating Rating
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRssTextInput TextInput
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<RssHour> SkipHours
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<RssDay> SkipDays
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IRssCategory> Categories
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IRssItem> Items
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
