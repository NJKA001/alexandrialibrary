using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OneToManyAttribute : Attribute
    {
        public OneToManyAttribute(string tableName)
        {
            this.tableName = tableName;
        }

        public OneToManyAttribute(string tableName, string foreignKeyName, Type foreignKeyType)
        {
            this.tableName = tableName;
            this.foreignKeyName = foreignKeyName;
            this.foreignKeyType = foreignKeyType;
        }

        public OneToManyAttribute(string tableName, string foreignKeyName, Type foreignKeyType, string sequenceName, Type sequenceType)
        {
            this.tableName = tableName;
            this.foreignKeyName = foreignKeyName;
            this.foreignKeyType = foreignKeyType;
            this.sequenceName = sequenceName;
            this.sequenceType = sequenceType;
        }

        private readonly string tableName;
        private bool hasForeignKey = true;
        private string foreignKeyName = "Parent";
        private Type foreignKeyType = typeof(Guid);
        private bool hasSequence = true;
        private string sequenceName = "Sequence";
        private Type sequenceType = typeof(int);

        public string TableName
        {
            get { return tableName; }
        }

        public bool HasForeignKey
        {
            get { return hasForeignKey; }
            set { hasForeignKey = value; }
        }

        public string ForeignKeyName
        {
            get { return foreignKeyName; }
            set { foreignKeyName = value; }
        }

        public Type ForeignKeyType
        {
            get { return foreignKeyType; }
            set { foreignKeyType = value; }
        }

        public bool HasSequence
        {
            get { return hasSequence; }
            set { hasSequence = value; }
        }

        public string SequenceName
        {
            get { return sequenceName; }
            set { sequenceName = value; }
        }

        public Type SequenceType
        {
            get { return sequenceType; }
            set { sequenceType = value; }
        }
    }
}
