using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public abstract class ModelBase :
        IModel
    {
        protected ModelBase(IModelContext modelContext)
        {
            this.modelContext = modelContext;
            this.id = Guid.NewGuid();
            isNew = true;
        }

        protected ModelBase(IModelContext modelContext, Guid id)
        {
            this.modelContext = modelContext;
            this.id = id;
        }

        private readonly IModelContext modelContext;
        private readonly Guid id;
        private readonly bool isNew;

        public IModelContext ModelContext
        {
            get { return modelContext; }
        }

        public Guid Id
        {
            get { return id; }
        }

        public bool IsNew
        {
            get { return isNew; }
        }
    }
}
