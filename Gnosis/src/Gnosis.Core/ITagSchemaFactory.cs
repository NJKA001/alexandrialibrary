using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagSchemaFactory
    {
        ITagSchema Create(long id);

        void Add(ITagSchema schema);
        void Remove(ITagSchema schema);
    }
}
