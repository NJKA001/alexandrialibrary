using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackMetadatum
        : ValueBase, ITrackMetadatum
    {
        public TrackMetadatum()
        {
            AddInitializer("TextEncoding", value => this.textEncoding = value.ToString());
            AddInitializer("Description", value => this.description = value.ToString());
            AddInitializer("Content", value => this.content = value.ToString());
        }

        public TrackMetadatum(Guid parent, string textEncoding, string description, string content)
        {
            AddInitializer("TextEncoding", x => this.textEncoding = textEncoding);
            AddInitializer("Description", x => this.description = description);
            AddInitializer("Content", x => this.content = content);

            Initialize(parent);
        }

        private string textEncoding;
        private string description;
        private string content;

        #region ITrackMetadata Members

        public string TextEncoding
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
