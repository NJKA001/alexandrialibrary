using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class CardViewModel
    {
        public CardViewModel(Card card)
        {
            _card = card;
        }

        private Card _card;

        public string Id
        {
            get { return _card.Id; }
        }

        public string Title
        {
            get { return _card.Title; }
        }

        public string FullTitle
        {
            get
            {
                return !string.IsNullOrEmpty(OppositeTitle) ?
                    string.Format("{0} ({1})", Title, OppositeTitle)
                    : Title;
            }
        }

        public string SetName
        {
            get { return _card.CardSet.Name; }
        }

        public string Number
        {
            get { return _card.Number.ToString(); }
        }

        public string Quantity
        {
            get { return _card.Quantity.ToString(); }
        }

        public IEnumerable<string> Keywords
        {
            get { return _card.Keywords; }
        }

        public IEnumerable<string> Traits
        {
            get { return _card.Traits; }
        }

        public CardType CardType
        {
            get { return _card.CardType; }
        }

        public string Sphere
        {
            get { return _card.Sphere.ToString(); }
        }

        public string ThreatCost
        {
            get { return _card.ThreatCost.ToString(); }
        }

        public string ResourceCost
        {
            get { return _card.ResourceCost.ToString(); }
        }

        public string Willpower
        {
            get { return _card.Willpower.ToString(); }
        }

        public string Attack
        {
            get { return _card.Attack.ToString(); }
        }

        public string Defense
        {
            get { return _card.Defense.ToString(); }
        }

        public string HitPoints
        {
            get { return _card.HitPoints.ToString(); }
        }

        public string QuestPoints
        {
            get { return _card.QuestPoints.ToString(); }
        }

        public string Threat
        {
            get { return _card.Threat.ToString(); }
        }

        public string EngagementCost
        {
            get { return _card.EngagementCost.ToString(); }
        }

        public string Text
        {
            get { return _card.Text; }
        }

        public string Shadow
        {
            get { return _card.Shadow; }
        }

        public string FlavorText
        {
            get { return _card.FlavorText; }
        }

        public string OppositeFlavorText
        {
            get { return _card.OppositeFlavorText; }
        }

        public string OppositeTitle
        {
            get { return _card.OppositeTitle; }
        }

        public string OppositeText
        {
            get { return _card.OppositeText; }
        }

        public string ImagePath
        {
            get
            {
                return string.IsNullOrEmpty(_card.ImageName) ?
                    string.Format("/Images/Cards/{0}/{1}.jpg", _card.CardSet.Name.ToUrlSafeString(), Title.ToUrlSafeString())
                    : string.Format("/Images/Cards/{0}.png", _card.ImageName.ToUrlSafeString());
            }
        }

        public string DetailPath
        {
            get { return string.Format("/Cards/Details/{0}", Id); }
        }

        public bool HasCharacterStats
        {
            get
            {
                return _card.Willpower > 0 || _card.Attack > 0 || _card.Defense > 0 || _card.HitPoints > 0;
            }
        }

    }
}