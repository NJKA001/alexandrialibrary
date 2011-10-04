using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags
{
    public class TagSchemaRoot
        : TagSchema
    {
        protected TagSchemaRoot(string name)
            : base(new Uri("http://gn0s1s.com/ns/1/tag-schema/"), "")
        {
        }
    }
}
