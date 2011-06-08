using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackMetadata
        : ValueBase, ITrackMetadata
    {
        public TrackMetadata()
        {
            AddInitializer("TextEncoding", x => this.textEncoding = x.ToString());
            AddInitializer("Description", x => this.description = x.ToString());
            AddInitializer("Content", x => this.content = x.ToString());
        }

        public TrackMetadata(Guid parent, string textEncoding, string description, string content)
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
