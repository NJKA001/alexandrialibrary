using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class MediaType
        : IMediaType
    {
        protected internal MediaType(string name)
            : this(name, null, null)
        {
        }

        protected internal MediaType(string name, string charSet)
            : this(name, charSet, null)
        {
        }

        protected internal MediaType(string name, string charSet, string boundary)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.charSet = charSet;
            this.boundary = boundary;
            this.supertype = GetMediaSuperType(name);
        }

        private readonly string name;
        private readonly string charSet;
        private readonly string boundary;
        private readonly MediaSupertype supertype;

        private static MediaSupertype GetMediaSuperType(string name)
        {
            if (name.ToLower().StartsWith("audio/"))
                return MediaSupertype.Audio;
            else if (name.ToLower().StartsWith("image/"))
                return MediaSupertype.Image;
            else if (name.ToLower().StartsWith("message/"))
                return MediaSupertype.Message;
            else if (name.ToLower().StartsWith("model/"))
                return MediaSupertype.Model;
            else if (name.ToLower().StartsWith("multipart/"))
                return MediaSupertype.Multipart;
            else if (name.ToLower().StartsWith("text/"))
                return MediaSupertype.Text;
            else if (name.ToLower().StartsWith("video/"))
                return MediaSupertype.Video;
            else
                return MediaSupertype.Application;
        }

        public string Name
        {
            get { return name; }
        }

        public string CharSet
        {
            get { return charSet; }
        }

        public string Boundary
        {
            get { return boundary; }
        }

        public MediaSupertype Supertype
        {
            get { return supertype; }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(name);

            if (!string.IsNullOrEmpty(charSet))
                builder.AppendFormat("; charset={0}", charSet.ToString());

            if (!string.IsNullOrEmpty(boundary))
                builder.AppendFormat("; boundary={0}", boundary);

            return builder.ToString();
        }
    }
}
