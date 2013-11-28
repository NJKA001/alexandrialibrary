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

        public string SetName
        {
            get { return _card.CardSet.Name; }
        }

        public IEnumerable<string> Keywords
        {
            get { return _card.Keywords; }
        }

        public IEnumerable<string> Traits
        {
            get { return _card.Traits; }
        }

        public string CardType
        {
            get { return _card.CardType.ToString(); }
        }

        public string Text
        {
            get { return _card.Text; }
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

    }
}