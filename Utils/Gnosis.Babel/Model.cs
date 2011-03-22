using System;

namespace Gnosis.Babel
{
    public abstract class Model : IModel
    {
        protected virtual void OnInitialize()
        {
        }

        public object Id { get; private set; }

        public bool IsNew
        {
            get { return Id == null; }
        }

        public void Initialize(object id)
        {
            if (Id != null)
                throw new InvalidOperationException("This model has already been initialized. A model cannot be initialized twice.");

            OnInitialize();

            Id = id;
        }
    }
}
