using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IHouse
        : ICard
    {
        HouseType HouseType { get; }

        IEnumerable<ICard> Attachments { get; }

        byte GoldTokens { get; }
        byte PowerTokens { get; }

        void AddGoldToken();
        void RemoveGoldToken();

        void AddPowerToken();
        void RemovePowerToken();

        void AddAttachment(ICard card);
        void RemoveAttachment(ICard card);
    }
}
