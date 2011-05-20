using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ForeignKeyInfo
    {
        public ForeignKeyInfo(string tableName)
        {
            this.tableName = tableName;
        }

        public ForeignKeyInfo(string tableName, PrimaryKeyInfo primaryKey)
        {
            this.tableName = tableName;
            this.primaryKey = primaryKey;
        }

        private ForeignKeyInfo(string tableName, PrimaryKeyInfo primaryKey, string foreignKeyName, Type foreignKeyType, string sequenceName, Type sequenceType)
        {
            this.tableName = tableName;
            this.primaryKey = primaryKey;
            this.foreignKeyName = foreignKeyName;
            this.foreignKeyType = foreignKeyType;
            this.sequenceName = sequenceName;
            this.sequenceType = sequenceType;
        }

        private readonly string tableName;
        private readonly string foreignKeyName;
        private readonly Type foreignKeyType;
        private readonly string sequenceName;
        private readonly Type sequenceType;
        private readonly PrimaryKeyInfo primaryKey;

        public string TableName
        {
            get { return tableName; }
        }

        public PrimaryKeyInfo PrimaryKey
        {
            get { return primaryKey; }
        }

        public string ForeignKeyName
        {
            get { return foreignKeyName; }
        }

        public Type ForeignKeyType
        {
            get { return foreignKeyType; }
        }

        public string SequenceName
        {
            get { return sequenceName; }
        }

        public Type SequenceType
        {
            get { return sequenceType; }
        }

        public TableInfo GetTable()
        {
            return null;
        }
    }
}
