using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Audio
{
    public abstract class AudioBase
        : IMedia
    {
        protected AudioBase(Uri location, IMediaType type)
        {
            this.location = location;
            this.type = type;
        }

        private readonly Uri location;
        private readonly IMediaType type;

        #region IMedia Members

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        #endregion
    }
}
