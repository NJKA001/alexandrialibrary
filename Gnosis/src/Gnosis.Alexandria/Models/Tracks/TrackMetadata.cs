using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackMetadata
        : ValueBase, ITrackMetadata
    {
        public TrackMetadata(string textEncoding, string description, string content)
        {
            this.textEncoding = textEncoding;
            this.description = description;
            this.content = content;
        }

        private readonly string textEncoding;
        private readonly string description;
        private readonly string content;

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
    }
}
