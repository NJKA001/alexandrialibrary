using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateCommandBuilder : CommandBuilder
    {
        public CreateCommandBuilder(Type type, object instance)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (instance == null)
                throw new ArgumentNullException("instance");

            var table = type.GetTableAttribute();
            var indices = type.GetIndexAttributes();

            if (table != null)
            {
                AddStatement(new CreateTableBuilder(this, table.Name, type, instance));
                
                foreach (var index in indices)
                {
                    AddStatement(new CreateIndexBuilder(table.Name, index.Name, index.IsUnique, index.Columns));
                }
            }
        }
    }
}
