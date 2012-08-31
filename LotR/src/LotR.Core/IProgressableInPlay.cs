using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IProgressableInPlay
        : ICardInPlay
    {
        new IProgressableCard Card { get; }
        byte ProgressTokens { get; }

        void AddProgressTokens(byte value);
        void RemoveProgressTokens(byte value);
    }
}
