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
        private readonly List<CardEffect> _keywordEffects = new List<CardEffect>();
        private readonly List<CardEffect> _textEffects = new List<CardEffect>();
        private readonly List<CardEffect> _shadowEffects = new List<CardEffect>();

        public List<CardEffect> KeywordEffects { get { return _keywordEffects; } }
        public List<CardEffect> TextEffects { get { return _textEffects; } }
        public List<CardEffect> ShadowEffects { get { return _shadowEffects; } }

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

        public string CardTypeName
        {
            get { return _card.CardType.ToString().Replace('_', '-'); }
        }

        public string CampaignCardType
        {
            get
            {
                return (_card.CampaignCardType == Models.CampaignCardType.Boon || _card.CampaignCardType == Models.CampaignCardType.Burden) ?
                    _card.CampaignCardType.ToString()
                    : (string)null;

            }
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
            get { 
                return _card.IsVariableCost ?
                    "X"
                    : _card.ResourceCost.ToString();
            }
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
            get {

                if (_card.IsVariableQuestPoints)
                    return "X";

                return _card.QuestPoints.HasValue ?
                    _card.QuestPoints.ToString()
                    : "-";
            }
        }

        public string Threat
        {
            get { return 
                _card.IsVariableThreat ?
                "X"
                : _card.Threat.ToString(); 
            }
        }

        public string VictoryPoints
        {
            get
            {
                return _card.VictoryPoints > 0 ?
                    string.Format("Victory: {0}", _card.VictoryPoints)
                    : (string)null;
            }
        }

        public string EngagementCost
        {
            get { return _card.EngagementCost.ToString(); }
        }

        public string Text
        {
            get {
                return !string.IsNullOrEmpty(_card.Text) ?
                    _card.Text.Replace("~", string.Empty)
                    : string.Empty;
            }
        }

        public string Shadow
        {
            get {
                return !string.IsNullOrEmpty(_card.Shadow) ?
                    _card.Shadow.Replace("~", string.Empty)
                    : string.Empty;
            }
        }

        public IEnumerable<string> FlavorText
        {
            get
            {
                return
                    !string.IsNullOrEmpty(_card.FlavorText) ?
                    _card.FlavorText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    : Enumerable.Empty<string>();
            }
        }

        public bool HasFlavorText
        {
            get { return FlavorText.Count() > 0; }
        }

        public IEnumerable<string> OppositeFlavorText
        {
            get
            {
                return
                    !string.IsNullOrEmpty(_card.OppositeFlavorText) ?
                    _card.OppositeFlavorText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    : Enumerable.Empty<string>();
            }
        }

        public bool HasOppositeFlavorText
        {
            get { return OppositeFlavorText.Count() > 0; }
        }


        public string OppositeTitle
        {
            get { return _card.OppositeTitle; }
        }

        public string OppositeText
        {
            get {
                return !string.IsNullOrEmpty(_card.OppositeText) ?
                    _card.OppositeText.Replace("~", string.Empty)
                    : string.Empty;
            }
        }

        public string ImagePath
        {
            get
            {
                var format = ImageType.Jpg;
                if (_card.ImageType != ImageType.None)
                    format = _card.ImageType;
                else if (!string.IsNullOrEmpty(_card.ImageName)) {
                    format = ImageType.Png;
                }

                var ext = string.Format(".{0}", format.ToString().ToLower());

                return string.IsNullOrEmpty(_card.ImageName) ?
                    string.Format("/Images/Cards/{0}/{1}{2}", _card.CardSet.Name.ToUrlSafeString(), Title.ToUrlSafeString(), ext)
                    : string.Format("/Images/Cards/{0}{1}", _card.ImageName.ToUrlSafeString(), ext);
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

        public bool IsUnique
        {
            get { return _card.IsUnique; }
        }

        public bool HasSphere
        {
            get { return _card.Sphere == Models.Sphere.Leadership || _card.Sphere == Models.Sphere.Tactics || _card.Sphere == Models.Sphere.Spirit || _card.Sphere == Models.Sphere.Lore; }
        }

        /*
        private CardEffectViewModel GetEffect(string text, bool isLastLine)
        {
            string prefix = null;

            var index = text.IndexOf(':');
            if (index > -1 && index < 20)
            {
                prefix = text.Substring(0, index);
                text = text.Substring(index + 1, text.Length - index - 1).Trim();
            }

            var isCritical = false;
            if (isLastLine && prefix == null && (text.Contains(" must") || text.Contains(" cannot") || text.Contains(" won") || text.Contains(" win")) || text.Contains(" lose") || text.Contains(" lost"))
                isCritical = true;

            return new CardEffectViewModel() { Prefix = prefix, Text = text, IsCritical = isCritical };
        }

        public IEnumerable<CardEffectViewModel> GetCardEffects()
        {
            var effects = new List<CardEffectViewModel>();

            if (!string.IsNullOrEmpty(Text))
            {
                var lines = Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var count = 0;
                var isLastLine = false;
                foreach (var line in lines)
                {
                    count++;
                    isLastLine = (count == lines.Length);
                    if (!string.IsNullOrEmpty(line))
                    {
                        effects.Add(GetEffect(line, isLastLine));
                    }
                }
            }

            if (!string.IsNullOrEmpty(OppositeText))
            {
                var lines = OppositeText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var count = 0;
                var isLastLine = false;
                foreach (var line in lines)
                {
                    count++;
                    isLastLine = (count == lines.Length);
                    if (!string.IsNullOrEmpty(line))
                    {
                        effects.Add(GetEffect(line, isLastLine));
                    }
                }
            }

            if (!string.IsNullOrEmpty(Shadow))
            {
                effects.Add(GetEffect(Shadow, false));
            }

            return effects;
        }
        */
    }
}