using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TimeStamp : ITimeStamp
    {
        public TimeStamp(Uri createdBy)
        {
            this.createdBy = createdBy;
            this.createdDate = DateTime.Now.ToUniversalTime();
        }

        public TimeStamp(Uri createdBy, DateTime createdDate, Uri lastAccessedBy, DateTime lastAccessedDate, Uri lastModifiedBy, DateTime lastModifiedDate)
        {
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.lastAccessedBy = lastAccessedBy;
            this.lastAccessedDate = lastAccessedDate;
            this.lastModifiedBy = lastModifiedBy;
            this.lastModifiedDate = lastModifiedDate;
        }

        private readonly Guid id = Guid.NewGuid();
        private readonly Uri createdBy;
        private readonly DateTime createdDate;
        private readonly Uri lastAccessedBy = UriExtensions.EmptyUri;
        private readonly DateTime lastAccessedDate = DateTime.MinValue;
        private readonly Uri lastModifiedBy = UriExtensions.EmptyUri;
        private readonly DateTime lastModifiedDate = DateTime.MinValue;

        public Guid Id
        {
            get { return id; }
        }

        public Uri CreatedBy
        {
            get { return createdBy; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
        }

        public Uri LastAccessedBy
        {
            get { return lastAccessedBy; }
        }

        public DateTime LastAccessedDate
        {
            get { return lastAccessedDate; }
        }

        public Uri LastModifiedBy
        {
            get { return lastModifiedBy; }
        }

        public DateTime LastModifiedDate
        {
            get { return lastModifiedDate; }
        }
    }
}
