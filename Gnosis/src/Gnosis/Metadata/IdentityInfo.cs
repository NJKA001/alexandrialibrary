using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public struct IdentityInfo
    {
        public IdentityInfo(Uri location, IContentType type, string name, string summary, DateTime fromDate, DateTime toDate, uint number)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");
            if (summary == null)
                throw new ArgumentNullException("summary");

            this.location = location;
            this.type = type;
            this.name = name;
            this.summary = summary;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.number = number;
        }

        private Uri location;
        private IContentType type;
        private string name;
        private string summary;
        private DateTime fromDate;
        private DateTime toDate;
        private uint number;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Summary
        {
            get { return summary; }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
        }

        public DateTime ToDate
        {
            get { return toDate; }
        }

        public uint Number
        {
            get { return number; }
        }

        public static IdentityInfo GetDefault(IContentType type)
        {    
            return new IdentityInfo(Guid.Empty.ToUrn(), type, "Unknown", string.Empty, DateTime.MinValue, DateTime.MaxValue, 0);
        }

        public static IdentityInfo GetNew(IContentType type)
        {
            return new IdentityInfo(Guid.NewGuid().ToUrn(), type, "Unknown", string.Empty, DateTime.MinValue, DateTime.MaxValue, 0);
        }
    }
}
