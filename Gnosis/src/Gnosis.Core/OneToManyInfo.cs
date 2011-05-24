using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Core
{
    public class OneToManyInfo
    {
        public OneToManyInfo(OneToManyAttribute oneToManyAttribute, PropertyInfo property)
        {
            this.tableName = oneToManyAttribute.TableName;
            this.property = property;
            this.primaryKey = oneToManyAttribute.HasPrimaryKey ? new PrimaryKeyInfo(oneToManyAttribute.PrimaryKeyName, oneToManyAttribute.PrimaryKeyType, oneToManyAttribute.PrimaryKeyIsAutoIncrement) : null;
            this.foreignKey = oneToManyAttribute.HasForeignKey ? new ForeignKeyInfo(oneToManyAttribute.ForeignKeyName, oneToManyAttribute.ForeignKeyType) : null;
            this.sequence = oneToManyAttribute.HasSequence ? new SequenceInfo(oneToManyAttribute.SequenceName, oneToManyAttribute.SequenceType) : null;
            this.table = ChildType.GetTableInfo();
            GetIndices();
        }

        public OneToManyInfo(string tableName, PropertyInfo property, PrimaryKeyInfo primaryKey, ForeignKeyInfo foreignKey, SequenceInfo sequence)
        {
            this.tableName = tableName;
            this.property = property;
            this.primaryKey = primaryKey;
            this.foreignKey = foreignKey;
            this.sequence = sequence;
            this.table = ChildType.GetTableInfo();
            GetIndices();
        }

        private readonly string tableName;
        private readonly PropertyInfo property;
        private readonly PrimaryKeyInfo primaryKey;
        private readonly ForeignKeyInfo foreignKey;
        private readonly SequenceInfo sequence;
        private readonly TableInfo table;
        private readonly IList<IndexInfo> foreignIndices = new List<IndexInfo>();

        private void GetIndices()
        {
            foreach (var attribute in property.GetCustomAttributes(true))
            {
                var indexAttribute = attribute as ForeignIndexAttribute;
                if (indexAttribute != null)
                {
                    foreignIndices.Add(new IndexInfo(indexAttribute));
                }
            }
        }

        public string TableName
        {
            get { return tableName; }
        }

        public PrimaryKeyInfo PrimaryKey
        {
            get { return primaryKey; }
        }

        public ForeignKeyInfo ForeignKey
        {
            get { return foreignKey; }
        }

        public SequenceInfo Sequence
        {
            get { return sequence; }
        }

        public Type ChildType
        {
            get { return property.PropertyType.GetGenericArguments()[0]; }
        }

        public IEnumerable<IndexInfo> ForeignIndices
        {
            get { return foreignIndices; }
        }

        public IEnumerable<ColumnInfo> OriginalColumns
        {
            get { return table.Columns; }
        }

        public IEnumerable<IndexInfo> OriginalIndices
        {
            get { return table.Indices; }
        }

        public IEnumerable<CollectionItemInfo> GetItemInfo(object instance)
        {
            var value = property.GetValue(instance, null) as ISet;
            if (value != null)
            {
                return value.GetItemInfo();
            }
            else
            {
                return new List<CollectionItemInfo>();
            }
        }
    }
}
