using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ICardInPlay
        : INotifyPropertyChanged
    {
        bool IsInHouse { get; }
        bool IsMoribund { get; }
        bool IsStanding { get; }
        bool IsCompletelyBlank { get; }
        bool IsBlankExceptForTraits { get; }

        CardType ActingAs { get; }
        ICardInPlay AttachedTo { get; }
        ICard Card { get; }
        IEnumerable<ICardInPlay> Attachments { get; }
        IEnumerable<IModifier> Modifiers { get; }
        IEnumerable<Crest> Crests { get; }

        byte GoldTokens { get; }
        byte PowerTokens { get; }
        byte OverallStrength { get; }
        bool HasMilitaryIcon { get; }
        bool HasIntrigueIcon { get; }
        bool HasPowerIcon { get; }

        void BecomeMoribund();
        void SaveFromMoribund();

        void AddAttachment(ICardInPlay attachment);
        void RemoveAttachment(ICardInPlay attachment);

        void AddModifier(IModifier modifier);
        void RemoveModifier(IModifier modifier);

        void AddGoldToken();
        void RemoveGoldToken();

        void AddPowerToken();
        void RemovePowerToken();

        void Kneel();
        void Stand();
    }
}
