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
    public interface IEffectHandle
        : INotifyPropertyChanged
    {
        IEffect Effect { get; }
        IChoice Choice { get; }
        ICost Cost { get; }
        ILimit Limit { get; }
        IPayment Payment { get; }

        bool IsCancelled { get; }
        bool IsResolved { get; }
        bool IsAccepted { get; }
        bool IsRejected { get; }
        string Status { get; }
        
        void AddPayment(IPayment payment);

        void Accept();
        void Reject();
        void Cancel(string status);
        void Resolve(string status);
    }
}
