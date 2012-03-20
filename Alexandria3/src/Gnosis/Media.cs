using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class Media
        : IMedia
    {
        public Media(string path, IMediaType type)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if (type == null)
                throw new ArgumentNullException("type");

            this.path = path;
            this.type = type;
        }

        private readonly string path;
        private readonly IMediaType type;

        public string Path
        {
            get { return path; }
        }

        public IMediaType Type
        {
            get { return type; }
        }
    }
}
