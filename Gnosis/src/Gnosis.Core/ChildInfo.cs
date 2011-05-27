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
    public class ChildInfo
    {
        public ChildInfo(OneToManyAttribute oneToManyAttribute, PropertyInfo property)
        {
            this.tableName = oneToManyAttribute.TableName;
            this.property = property;
            this.primaryKey = (oneToManyAttribute.HasPrimaryKey && !BaseType.IsEntityType()) ? new PrimaryKeyInfo(oneToManyAttribute.PrimaryKeyName, oneToManyAttribute.PrimaryKeyType, oneToManyAttribute.PrimaryKeyIsAutoIncrement) : null;
            this.foreignKey = oneToManyAttribute.HasForeignKey ? new ForeignKeyInfo(oneToManyAttribute.ForeignKeyName, oneToManyAttribute.ForeignKeyType) : null;
            this.sequence = oneToManyAttribute.HasSequence ? new SequenceInfo(oneToManyAttribute.SequenceName, oneToManyAttribute.SequenceType) : null;
            this.table = BaseType.GetTableInfo();
        }

        public ChildInfo(string tableName, PropertyInfo property, PrimaryKeyInfo primaryKey, ForeignKeyInfo foreignKey, SequenceInfo sequence)
        {
            this.tableName = tableName;
            this.property = property;
            this.primaryKey = primaryKey;
            this.foreignKey = foreignKey;
            this.sequence = sequence;
            this.table = BaseType.GetTableInfo();
        }

        private readonly string tableName;
        private readonly PropertyInfo property;
        private readonly PrimaryKeyInfo primaryKey;
        private readonly ForeignKeyInfo foreignKey;
        private readonly SequenceInfo sequence;
        private readonly TableInfo table;

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

        public Type BaseType
        {
            get { return property.PropertyType.GetGenericArguments()[0]; }
        }

        public TableInfo BaseTable
        {
            get { return table; }
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

        public override string ToString()
        {
            return string.Format("CHILD {0} ({1})", tableName, BaseType.Name);
        }
    }
}
