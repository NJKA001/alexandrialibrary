using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Image
{
    public abstract class ImageBase
        : IImage
    {
        protected ImageBase(Uri location, IContentType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            this.location = location;
            this.type = type;
        }

        private Uri location;
        private IContentType type;
        
        protected bool isLoaded;
        protected byte[] data;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
        }

        public bool IsLoaded
        {
            get { return isLoaded; }
        }

        public virtual void Load()
        {
            if (isLoaded)
                return;
            
            isLoaded = true;
            data = location.ToContentData();
        }

        public virtual byte[] GetData()
        {
            return data;
        }

        public virtual object GetImageSource()
        {
            return isLoaded ? data : (object)location;
        }

        public virtual IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public virtual IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
