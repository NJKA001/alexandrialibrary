using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Choices
{
    public interface IChooseTopOrBottomOfDeck
        : IChoice
    {
        bool TopOfDeck { get; }
    }
}
