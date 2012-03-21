using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;

namespace Gnosis.Importing
{
    public class ImportInfo
        : IImportInfo
    {
        public ImportInfo(IMedia media, IEntity entity)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            this.media = media;
            this.entity = entity;
        }

        private readonly IMedia media;
        private readonly IEntity entity;

        public string Path
        {
            get { return media.Path; }
        }

        public IMediaType MediaType
        {
            get { return media.Type; }
        }

        public IEntity Entity
        {
            get { return entity; }
        }
    }
}
