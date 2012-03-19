using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gnosis.Data
{
    public class EntityInitialState
        : IEntityInitialState
    {
        public EntityInitialState()
            : this(Guid.Empty, 0)
        {
        }

        public EntityInitialState(Guid parent)
            : this(parent, 0)
        {
        }

        public EntityInitialState(Guid parent, uint sequence)
        {
            this.id = Guid.NewGuid();
            this.timeStamp = DateTime.Now.ToUniversalTime();
            this.parent = parent;
            this.sequence = sequence;
            this.isNew = true;
            this.record = null;
        }

        public EntityInitialState(IDataRecord record)
        {
            this.id = record.GetGuid("Id");
            this.timeStamp = record.GetDateTime("TimeStamp");
            this.parent = record.GetOrdinal("Parent") > -1 ? record.GetGuid("Parent") : Guid.Empty;
            this.sequence = record.GetOrdinal("Sequence") > -1 ? record.GetUInt32("Sequence") : 0;
            this.isNew = false;
            this.record = record;
        }

        private readonly Guid id;
        private readonly DateTime timeStamp;
        private readonly Guid parent;
        private readonly uint sequence;
        private readonly bool isNew;
        private readonly IDataRecord record;

        #region IEntityInitialState Members

        public Guid Id
        {
            get { return id; }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
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

        public void Initialize(string name, Action<string, IDataRecord> action)
        {
            if (record != null && record.GetOrdinal(name) > -1)
                action(name, record);
        }

        #endregion
    }
}
