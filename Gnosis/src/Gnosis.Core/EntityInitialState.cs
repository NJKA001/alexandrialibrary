using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class EntityInitialState
        : IEntityInitialState
    {
        public EntityInitialState(IContext context, ILogger logger)
            : this(context, logger, Guid.Empty, 0)
        {
        }

        public EntityInitialState(IContext context, ILogger logger, Guid parent)
            : this(context, logger, parent, 0)
        {
        }

        public EntityInitialState(IContext context, ILogger logger, Guid parent, uint sequence)
        {
            this.context = context;
            this.logger = logger;
            this.id = Guid.NewGuid();
            this.timeStamp = DateTime.Now.ToUniversalTime();
            this.parent = parent;
            this.sequence = sequence;
            this.isNew = true;
            this.record = null;
        }

        public EntityInitialState(IContext context, ILogger logger, IDataRecord record)
        {
            this.context = context;
            this.logger = logger;
            this.id = record.GetGuid("Id");
            this.timeStamp = record.GetDateTime("TimeStamp");
            this.parent = record.GetOrdinal("Parent") > -1 ? record.GetGuid("Parent") : Guid.Empty;
            this.sequence = record.GetOrdinal("Sequence") > -1 ? record.GetUInt32("Sequence") : 0;
            this.isNew = false;
            this.record = record;
        }

        private readonly IContext context;
        private readonly ILogger logger;
        private readonly Guid id;
        private readonly DateTime timeStamp;
        private readonly Guid parent;
        private readonly uint sequence;
        private readonly bool isNew;
        private readonly IDataRecord record;

        public IContext Context
        {
            get { return context; }
        }

        public ILogger Logger
        {
            get { return logger; }
        }

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
    }
}
