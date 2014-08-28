using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class CardSet
    {
        protected CardSet()
        {
            Cards = new List<Card>();

            Initialize();

            foreach (var card in Cards)
            {
                card.CardSet = this;
            }
        }

        private string normalizedName;

        protected virtual void Initialize()
        {
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        
        public string NormalizedName
        {
            get
            {
                return (!string.IsNullOrEmpty(normalizedName)) ?
                    normalizedName
                    : Name;
            }
            protected set
            {
                normalizedName = value;
            }
        }

        public string Abbreviation { get; protected set; }

        public bool DisablePublicImages { get; set; }

        private string publicSlug;
        public string PublicSlug
        {
            get { return !string.IsNullOrEmpty(publicSlug) ? publicSlug : Abbreviation.ToLower(); }
            protected set { publicSlug = value; }
        }

        public string Cycle { get; protected set; }
        public int Number { get; protected set; }
        public SetType SetType { get; protected set; }
        public List<Card> Cards { get; protected set; }

        static CardSet()
        {
        }

        public static CardSet CoreSet = new Sets.CoreSet();
        public static CardSet TheHuntForGollum = new Sets.TheHuntforGollum();
        public static CardSet ConflictAtTheCarrock = new Sets.ConflictattheCarrock();
        public static CardSet AJourneyToRhosgobel = new Sets.AJourneytoRhosgobel();
        public static CardSet TheHillsOfEmynMuil = new Sets.TheHillsofEmynMuil();
        public static CardSet TheDeadMarshes = new Sets.TheDeadMarshes();
        public static CardSet ReturnToMirkwood = new Sets.ReturntoMirkwood();
        
        public static CardSet KhazadDum = new Sets.Khazaddum();
        public static CardSet TheRedhornGate = new Sets.TheRedhornGate();
        public static CardSet RoadToRivendell = new Sets.RoadtoRivendell();
        public static CardSet TheWatcherInTheWater = new Sets.TheWatcherintheWater();
        public static CardSet TheLongDark = new Sets.TheLongDark();
        public static CardSet FoundationsOfStone = new Sets.FoundationsofStone();
        public static CardSet ShadowAndFlame = new Sets.ShadowandFlame();

        public static CardSet HeirsOfNumenor = new Sets.HeirsofNumenor();
        public static CardSet TheStewardsFear = new Sets.TheStewardsFear();
        public static CardSet TheDruadanForest = new Sets.TheDruadanForest();
        public static CardSet EncounterAtAmonDin = new Sets.EncounteratAmonDin();
        public static CardSet AssaultOnOsgiliath = new Sets.AssaultonOsgiliath();
        public static CardSet TheBloodOfGondor = new Sets.TheBloodofGondor();
        public static CardSet TheMorgulVale = new Sets.TheMorgulVale();

        public static CardSet TheVoiceOfIsengard = new Sets.VoiceofIsengard();
        public static CardSet TheDunlandTrap = new Sets.TheDunlandTrap();
        public static CardSet TheThreeTrials = new Sets.TheThreeTrials();

        public static CardSet TheHobbitOverHillAndUnderHill = new Sets.TheHobbitOverHillandUnderHill();
        public static CardSet TheHobbitOnTheDoorstep = new Sets.TheHobbitOntheDoorstep();

        public static CardSet TheBlackRiders = new Sets.TheBlackRiders();

        public static CardSet TheMassingAtOsgiliath = new Sets.TheMassingatOsgiliath();
        public static CardSet TheBattleOfLakeTown = new Sets.TheBattleofLakeTown();
        public static CardSet TheStoneOfErech = new Sets.TheStoneofErech();

        public static CardSet PassageThroughMirkwoodNightmare = new Sets.PassageThroughMirkwoodNightmare();
        public static CardSet JourneyAlongTheAnduinNightmare = new Sets.JourneyAlongTheAnduinNightmare();
        public static CardSet EscapeFromDolGuldurNightmare = new Sets.EscapeFromDolGuldurNightmare();
        public static CardSet TheHuntForGollumNightmare = new Sets.TheHuntForGollumNightmare();
        public static CardSet ConflictAtTheCarrockNightmare = new Sets.ConflictAtTheCarrockNightmare();
        public static CardSet AJourneyToRhosgobelNightmare = new Sets.AJourneyToRhosgobelNightmare();
        public static CardSet TheHillsOfEmynMuilNightmare = new Sets.TheHillsOfEmynMuilNightmare();
        public static CardSet TheDeadMarshesNightmare = new Sets.TheDeadMarshesNightmare();
        public static CardSet ReturnToMirkwoodNightmare = new Sets.ReturnToMirkwoodNightmare();
        public static CardSet IntoThePitNightmare = new Sets.IntoThePitNightmare();
        public static CardSet TheSeventhLevelNightmare = new Sets.TheSeventhLevelNightmare();
        public static CardSet FlightFromMoriaNightmare = new Sets.FlightFromMoriaNightmare();
        public static CardSet WeMustAwayEreBreakOfDayNightmare = new Sets.WeMustAwayEreBreakOfDayNightmare();
        public static CardSet OverTheMistyMountainsGrimNightmare = new Sets.OverTheMistyMountainsGrimNightmare();
        public static CardSet DungeonsDeepAndCavernsDimNightmare = new Sets.DungeonsDeepAndCavernsDimNightmare();

        public static CardSet TheRedhornGateNightmare = new Sets.TheRedhornGateNightmare();

        //public static CardSet FirstAge = new Sets.FirstAge();
    }
}