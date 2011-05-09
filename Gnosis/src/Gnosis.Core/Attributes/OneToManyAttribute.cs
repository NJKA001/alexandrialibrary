using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OneToManyAttribute : Attribute
    {
        public OneToManyAttribute(string name)
        {
            this.name = name;
        }

        public OneToManyAttribute(string name, string primaryKeyName, Type primaryKeyType)
        {
            this.name = name;
            this.primaryKeyName = primaryKeyName;
            this.primaryKeyType = primaryKeyType;
        }

        public OneToManyAttribute(string name, string primaryKeyName, Type primaryKeyType, string foreignKeyName, Type foreignKeyType)
        {
            this.name = name;
            this.primaryKeyName = primaryKeyName;
            this.primaryKeyType = primaryKeyType;
            this.foreignKeyName = foreignKeyName;
            this.foreignKeyType = foreignKeyType;
        }

        public OneToManyAttribute(string name, string primaryKeyName, Type primaryKeyType, string foreignKeyName, Type foreignKeyType, string sequenceName, Type sequenceType)
        {
            this.name = name;
            this.primaryKeyName = primaryKeyName;
            this.primaryKeyType = primaryKeyType;
            this.foreignKeyName = foreignKeyName;
            this.foreignKeyType = foreignKeyType;
            this.hasSequence = true;
            this.sequenceName = sequenceName;
            this.sequenceType = sequenceType;
        }

        private readonly string name;
        private bool hasPrimaryKey = true;
        private readonly string primaryKeyName = "Id";
        private readonly Type primaryKeyType = typeof(int);
        private bool hasForeignKey = true;
        private readonly string foreignKeyName = "Parent";
        private readonly Type foreignKeyType = typeof(Guid);
        private bool hasSequence;
        private readonly string sequenceName = "Sequence";
        private readonly Type sequenceType = typeof(int);

        public string Name
        {
            get { return name; }
        }

        public bool HasPrimaryKey
        {
            get { return hasPrimaryKey; }
            set { hasPrimaryKey = value; }
        }

        public string PrimaryKeyName
        {
            get { return primaryKeyName; }
        }

        public Type PrimaryKeyType
        {
            get { return primaryKeyType; }
        }

        public bool HasForeignKey
        {
            get { return hasForeignKey; }
            set { hasForeignKey = value; }
        }

        public string ForeignKeyName
        {
            get { return foreignKeyName; }
        }

        public Type ForeignKeyType
        {
            get { return foreignKeyType; }
        }

        public bool HasSequence
        {
            get { return hasSequence; }
            set { hasSequence = value; }
        }

        public string SequenceName
        {
            get { return sequenceName; }
        }

        public Type SequenceType
        {
            get { return sequenceType; }
        }
    }
}
