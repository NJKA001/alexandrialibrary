using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class MediaFactory
        : IMediaFactory
    {
        public MediaFactory()
        {
        }

        private readonly IDictionary<string, Func<Uri, IContentType, IMedia>> createFunctions = new Dictionary<string, Func<Uri, IContentType, IMedia>>();

        public IMedia Create(Uri location, IContentType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            var key = type.Name.ToLower();
            return createFunctions.ContainsKey(key) ?
                createFunctions[key](location, type)
                : null;
        }

        public IEnumerable<string> GetMediaTypes()
        {
            return createFunctions.Keys.ToList();
        }

        public void MapMediaType(string mediaType, Func<Uri, IContentType, IMedia> createFunction)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");
            if (createFunction == null)
                throw new ArgumentNullException("createFunction");

            var key = mediaType.ToLower();
            createFunctions[key] = createFunction;
        }
    }
}
