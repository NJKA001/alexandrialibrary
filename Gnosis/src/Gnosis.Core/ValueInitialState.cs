using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class ValueInitialState
        : IValueInitialState
    {
        public ValueInitialState(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            this.id = record.GetGuid("Id");
            this.parent = record.GetGuid("Parent");
            this.sequence = record.GetUInt32("Sequence");
            this.isNew = false;
            this.record = record;
        }

        private readonly Guid id;
        private readonly Guid parent;
        private readonly uint sequence;
        private readonly bool isNew;
        private readonly IDataRecord record;

        public Guid Id
        {
            get { return id; }
        }

        public Guid Parent
        {
            get { return parent; }
        }

        public uint Sequence
        {
            get { return sequence; }
        }

        public bool IsNew
        {
            get { return isNew; }
        }

        public void Initialize(string name, Action<object> action)
        {
            if (record != null && record.GetOrdinal(name) > -1)
                action(record[name]);
        }
    }
}
