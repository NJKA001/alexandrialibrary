using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;

namespace LotR.Effects
{
    public class EffectHandle
        : IEffectHandle
    {
        public EffectHandle()
            : this(null, null, null)
        {
        }

        public EffectHandle(IChoice choice)
            : this(choice, null, null)
        {
        }

        public EffectHandle(IChoice choice, ICost cost)
            : this(choice, cost, null)
        {
        }

        public EffectHandle(IChoice choice, ILimit limit)
            : this(choice, null, limit)
        {
        }

        public EffectHandle(ICost cost)
            : this(null, cost, null)
        {
        }

        public EffectHandle(ICost cost, ILimit limit)
            : this(null, cost, limit)
        {
        }

        public EffectHandle(IChoice choice, ICost cost, ILimit limit)
        {
            this.choice = choice;
            this.cost = cost;
            this.limit = limit;
        }

        private readonly IChoice choice;
        private readonly ICost cost;
        private readonly ILimit limit;
        
        private IPayment payment;
        private bool isCancelled;
        private bool isResolved;
        private bool isAccepted;
        private bool isRejected;
        private string status;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public IChoice Choice
        {
            get { return choice; }
        }

        public ICost Cost
        {
            get { return cost; }
        }

        public ILimit Limit
        {
            get { return limit; }
        }

        public IPayment Payment
        {
            get { return payment; }
        }

        public bool IsCancelled
        {
            get { return isCancelled; }
            protected set
            {
                if (isCancelled == value)
                    return;

                isCancelled = value;
                OnPropertyChanged("IsCancelled");
            }
        }

        public bool IsResolved
        {
            get { return isResolved; }
            set
            {
                if (isResolved == value)
                    return;

                isResolved = value;
                OnPropertyChanged("IsCompleted");
            }
        }

        public bool IsAccepted
        {
            get { return isAccepted; }
            protected set
            {
                if (isAccepted == value)
                    return;

                isAccepted = value;

                OnPropertyChanged("IsAccepted");
            }
        }

        public bool IsRejected
        {
            get { return isRejected; }
            set
            {
                if (isRejected == value)
                    return;

                isRejected = true;
                OnPropertyChanged("IsRejected");
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                if (status == value)
                    return;

                status = value;

                OnPropertyChanged("Status");
            }
        }

        public void AddPayment(IPayment payment)
        {
            if (payment == null)
                throw new ArgumentNullException("payment");

            if (this.payment != null)
                throw new InvalidOperationException("payment is already defined, cannot add multiple payments");

            this.payment = payment;
        }

        public void Accept()
        {
            if (isRejected)
                throw new InvalidOperationException("effect is already rejected, cannot accept");

            IsAccepted = true;
        }

        public void Reject()
        {
            if (isAccepted)
                throw new InvalidOperationException("effect is already accepted, cannot reject");

            IsRejected = true;
        }

        public void Cancel(string status)
        {
            if (status == null)
                throw new ArgumentNullException("status");

            if (isResolved)
                throw new InvalidOperationException("effect is already resolved, cannot cancel");

            Status = status;
            IsCancelled = true;
        }

        public void Resolve(string status)
        {
            if (status == null)
                throw new ArgumentNullException("status");

            if (isCancelled)
                throw new InvalidOperationException("effect is already cancelled, cannot resolve");

            Status = status;
            IsResolved = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
