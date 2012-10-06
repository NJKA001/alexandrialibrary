using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Choices
{
    public interface IChoosePlayer
        : IChoice
    {
        IPlayer Player { get; set; }
    }
}
