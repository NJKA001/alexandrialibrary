using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseAttachmentHost
        : IChoice
    {
        IAttachableCard Attachment { get; }
        
        IAttachmentHostInPlay ChosenAttachmentHost { get; set; }
    }
}
