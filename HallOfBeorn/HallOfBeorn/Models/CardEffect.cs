using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class CardEffect
    {
        public CardEffectType EffectType { get; set; }
        public string Text { get; set; }

        public string Prefix
        {
            get
            {
                switch (EffectType)
                {
                    case CardEffectType.Action:
                        return "Action: ";
                    case CardEffectType.Forced:
                        return "Forced: ";
                    case CardEffectType.Response:
                        return "Response: ";
                    case CardEffectType.Setup:
                        return "Setup: ";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}