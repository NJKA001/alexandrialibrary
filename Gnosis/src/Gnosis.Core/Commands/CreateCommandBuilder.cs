using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Core.Commands
{
    public class CreateCommandBuilder : CommandBuilder
    {
        public CreateCommandBuilder()
        {
        }

        /*
        public CreateCommandBuilder(Type type, object instance)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (instance == null)
                throw new ArgumentNullException("instance");

            var table = type.GetTableInfo();
            if (table != null)
            {
                AddStatement(new CreateTableStatement(this, table.Name, type, instance));
                
                foreach (var index in table.Indices) //type.GetIndexAttributes())
                {
                    AddStatement(new CreateIndexStatement(table.Name, index.Name, index.IsUnique, index.Columns));
                }

                foreach (var child in table.Children)
                {
                    //AddStatement(new CreateTableStatement(this, child.TableName, child.ChildType, null));


                }
            }
        }*/
    }
}
