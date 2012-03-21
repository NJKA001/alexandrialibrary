using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;

namespace Gnosis.Tagging
{
    public interface ITagger
    {
        ITag GetTag(string path, Category category);
        IEnumerable<ITag> GetTags(string path);

        void SaveTag(string path, ITag tag);
    }
}
