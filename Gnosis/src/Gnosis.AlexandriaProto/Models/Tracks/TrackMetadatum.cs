using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackMetadatum
        : ValueBase<ITrackMetadatum>, ITrackMetadatum
    {
        public TrackMetadatum()
        {
            AddInitializer(value => this.textEncoding = value.ToEnum<TextEncoding>(), meta => meta.TextEncoding);
            AddInitializer(value => this.description = value.ToString(), meta => meta.Description);
            AddInitializer(value => this.content = value.ToString(), meta => meta.Content);
        }

        public TrackMetadatum(Guid parent, TextEncoding textEncoding, string description, string content)
        {
            AddInitializer(value => this.textEncoding = textEncoding, meta => meta.TextEncoding);
            AddInitializer(value => this.description = description, meta => meta.Description);
            AddInitializer(value => this.content = content, meta => meta.Content);

            Initialize(parent);
        }

        private TextEncoding textEncoding;
        private string description;
        private string content;

        #region ITrackMetadata Members

        public TextEncoding TextEncoding
        {
            get { return textEncoding; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Content
        {
            get { return content; }
        }

        #endregion
    }
}
