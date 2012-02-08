using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models
{
    public abstract class OutlineBase<T>
        : IOutline<T>
        where T : IEntity
    {
        private Guid id;
        private bool isInitialized;

        protected abstract void InitializeProperties(IDataRecord record);

        #region IOutline<T> Members

        public Guid Id { get { return id; } }

        public void Initialize(IDataRecord record)
        {
            if (isInitialized)
                throw new InvalidOperationException("Cannot initialize outline, outline is already initialized");

            id = record.GetGuid("Id");
            isInitialized = true;

            InitializeProperties(record);
        }

        #endregion
    }
}
