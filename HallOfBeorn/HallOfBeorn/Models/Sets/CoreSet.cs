using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class CoreSet : CardSet
    {
        protected override void Initialize()
        {
            Name = "Core Set";
            Abbreviation = "Core";
            Number = 1;
            SetType = Models.SetType.Core;

            Cards.Add(new Card() {
                ImageName = "M1001",
                Title = "Aragorn",
                Id = "51223bd0-ffd1-11df-a976-0801200c9001",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 12,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                Willpower = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Dúnedain.", " Noble.", " Ranger." },
                NormalizedTraits = new List<string> { "Dunedain." },
                Text = "Response: After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him.",
                Keywords = new List<string>() { "Sentinel." },
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1002",
                Title = "Theodred",
                Id = "51223bd0-ffd1-11df-a976-0801200c9002",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 8,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Noble.", " Rohan.", " Warrior." },
                Text = "Response: After Theodred commits to a quest, choose a hero committed to that quest. Add 1 resource to that hero's resource pool.",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1003",
                Title = "Glóin",
                NormalizedTitle = "Gloin",
                Id = "51223bd0-ffd1-11df-a976-0801200c9003",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Dwarf.", " Noble." },
                Text = "Response: After Gloin suffers damage, add 1 resource to his resource pool for each point of damage he just suffered.",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1004",
                Title = "Gimli",
                Id = "51223bd0-ffd1-11df-a976-0801200c9004",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                ThreatCost = 11,
                IsUnique = true,
                Attack = 2,
                Defense = 2,
                Willpower = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Dwarf.", " Noble.", " Warrior." },
                Text = "Gimli gets +1 Attack for each damage token on him.",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1005",
                Title = "Legolas",
                Id = "51223bd0-ffd1-11df-a976-0801200c9005",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 3,
                Defense = 1,
                Willpower = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Noble.", " Silvan.", " Warrior." },
                Text = "Response: After Legolas participates in an attack that destroys an enemy, place 2 progress tokens on the current quest.",
                Keywords = new List<string>() { "Ranged." },
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1006",
                Title = "Thalin",
                Id = "51223bd0-ffd1-11df-a976-0801200c9006",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 2,
                Defense = 2,
                Willpower = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Dwarf.", " Warrior." },
                Text = "While Thalin is committed to a quest, deal 1 damage to each enemy as it is revealed by the encounter deck.",
                Quantity = 1,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1007",
                Title = "Éowyn",
                NormalizedTitle = "Eowyn",
                Id = "51223bd0-ffd1-11df-a976-0801200c9007",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 4,
                HitPoints = 3,
                Traits = new List<string>() { "Noble.", " Rohan." },
                Text = "Action: Discard 1 card from your hand to give Éowyn +1 Willpower until the end of the phase. This effect may be triggered by each player once each round.",
                Quantity = 1,
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1008",
                Title = "Eleanor",
                Id = "51223bd0-ffd1-11df-a976-0801200c9008",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 7,
                IsUnique = true,
                Attack = 1,
                Defense = 2,
                Willpower = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Noble." },
                Text = "Response: Exhaust Eleanor to cancel the 'when revealed' effects of a treachery card just revealed by the encounter deck. Then, discard that card, and replace it with the next card from the encounter deck.",
                Quantity = 1,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1009",
                Title = "Dúnhere",
                NormalizedTitle = "Dunhere",
                Id = "51223bd0-ffd1-11df-a976-0801200c9009",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 8,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Rohan.", " Warrior." },
                Text = "Dunhere can target enemies in the staging area when he attacks alone. When doing so, he gets +1 Attack.",
                Quantity = 1,
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1010",
                Title = "Denethor",
                Id = "51223bd0-ffd1-11df-a976-0801200c9010",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                ThreatCost = 8,
                IsUnique = true,
                Attack = 1,
                Defense = 3,
                Willpower = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Noble.", " Steward." },
                Text = "Action: Exhaust Denethor to look at the top card of the encounter deck. You may move that card to the bottom of the deck.",
                Quantity = 1,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1011",
                Title = "Glorfindel",
                Id = "51223bd0-ffd1-11df-a976-0801200c9011",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                ThreatCost = 12,
                IsUnique = true,
                Attack = 3,
                Defense = 1,
                Willpower = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Noble.", " Noldor.", " Warrior." },
                Text = "Action: Pay 1 resource from Glorfindel's pool to heal 1 damage on any character. (Limit once per round.)",
                Quantity = 1,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1012",
                Title = "Beravor",
                Id = "51223bd0-ffd1-11df-a976-0801200c9012",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                ThreatCost = 10,
                IsUnique = true,
                Attack = 2,
                Defense = 2,
                Willpower = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Dúnedain.", " Ranger." },
                NormalizedTraits = new List<string> { "Dunedain." },
                Text = "Action: Exhaust Beravor to choose a player. That player draws 2 cards. Limit once per round.",
                Quantity = 1,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1013",
                Title = "Guard of the Citadel",
                Id = "51223bd0-ffd1-11df-a976-0801200c9013",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Attack = 1,
                Defense = 0,
                Willpower = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Gondor.", " Warrior." },
                Quantity = 3,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1014",
                Title = "Faramir",
                Id = "51223bd0-ffd1-11df-a976-0801200c9014",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 4,
                IsUnique = true,
                Attack = 1,
                Defense = 2,
                Willpower = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Noble.", " Ranger." },
                Text = "Action: Exhaust Faramir to choose a player. Each character controlled by that player gets +1 Willpower until the end of the phase.",
                Quantity = 2,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1015",
                Title = "Son of Arnor",
                Id = "51223bd0-ffd1-11df-a976-0801200c9015",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 3,
                Attack = 2,
                Defense = 0,
                Willpower = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Dúnedain." },
                NormalizedTraits = new List<string> { "Dunedain." },
                Text = "Response: After Son of Arnor enters play, choose an enemy card in the staging area or currently engaged with another player. Engage that enemy.",
                Quantity = 2,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1016",
                Title = "Snowbourn Scout",
                Id = "51223bd0-ffd1-11df-a976-0801200c9016",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Attack = 0,
                Defense = 1,
                Willpower = 0,
                HitPoints = 1,
                Traits = new List<string>() { "Rohan.", " Scout." },
                Text = "Response: After Snowbourn Scout enters play, choose a location. Place 1 progress token on that location.",
                Quantity = 3,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1017",
                Title = "Silverlode Archer",
                Id = "51223bd0-ffd1-11df-a976-0801200c9017",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 3,
                Attack = 2,
                Defense = 0,
                Willpower = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Archer.", " Silvan." },
                Keywords = new List<string>() { "Ranged." },
                Quantity = 2,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1018",
                Title = "Longbeard Orc Slayer",
                Id = "51223bd0-ffd1-11df-a976-0801200c9018",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 4,
                Attack = 2,
                Defense = 1,
                Willpower = 0,
                HitPoints = 3,
                Traits = new List<string>() { "Dwarf.", " Warrior." },
                Text = "Response: After Longbeard Orc Slayer enters play, deal 1 damage to each Orc enemy in play.",
                Quantity = 2,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1019",
                Title = "Brok Ironfist",
                Id = "51223bd0-ffd1-11df-a976-0801200c9019",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 6,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Dwarf.", " Warrior." },
                Text = "Response: After a Dwarf hero you control leaves play, put Brok Ironfist into play from your hand.",
                Quantity = 1,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1020",
                Title = "Ever Vigilant",
                Id = "51223bd0-ffd1-11df-a976-0801200c9020",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Text = "Action: Choose and ready 1 ally card.",
                Quantity = 2,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1021",
                Title = "Common Cause",
                Id = "51223bd0-ffd1-11df-a976-0801200c9021",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Text = "Action: Exhaust 1 hero you control to choose and ready a different hero.",
                Quantity = 2,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1022",
                Title = "For Gondor!",
                Id = "51223bd0-ffd1-11df-a976-0801200c9022",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Text = "Action: Until the end of the phase, all characters get +1 Attack. All Gondor characters also get +1 Defense until the end of the phase.",
                Quantity = 2,
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1023",
                Title = "Sneak Attack",
                Id = "51223bd0-ffd1-11df-a976-0801200c9023",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Text = "Action: Put 1 ally card into play from your hand. At the end of the phase, if that ally is still in play, return it to your hand.",
                Quantity = 2,
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1024",
                Title = "Valiant Sacrifice",
                Id = "51223bd0-ffd1-11df-a976-0801200c9024",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Text = "Response: After an ally card leaves play, that card's controller draws 2 cards.",
                Quantity = 2,
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1025",
                Title = "Grim Resolve",
                Id = "51223bd0-ffd1-11df-a976-0801200c9025",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 5,
                Text = "Action: Ready all character cards in play.",
                Quantity = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1026",
                Title = "Steward of Gondor",
                Id = "51223bd0-ffd1-11df-a976-0801200c9026",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                IsUnique = true,
                Traits = new List<string>() { "Gondor.", " Title." },
                Text = "Attached hero gains the Gondor trait.Action: Exhaust Steward of Gondor to add 2 resources to attached hero's resource pool.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 2,
                Number = 26
            });
            Cards.Add(new Card() {
                ImageName = "M1027",
                Title = "Celebrían's Stone",
                NormalizedTitle = "Celebrian's Stone",
                Id = "51223bd0-ffd1-11df-a976-0801200c9027",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                IsUnique = true,
                Traits = new List<string>() { "Artifact.", " Item." },
                Text = "Attached hero gains +2 Willpower.\r\nIf attached hero is Aragorn, he also gains a Spirit resource icon.",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                Quantity = 1,
                Number = 27
            });
            Cards.Add(new Card() {
                ImageName = "M1028",
                Title = "Veteran Axehand",
                Id = "51223bd0-ffd1-11df-a976-0801200c9028",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Attack = 2,
                Defense = 1,
                Willpower = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Dwarf.", " Warrior." },
                Quantity = 3,
                Number = 28
            });
            Cards.Add(new Card() {
                ImageName = "M1029",
                Title = "Gondorian Spearman",
                Id = "51223bd0-ffd1-11df-a976-0801200c9029",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Attack = 1,
                Defense = 1,
                Willpower = 0,
                HitPoints = 1,
                Traits = new List<string>() { "Gondor.", " Warrior." },
                Text = "Response: After Gondorian Spearman is declared as a defender, deal 1 damage to the attacking enemy.",
                Keywords = new List<string>() { "Sentinel." },
                Quantity = 3,
                Number = 29
            });
            Cards.Add(new Card() {
                ImageName = "M1030",
                Title = "Horseback Archer",
                Id = "51223bd0-ffd1-11df-a976-0801200c9030",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 3,
                Attack = 2,
                Defense = 1,
                Willpower = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Rohan.", " Archer." },
                Keywords = new List<string>() { "Ranged." },
                Quantity = 2,
                Number = 30
            });
            Cards.Add(new Card() {
                ImageName = "M1031",
                Title = "Beorn",
                Id = "51223bd0-ffd1-11df-a976-0801200c9031",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 6,
                IsUnique = true,
                Attack = 3,
                Defense = 3,
                Willpower = 1,
                HitPoints = 6,
                Traits = new List<string>() { "Beorning.", " Warrior." },
                Text = "Action: Beorn gains +5 Attack until the end of the phase. At the end of the phase in which you trigger this effect, shuffle Beorn back into your deck. (Limit once per round.)",
                Quantity = 1,
                Number = 31
            });
            Cards.Add(new Card() {
                ImageName = "M1032",
                Title = "Blade Mastery",
                Id = "51223bd0-ffd1-11df-a976-0801200c9032",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Text = "Action: Choose a character. Until the end of the phase, that character gains +1 Attack and +1 Defense.",
                Quantity = 3,
                Number = 32
            });
            Cards.Add(new Card() {
                ImageName = "M1033",
                Title = "Rain of Arrows",
                Id = "51223bd0-ffd1-11df-a976-0801200c9033",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Text = "Action: Exhaust a character you control with the ranged keyword to choose a player. Deal 1 damage to each enemy engaged with that player.",
                Quantity = 2,
                Number = 33
            });
            Cards.Add(new Card() {
                ImageName = "M1034",
                Title = "Feint",
                Id = "51223bd0-ffd1-11df-a976-0801200c9034",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Text = "Combat Action: Choose an enemy engaged with a player. That enemy cannot attack that player this phase.",
                Quantity = 2,
                Number = 34
            });
            Cards.Add(new Card() {
                ImageName = "M1035",
                Title = "Quick Strike",
                Id = "51223bd0-ffd1-11df-a976-0801200c9035",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Text = "Action: Exhaust a character you control to immediately declare it as an attacker (and resolve its attack) against any eligible enemy target.",
                Quantity = 2,
                Number = 35
            });
            Cards.Add(new Card() {
                ImageName = "M1036",
                Title = "Thicket of Spears",
                Id = "51223bd0-ffd1-11df-a976-0801200c9036",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 3,
                Text = "You must use resources from 3 different heroes' pools to pay for this card.Action: Choose a player. That player's engaged enemies cannot attack that player this phase.",
                Quantity = 2,
                Number = 36
            });
            Cards.Add(new Card() {
                ImageName = "M1037",
                Title = "Swift Strike",
                Id = "51223bd0-ffd1-11df-a976-0801200c9037",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Text = "Response: After a character is declared as a defender, deal 2 damage to the attacking enemy.",
                Quantity = 1,
                Number = 37
            });
            Cards.Add(new Card() {
                ImageName = "M1038",
                Title = "Stand Together",
                Id = "51223bd0-ffd1-11df-a976-0801200c9038",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Text = "Action: Choose a player. That player may declare any number of his eligible characters as defenders against each enemy attacking him this phase.",
                Quantity = 1,
                Number = 38
            });
            Cards.Add(new Card() {
                ImageName = "M1039",
                Title = "Blade of Gondolin",
                Id = "51223bd0-ffd1-11df-a976-0801200c9039",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Traits = new List<string>() { "Item.", " Weapon." },
                Text = "Attached hero gets +1 Attack when attacking an Orc.\r\nResponse: After attached hero attacks and destroys an enemy, place 1 progress token on the current quest.",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                Quantity = 2,
                Number = 39
            });
            Cards.Add(new Card() {
                ImageName = "M1040",
                Title = "Citadel Plate",
                Id = "51223bd0-ffd1-11df-a976-0801200c9040",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 4,
                Traits = new List<string>() { "Item.", " Armor." },
                Text = "Attached hero gets +4 Hit Points.",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                Quantity = 2,
                Number = 40
            });
            Cards.Add(new Card() {
                ImageName = "M1041",
                Title = "Dwarven Axe",
                Id = "51223bd0-ffd1-11df-a976-0801200c9041",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Traits = new List<string>() { "Item.", " Weapon." },
                Text = "Attached hero gains +1 Attack. (+2 Attack instead if attached hero is a Dwarf.)",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                Quantity = 2,
                Number = 41
            });
            Cards.Add(new Card() {
                ImageName = "M1042",
                Title = "Horn of Gondor",
                Id = "51223bd0-ffd1-11df-a976-0801200c9042",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                IsUnique = true,
                Traits = new List<string>() { "Item.", " Artifact." },
                Text = "Response: After a character leaves play, add 1 resource to attached hero's pool.",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                Quantity = 1,
                Number = 42
            });
            Cards.Add(new Card() {
                ImageName = "M1043",
                Title = "Wandering Took",
                Id = "51223bd0-ffd1-11df-a976-0801200c9043",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Text = "Action: Reduce your threat by 3 to give control of Wandering Took to another player. Raise that player's threat by 3.",
                Quantity = 2,
                Number = 43
            });
            Cards.Add(new Card() {
                ImageName = "M1044",
                Title = "Lórien Guide",
                NormalizedTitle = "Lorien Guide",
                Id = "51223bd0-ffd1-11df-a976-0801200c9044",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Attack = 1,
                Defense = 0,
                Willpower = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Silvan.", " Scout." },
                Text = "Response: After Lórien Guide commits to a quest, place 1 progress token on the active location.",
                Quantity = 3,
                Number = 44
            });
            Cards.Add(new Card() {
                ImageName = "M1045",
                Title = "Northern Tracker",
                Id = "51223bd0-ffd1-11df-a976-0801200c9045",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 4,
                Attack = 2,
                Defense = 2,
                Willpower = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Dúnedain.", " Ranger." },
                NormalizedTraits = new List<string> { "Dunedain." },
                Text = "Response: After Northern Tracker commits to a quest, place 1 progress token on each location in the staging area.",
                Quantity = 2,
                Number = 45
            });
            Cards.Add(new Card() {
                ImageName = "M1046",
                Title = "The Galadhrim's Greeting",
                Id = "51223bd0-ffd1-11df-a976-0801200c9046",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Text = "Action: Reduce one player's threat by 6, or reduce each player's threat by 2.",
                Quantity = 2,
                Number = 46
            });
            Cards.Add(new Card() {
                ImageName = "M1047",
                Title = "Strength of Will",
                Id = "51223bd0-ffd1-11df-a976-0801200c9047",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Text = "Response: After you travel to a location, exhaust a Spirit character to place 2 progress tokens on that location.",
                Quantity = 2,
                Number = 47
            });
            Cards.Add(new Card() {
                ImageName = "M1048",
                Title = "Hasty Stroke",
                Id = "51223bd0-ffd1-11df-a976-0801200c9048",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Text = "Response: Cancel a shadow effect just triggered during combat.",
                Quantity = 2,
                Number = 48
            });
            Cards.Add(new Card() {
                ImageName = "M1049",
                Title = "Will of the West",
                Id = "51223bd0-ffd1-11df-a976-0801200c9049",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Text = "Action: Choose a player. Shuffle that player's discard pile back into his deck.",
                Quantity = 2,
                Number = 49
            });
            Cards.Add(new Card() {
                ImageName = "M1050",
                Title = "A Test of Will",
                Id = "51223bd0-ffd1-11df-a976-0801200c9050",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Text = "Response: Cancel the 'when revealed' effects of a card that was just revealed from the encounter deck.",
                Quantity = 2,
                Number = 50
            });
            Cards.Add(new Card() {
                ImageName = "M1051",
                Title = "Stand and Fight",
                Id = "51223bd0-ffd1-11df-a976-0801200c9051",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                IsVariableCost = true,
                Text = "Action: Choose an ally with a printed cost of X in any player's discard pile. Put that ally into play under your control. (The chosen ally can belong to any sphere of influence.)",
                Quantity = 3,
                Number = 51
            });
            Cards.Add(new Card() {
                ImageName = "M1052",
                Title = "A Light in the Dark",
                Id = "51223bd0-ffd1-11df-a976-0801200c9052",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Text = "Action: Choose an enemy engaged with a player. Return that enemy to the staging area.",
                Quantity = 2,
                Number = 52
            });
            Cards.Add(new Card() {
                ImageName = "M1053",
                Title = "Dwarven Tomb",
                Id = "51223bd0-ffd1-11df-a976-0801200c9053",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Text = "Action: Return 1 Spirit card from your discard pile to your hand.",
                Quantity = 1,
                Number = 53
            });
            Cards.Add(new Card() {
                ImageName = "M1054",
                Title = "Fortune or Fate",
                Id = "51223bd0-ffd1-11df-a976-0801200c9054",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 5,
                Text = "Action: Choose a hero in any player's discard pile. Put that card into play, under its owner's control.",
                Quantity = 1,
                Number = 54
            });
            Cards.Add(new Card() {
                ImageName = "M1055",
                Title = "The Favor of the Lady",
                Id = "51223bd0-ffd1-11df-a976-0801200c9055",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Traits = new List<string>() { "Condition." },
                Text = "Attached hero gains +1 Willpower.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 2,
                Number = 55
            });
            Cards.Add(new Card() {
                ImageName = "M1056",
                Title = "Power in the Earth",
                Id = "51223bd0-ffd1-11df-a976-0801200c9056",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Traits = new List<string>() { "Condition." },
                Text = "Attached location gets -1 Threat.",
                Keywords = new List<string>() { "Attach to a location." },
                Quantity = 2,
                Number = 56
            });
            Cards.Add(new Card() {
                ImageName = "M1057",
                Title = "Unexpected Courage",
                Id = "51223bd0-ffd1-11df-a976-0801200c9057",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Traits = new List<string>() { "Condition." },
                Text = "Action: Exhaust Unexpected Courage to ready attached hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 1,
                Number = 57
            });
            Cards.Add(new Card() {
                ImageName = "M1058",
                Title = "Daughter of the Nimrodel",
                Id = "51223bd0-ffd1-11df-a976-0801200c9058",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Attack = 0,
                Defense = 0,
                Willpower = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Silvan." },
                Text = "Action: Exhaust Daughter of the Nimrodel to heal up to 2 damage on any 1 hero.",
                Quantity = 3,
                Number = 58
            });
            Cards.Add(new Card() {
                ImageName = "M1059",
                Title = "Erebor Hammersmith",
                Id = "51223bd0-ffd1-11df-a976-0801200c9059",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Dwarf.", " Craftsman." },
                Text = "Response: After you play Erebor Hammersmith, return the topmost attachment in any player's discard pile to his hand.",
                Quantity = 2,
                Number = 59
            });
            Cards.Add(new Card() {
                ImageName = "M1060",
                Title = "Henamarth Riversong",
                Id = "51223bd0-ffd1-11df-a976-0801200c9060",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                IsUnique = true,
                Attack = 1,
                Defense = 0,
                Willpower = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Silvan." },
                Text = "Action: Exhaust Henamarth Riversong to look at the top card of the encounter deck.",
                Quantity = 1,
                Number = 60
            });
            Cards.Add(new Card() {
                ImageName = "M1061",
                Title = "Miner of the Iron Hills",
                Id = "51223bd0-ffd1-11df-a976-0801200c9061",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Attack = 1,
                Defense = 1,
                Willpower = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Dwarf." },
                Text = "Response: After Miner of the Iron Hills enters play, choose and discard 1 Condition attachment from play.",
                Quantity = 2,
                Number = 61
            });
            Cards.Add(new Card() {
                ImageName = "M1062",
                Title = "Gléowine",
                NormalizedTitle = "Gleowine",
                Id = "51223bd0-ffd1-11df-a976-0801200c9062",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                IsUnique = true,
                Attack = 0,
                Defense = 0,
                Willpower = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Minstrel.", " Rohan." },
                Text = "Action: Exhaust Gléowine to choose a player. That player draws 1 card.",
                Quantity = 2,
                Number = 62
            });
            Cards.Add(new Card() {
                ImageName = "M1063",
                Title = "Lore of Imladris",
                Id = "51223bd0-ffd1-11df-a976-0801200c9063",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Text = "Action: Choose a character. Heal all damage from that character.",
                Quantity = 3,
                Number = 63
            });
            Cards.Add(new Card() {
                ImageName = "M1064",
                Title = "Lórien's Wealth",
                NormalizedTitle = "Lorien's Wealth",
                Id = "51223bd0-ffd1-11df-a976-0801200c9064",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Text = "Action: Choose a player. That player draws 3 cards.",
                Quantity = 2,
                Number = 64
            });
            Cards.Add(new Card() {
                ImageName = "M1065",
                Title = "Radagast's Cunning",
                Id = "51223bd0-ffd1-11df-a976-0801200c9065",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Text = "Quest Action: Choose an enemy in the staging area. Until the end of the phase, that enemy does not contribute its Threat.",
                Quantity = 2,
                Number = 65
            });
            Cards.Add(new Card() {
                ImageName = "M1066",
                Title = "Secret Paths",
                Id = "51223bd0-ffd1-11df-a976-0801200c9066",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Text = "Quest Action: Choose a location in the staging area. Until the end of the phase, that location does not contribute its Threat.",
                Quantity = 2,
                Number = 66
            });
            Cards.Add(new Card() {
                ImageName = "M1067",
                Title = "Gandalf's Search",
                Id = "51223bd0-ffd1-11df-a976-0801200c9067",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                IsVariableCost = true,
                Text = "Action: Look at the top X cards of any player's deck, add 1 of those cards to its owner's hand, and return the rest to the top of the deck in any order.",
                Quantity = 2,
                Number = 67
            });
            Cards.Add(new Card() {
                ImageName = "M1068",
                Title = "Beorn's Hospitality",
                Id = "51223bd0-ffd1-11df-a976-0801200c9068",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 5,
                Text = "Action: Choose a player. Heal all damage on each hero controlled by that player.",
                Quantity = 1,
                Number = 68
            });
            Cards.Add(new Card() {
                ImageName = "M1069",
                Title = "Forest Snare",
                Id = "51223bd0-ffd1-11df-a976-0801200c9069",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Traits = new List<string>() { "Item.", " Trap." },
                Text = "Attached enemy cannot attack.",
                Keywords = new List<string>() { "Attach to an enemy engaged with a player." },
                Quantity = 2,
                Number = 69
            });
            Cards.Add(new Card() {
                ImageName = "M1070",
                Title = "Protector of Lórien",
                NormalizedTitle = "Protector of Lorien",
                Id = "51223bd0-ffd1-11df-a976-0801200c9070",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string>() { "Title." },
                Text = "Action: Discard a card from your hand to give attached hero +1 Defense or +1 Willpower until the end of the phase. Limit 3 times per phase.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 2,
                Number = 70
            });
            Cards.Add(new Card() {
                ImageName = "M1071",
                Title = "Dark Knowledge",
                Id = "51223bd0-ffd1-11df-a976-0801200c9071",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string>() { "Condition." },
                Text = "Response: Exhaust Dark Knowledge to look at 1 shadow card that was just dealt to an enemy attacking you.",
                Keywords = new List<string>() { "Attach to a hero.", " Attached hero gets -1 Willpower." },
                Quantity = 1,
                Number = 71
            });
            Cards.Add(new Card() {
                ImageName = "M1072",
                Title = "Self Preservation",
                Id = "51223bd0-ffd1-11df-a976-0801200c9072",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Traits = new List<string>() { "Skill." },
                Text = "Action: Exhaust Self Preservation to heal 2 points of damage from attached character.",
                Keywords = new List<string>() { "Attach to a character." },
                Quantity = 2,
                Number = 72
            });
            Cards.Add(new Card() {
                ImageName = "M1073",
                Title = "Gandalf",
                Id = "51223bd0-ffd1-11df-a976-0801200c9073",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                ResourceCost = 5,
                IsUnique = true,
                Attack = 4,
                Defense = 4,
                Willpower = 4,
                HitPoints = 4,
                Traits = new List<string>() { "Istari." },
                Text = "At the end of the round, discard Gandalf from play.\r\nResponse: After Gandalf enters play, (choose 1): draw 3 cards, deal 4 damage to 1 enemy in play, or reduce your threat by 5.",
                Quantity = 4,
                Number = 73
            });
            Cards.Add(new Card() {
                ImageName = "M1074",
                Title = "King Spider",
                Id = "51223bd0-ffd1-11df-a976-0801200c9074",
                CardType = CardType.Enemy,
                EngagementCost = 20,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Creature.", " Spider." },
                Text = "When Revealed: Each player must choose and exhaust 1 character he controls.",
                Shadow = "Shadow: Defending player must choose and exhaust 1 character he controls. (2 characters instead if this attack is undefended.)",
                Threat = 2,
                EncounterSet = "Spiders of Mirkwood",
                Quantity = 2,
                Number = 74
            });
            Cards.Add(new Card() {
                ImageName = "M1075",
                Title = "Hummerhorns",
                Id = "51223bd0-ffd1-11df-a976-0801200c9075",
                CardType = CardType.Enemy,
                EngagementCost = 40,
                Attack = 2,
                Defense = 0,
                HitPoints = 3,
                Traits = new List<string>() { "Creature.", " Insect." },
                Text = "Forced: After Hummerhorns engages you, deal 5 damage to a single hero you control.",
                Shadow = "Shadow: Deal 1 damage to each character the defending player controls. (2 damage instead if this attack is undefended.)",
                Threat = 1,
                EncounterSet = "Spiders of Mirkwood",
                Quantity = 1,
                VictoryPoints = 5,
                Number = 75
            });
            Cards.Add(new Card() {
                ImageName = "M1076",
                Title = "Ungoliant's Spawn",
                Id = "51223bd0-ffd1-11df-a976-0801200c9076",
                CardType = CardType.Enemy,
                EngagementCost = 32,
                Attack = 5,
                Defense = 2,
                HitPoints = 9,
                Traits = new List<string>() { "Creature.", " Spider." },
                Text = "When Revealed: Each character currently committed to a quest gets -1 Willpower until the end of the phase.",
                Shadow = "Shadow: Raise defending player's threat by 4. (Raise defending player's threat by 8 instead if this attack is undefended.)",
                Threat = 3,
                EncounterSet = "Spiders of Mirkwood",
                Quantity = 1,
                Number = 76
            });
            Cards.Add(new Card() {
                ImageName = "M1077",
                Title = "Great Forest Web",
                Id = "51223bd0-ffd1-11df-a976-0801200c9077",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "Travel: Each player must exhaust 1 hero he controls to travel here.",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "Spiders of Mirkwood",
                Quantity = 2,
                Number = 77
            });
            Cards.Add(new Card() {
                ImageName = "M1078",
                Title = "Mountains of Mirkwood",
                Id = "51223bd0-ffd1-11df-a976-0801200c9078",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest.", " Mountain." },
                Text = "Travel: Reveal the top card of the encounter deck and add it to the staging area to travel here.Response: After Mountains of Mirkwood leaves play as an explored location, each player may search the top 5 cards of his deck for 1 card and add it to his hand. Shuffle the rest of the searched cards back into their owners' decks.",
                Threat = 2,
                QuestPoints = 3,
                EncounterSet = "Spiders of Mirkwood",
                Quantity = 3,
                Number = 78
            });
            Cards.Add(new Card() {
                ImageName = "M1079",
                Title = "Eyes of the Forest",
                Id = "51223bd0-ffd1-11df-a976-0801200c9079",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player discards all event cards in his hand.",
                EncounterSet = "Spiders of Mirkwood",
                Quantity = 1,
                Number = 79
            });
            Cards.Add(new Card() {
                ImageName = "M1080",
                Title = "Caught in a Web",
                Id = "51223bd0-ffd1-11df-a976-0801200c9080",
                CardType = CardType.Treachery,
                Text = "When Revealed: The player with the highest threat level attaches this card to one of his heroes. (Counts as a Condition attachment with the text: 'Attached hero does not ready during the refresh phase unless you pay 2 resources from that hero's pool.')",
                EncounterSet = "Spiders of Mirkwood",
                Quantity = 2,
                Number = 80
            });
            Cards.Add(new Card() {
                ImageName = "M1081",
                Title = "Wolf Rider",
                Id = "51223bd0-ffd1-11df-a976-0801200c9081",
                CardType = CardType.Enemy,
                EngagementCost = 10,
                Attack = 2,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Goblin.", " Orc." },
                Text = " ",
                Shadow = "Shadow: Wolf Rider attacks the defending player. That player may declare 1 character as a defender. Deal Wolf Rider its own Shadow card. After combat, return Wolf Rider to the top of the encounter deck.",
                Keywords = new List<string>() { "Surge." },
                Threat = 1,
                EncounterSet = "Wilderlands",
                Quantity = 1,
                Number = 81
            });
            Cards.Add(new Card() {
                ImageName = "M1082",
                Title = "Hill Troll",
                Id = "51223bd0-ffd1-11df-a976-0801200c9082",
                CardType = CardType.Enemy,
                EngagementCost = 30,
                Attack = 6,
                Defense = 3,
                HitPoints = 9,
                Traits = new List<string>() { "Troll." },
                Text = "Excess combat damage dealt by Hill Troll (damage that is dealt beyond the remaining hit points of the character damaged by its attack) must be assigned as an increase to your threat.",
                Threat = 1,
                EncounterSet = "Wilderlands",
                Quantity = 2,
                VictoryPoints = 4,
                Number = 82
            });
            Cards.Add(new Card() {
                ImageName = "M1083",
                Title = "Goblin Sniper",
                Id = "51223bd0-ffd1-11df-a976-0801200c9083",
                CardType = CardType.Enemy,
                EngagementCost = 48,
                Attack = 2,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Goblin.", " Orc." },
                Text = "During the encounter phase, players cannot optionally engage Goblin Sniper if there are other enemies in the staging area.Forced: If Goblin Sniper is in the staging area at the end of the combat phase, each player deals 1 point of damage to 1 character he controls.",
                Threat = 2,
                EncounterSet = "Wilderlands",
                Quantity = 2,
                Number = 83
            });
            Cards.Add(new Card() {
                ImageName = "M1084",
                Title = "Marsh Adder",
                Id = "51223bd0-ffd1-11df-a976-0801200c9084",
                CardType = CardType.Enemy,
                EngagementCost = 40,
                Attack = 4,
                Defense = 1,
                HitPoints = 7,
                Traits = new List<string>() { "Creature." },
                Text = "Forced: Each time Marsh Adder attacks you, raise your threat by 1.",
                Threat = 3,
                EncounterSet = "Wilderlands",
                Quantity = 1,
                VictoryPoints = 3,
                Number = 84
            });
            Cards.Add(new Card() {
                ImageName = "M1085",
                Title = "Wargs",
                Id = "51223bd0-ffd1-11df-a976-0801200c9085",
                CardType = CardType.Enemy,
                EngagementCost = 20,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Creature." },
                Text = "Forced: If Wargs is dealt a shadow card with no effect, return Wargs to the staging area after it attacks.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if this attack is undefended.)",
                Threat = 2,
                EncounterSet = "Wilderlands",
                Quantity = 2,
                Number = 85
            });
            Cards.Add(new Card() {
                ImageName = "M1086",
                Title = "Despair",
                Id = "51223bd0-ffd1-11df-a976-0801200c9086",
                CardType = CardType.Treachery,
                Text = "When Revealed: Remove 4 progress tokens from the current quest card. (If there are fewer than 4 progress tokens on the quest, remove all progress tokens from that quest.)",
                Shadow = "Shadow: Defending character does not count its Defense.",
                EncounterSet = "Wilderlands",
                Quantity = 2,
                Number = 86
            });
            Cards.Add(new Card() {
                ImageName = "M1087",
                Title = "The Brown Lands",
                Id = "51223bd0-ffd1-11df-a976-0801200c9087",
                CardType = CardType.Location,
                Traits = new List<string>() { "Wasteland." },
                Text = "Forced: After the players travel to The Brown Lands, place 1 progress token on it.",
                Threat = 5,
                QuestPoints = 1,
                EncounterSet = "Wilderlands",
                Quantity = 2,
                Number = 87
            });
            Cards.Add(new Card() {
                ImageName = "M1088",
                Title = "The East Bight",
                Id = "51223bd0-ffd1-11df-a976-0801200c9088",
                CardType = CardType.Location,
                Traits = new List<string>() { "Wasteland." },
                Text = "When faced with the option to travel, the players must travel to The East Bight if there is no active location.",
                Threat = 1,
                QuestPoints = 6,
                EncounterSet = "Wilderlands",
                Quantity = 2,
                Number = 88
            });
            Cards.Add(new Card() {
                ImageName = "M1089",
                Title = "Dol Guldur Orcs",
                Id = "51223bd0-ffd1-11df-a976-0801200c9089",
                CardType = CardType.Enemy,
                EngagementCost = 10,
                Attack = 2,
                Defense = 0,
                HitPoints = 3,
                Traits = new List<string>() { "Dol Guldur.", " Orc." },
                Text = "When Revealed: The first player chooses 1 character currently committed to a quest. Deal 2 damage to that character.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+3 Attack instead if this attack is undefended.)",
                Threat = 2,
                EncounterSet = "Dol Guldur Orcs",
                Quantity = 3,
                Number = 89
            });
            Cards.Add(new Card() {
                ImageName = "M1090",
                Title = "Chieftan Ufthak",
                Id = "51223bd0-ffd1-11df-a976-0801200c9090",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Attack = 3,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Dol Guldur.", " Orc." },
                Text = "Chieftain Ufthak get +2 Attack for each resource token on him.\r\nForced: After Chieftain Ufthak attacks, place 1 resource token on him.",
                Threat = 2,
                EncounterSet = "Dol Guldur Orcs",
                Quantity = 1,
                VictoryPoints = 4,
                Number = 90
            });
            Cards.Add(new Card() {
                ImageName = "M1091",
                Title = "Dol Guldur Beastmaster",
                Id = "51223bd0-ffd1-11df-a976-0801200c9091",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Attack = 3,
                Defense = 1,
                HitPoints = 5,
                Traits = new List<string>() { "Dol Guldur.", " Orc." },
                Text = "Forced: When Dol Guldur Beastmaster attacks, deal it 1 additional shadow card.",
                Threat = 2,
                EncounterSet = "Dol Guldur Orcs",
                Quantity = 2,
                Number = 91
            });
            Cards.Add(new Card() {
                ImageName = "M1092",
                Title = "Driven by Shadow",
                Id = "51223bd0-ffd1-11df-a976-0801200c9092",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each enemy and each location currently in the staging area gets +1 Threat until the end of the phase. If there are no cards in the staging area, Driven by Shadow gains surge.",
                Shadow = "Shadow: Choose and discard 1 attachment from the defending character. (If this attack is undefended, discard all attachments you control.)",
                EncounterSet = "Dol Guldur Orcs",
                Quantity = 1,
                Number = 92
            });
            Cards.Add(new Card() {
                ImageName = "M1093",
                Title = "The Necromancer's Reach",
                Id = "51223bd0-ffd1-11df-a976-0801200c9093",
                CardType = CardType.Treachery,
                Text = "When Revealed: Deal 1 damage to each exhausted character.",
                FlavorText = "There was an eye in the Dark Tower that did not sleep. He knew that it had become aware of his gaze. A fierce eager will was there. - The Fellowship of the Ring",
                EncounterSet = "Dol Guldur Orcs",
                Quantity = 3,
                Number = 93
            });
            Cards.Add(new Card() {
                ImageName = "M1094",
                Title = "Necromancer's Pass",
                Id = "51223bd0-ffd1-11df-a976-0801200c9094",
                CardType = CardType.Location,
                Traits = new List<string>() { "Stronghold.", " Dol Guldur." },
                Text = "Travel: The first player must discard 2 cards from his hand at random to travel here.",
                Threat = 3,
                QuestPoints = 2,
                EncounterSet = "Dol Guldur Orcs",
                Quantity = 2,
                Number = 94
            });
            Cards.Add(new Card() {
                ImageName = "M1095",
                Title = "Enchanted Stream",
                Id = "51223bd0-ffd1-11df-a976-0801200c9095",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "While Enchanted Stream is the active location, players cannot draw cards.",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "Dol Guldur Orcs",
                Quantity = 2,
                Number = 95
            });
            Cards.Add(new Card() {
                ImageName = "M1096",
                Title = "Forest Spider",
                Id = "51223bd0-ffd1-11df-a976-0801200c9096",
                CardType = CardType.Enemy,
                EngagementCost = 25,
                Attack = 2,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Creature.", " Spider." },
                Text = "Forced: After Forest Spider engages a player, it gets +1 Attack until the end of the round.",
                Shadow = "Shadow: Defending player must choose and discard 1 attachment he controls.",
                Threat = 2,
                EncounterSet = "Passage Through Mirkwood",
                Quantity = 4,
                Number = 96
            });
            Cards.Add(new Card() {
                ImageName = "M1097",
                Title = "East Bight Patrol",
                Id = "51223bd0-ffd1-11df-a976-0801200c9097",
                CardType = CardType.Enemy,
                EngagementCost = 5,
                Attack = 3,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Goblin.", " Orc." },
                Text = " ",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (If this attack is undefended, also raise your threat by 3.)",
                Threat = 3,
                EncounterSet = "Passage Through Mirkwood",
                Quantity = 1,
                Number = 97
            });
            Cards.Add(new Card() {
                ImageName = "M1098",
                Title = "Black Forest Bats",
                Id = "51223bd0-ffd1-11df-a976-0801200c9098",
                CardType = CardType.Enemy,
                EngagementCost = 15,
                Attack = 1,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Creature." },
                Text = "When Revealed: Each player must choose 1 character currently committed to a quest, and remove that character from the quest. (The chosen character does not ready.)",
                Threat = 1,
                EncounterSet = "Passage Through Mirkwood",
                Quantity = 1,
                Number = 98
            });
            Cards.Add(new Card() {
                ImageName = "M1099",
                Title = "Old Forest Road",
                Id = "51223bd0-ffd1-11df-a976-0801200c9099",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "Response: After you travel to Old Forest Road, the first player may choose and ready 1 character he controls.",
                Threat = 1,
                QuestPoints = 3,
                EncounterSet = "Passage Through Mirkwood",
                Quantity = 2,
                Number = 99
            });
            Cards.Add(new Card() {
                ImageName = "M1100",
                Title = "Forest Gate",
                Id = "51223bd0-ffd1-11df-a976-0801200c9100",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "Response: After you travel to Forest Gate, the first player may draw 2 cards.",
                Threat = 2,
                QuestPoints = 4,
                EncounterSet = "Passage Through Mirkwood",
                Quantity = 2,
                Number = 100
            });
            Cards.Add(new Card() {
                ImageName = "M1101",
                Title = "Dungeon Jailor",
                Id = "51223bd0-ffd1-11df-a976-0801200c9101",
                CardType = CardType.Enemy,
                EngagementCost = 38,
                Attack = 2,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Dol Guldur.", " Orc." },
                Text = "Forced: If Dungeon Jailor is in the staging area after the players have just quested unsuccessfully, shuffle 1 unclaimed objective card from the staging area back into the encounter deck.",
                Threat = 1,
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 2,
                VictoryPoints = 5,
                Number = 101
            });
            Cards.Add(new Card() {
                ImageName = "M1102",
                Title = "Nazgûl of Dol Guldur",
                NormalizedTitle = "Nazgul of Dol Guldur",
                Id = "51223bd0-ffd1-11df-a976-0801200c9102",
                CardType = CardType.Enemy,
                EngagementCost = 40,
                Attack = 4,
                Defense = 3,
                HitPoints = 9,
                Traits = new List<string>() { "Nazgûl." },
                NormalizedTraits = new List<string> { "Nazgul." },
                Text = "Forced: When the prisoner is 'rescued', move Nazgul of Dol Guldur into the staging area.\r\nForced: After a shadow effect dealt to Nazgul of Dol Guldur resolves, the engaged player must choose and discard 1 character he controls.",
                Keywords = new List<string>() { "No attachments." },
                Threat = 5,
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 1,
                Number = 102
            });
            Cards.Add(new Card() {
                ImageName = "M1103",
                Title = "Cavern Guardian",
                Id = "51223bd0-ffd1-11df-a976-0801200c9103",
                CardType = CardType.Enemy,
                EngagementCost = 8,
                Attack = 2,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Undead." },
                Text = " ",
                Shadow = "Shadow: Choose and discard 1 attachment you control. Discarded  objective cards are returned to the staging area. (If this attack is  undefended, discard all attachments you control.)",
                Keywords = new List<string>() { "Doomed 1." },
                Threat = 2,
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 2,
                Number = 103
            });
            Cards.Add(new Card() {
                ImageName = "M1104",
                Title = "Under the Shadow",
                Id = "51223bd0-ffd1-11df-a976-0801200c9104",
                CardType = CardType.Treachery,
                Text = "When Revealed: Until the end of the phase, raise the total Threat in the  staging area by X, where X is the number of players in the game.",
                Shadow = "Shadow: Defending player raises his threat by the number of enemies  with which he is engaged.",
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 2,
                Number = 104
            });
            Cards.Add(new Card() {
                ImageName = "M1105",
                Title = "Iron Shackles",
                Id = "51223bd0-ffd1-11df-a976-0801200c9105",
                CardType = CardType.Treachery,
                Text = "When Revealed: Attach Iron Shackles to the top of the first player's deck. (Counts as a Condition attachment with the text: 'The next time a player would draw 1 or more cards from attached deck, discard Iron Shackles instead.')",
                Shadow = "Shadow: Resolve the 'When Revealed' effect of Iron Shackles.",
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 1,
                Number = 105
            });
            Cards.Add(new Card() {
                ImageName = "M1106",
                Title = "Endless Caverns",
                Id = "51223bd0-ffd1-11df-a976-0801200c9106",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dungeon." },
                Keywords = new List<string>() { "Doomed 1.", " Surge." },
                Threat = 1,
                QuestPoints = 3,
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 2,
                Number = 106
            });
            Cards.Add(new Card() {
                ImageName = "M1107",
                Title = "Tower Gate",
                Id = "51223bd0-ffd1-11df-a976-0801200c9107",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dungeon." },
                Text = "Forced: After travelling to Tower Gate, each player places the top card of his deck, face down in front of him, as if it just engaged him from the staging area. These cards are called 'Orc Guard', and act as enemies with: 1 hit point, 1 Attack, and 1 Defense.",
                Threat = 2,
                QuestPoints = 1,
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 2,
                Number = 107
            });
            Cards.Add(new Card() {
                ImageName = "M1108",
                Title = "Gandalf's Map",
                Id = "51223bd0-ffd1-11df-a976-0801200c9108",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Item." },
                Text = "Action: Raise your threat by 2 to claim this objective when it is free of encounters. When claimed, attach Gandalf's Map to a hero you control. (Counts as an attachment. If detached, return Gandalf's Map to the staging area.) Attached hero cannot attack or defend.",
                Keywords = new List<string>() { "Guarded.", " Restricted." },
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 1,
                Number = 108
            });
            Cards.Add(new Card() {
                ImageName = "M1109",
                Title = "Dungeon Torch",
                Id = "51223bd0-ffd1-11df-a976-0801200c9109",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Item." },
                Text = "Action: Raise your threat by 2 to claim this objective when it is free of encounters. When claimed, attach Dungeon Torch to a hero you control. (Counts as an attachment. If detached, return Dungeon Torch to the staging area.)Forced: At the end of each round, raise attached hero's controller's threat by 2.",
                Keywords = new List<string>() { "Guarded.", " Restricted." },
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 1,
                Number = 109
            });
            Cards.Add(new Card() {
                ImageName = "M1110",
                Title = "Shadow Key",
                Id = "51223bd0-ffd1-11df-a976-0801200c9110",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Item." },
                Text = "Action: Raise your threat by 2 to claim this objective when it is free of encounters. When claimed, attach Shadow Key to a hero you control. (Counts as an attachment. If detached, return Shadow Key to the staging area.)Forced: At the end of each round, attached hero suffers 1 damage.",
                Keywords = new List<string>() { "Guarded.", " Restricted." },
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 1,
                Number = 110
            });
            Cards.Add(new Card() {
                ImageName = "M1111",
                Title = "Misty Mountain Goblins",
                Id = "51223bd0-ffd1-11df-a976-0801200c9111",
                CardType = CardType.Enemy,
                EngagementCost = 15,
                Attack = 2,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Goblin.", " Orc." },
                Text = "Forced: After Misty Mountain Goblins attacks, remove 1 progress token from the current quest.",
                Shadow = "Shadow: Remove 1 progress token from the current quest. (3 progress tokens instead if this attack is undefended.)",
                Threat = 2,
                EncounterSet = "Journey Down the Anduin",
                Quantity = 3,
                Number = 111
            });
            Cards.Add(new Card() {
                ImageName = "M1112",
                Title = "Massing at Night",
                Id = "51223bd0-ffd1-11df-a976-0801200c9112",
                CardType = CardType.Treachery,
                Text = "When Revealed: Reveal X additional cards from the encounter deck. X is the number of players in the game.",
                Shadow = "Shadow: Deal X shadow cards to this attacker. X is the number of players in the game.",
                EncounterSet = "Journey Down the Anduin",
                Quantity = 1,
                Number = 112
            });
            Cards.Add(new Card() {
                ImageName = "M1113",
                Title = "Banks of the Anduin",
                Id = "51223bd0-ffd1-11df-a976-0801200c9113",
                CardType = CardType.Location,
                Traits = new List<string>() { "Riverland." },
                Text = "Forced: If Banks of the Anduin leaves play, return it to the top of the encounter deck instead of placing it in the discard pile.",
                Threat = 1,
                QuestPoints = 3,
                EncounterSet = "Journey Down the Anduin",
                Quantity = 2,
                Number = 113
            });
            Cards.Add(new Card() {
                ImageName = "M1114",
                Title = "Gladden Fields",
                Id = "51223bd0-ffd1-11df-a976-0801200c9114",
                CardType = CardType.Location,
                Traits = new List<string>() { "Marshland." },
                Text = "Forced: While Gladden Fields is the active location, each player must raise his threat by an additional point during the refresh phase.",
                Threat = 3,
                QuestPoints = 3,
                EncounterSet = "Journey Down the Anduin",
                Quantity = 3,
                VictoryPoints = 3,
                Number = 114
            });
            Cards.Add(new Card() {
                ImageName = "M1115",
                Title = "Eastern Crows",
                Id = "51223bd0-ffd1-11df-a976-0801200c9115",
                CardType = CardType.Enemy,
                EngagementCost = 30,
                Attack = 1,
                Defense = 0,
                HitPoints = 1,
                Traits = new List<string>() { "Creature." },
                Text = "Forced: After Eastern Crows is defeated, shuffle it back into the encounter deck.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if defending player's threat is 35 or higher.)",
                Keywords = new List<string>() { "Surge." },
                Threat = 1,
                EncounterSet = "Sauron's Reach",
                Quantity = 3,
                Number = 115
            });
            Cards.Add(new Card() {
                ImageName = "M1116",
                Title = "Evil Storm",
                Id = "51223bd0-ffd1-11df-a976-0801200c9116",
                CardType = CardType.Treachery,
                Text = "When Revealed: Deal 1 damage to each character controlled by each player with a threat of 35 or higher.",
                EncounterSet = "Sauron's Reach",
                Quantity = 3,
                Number = 116
            });
            Cards.Add(new Card() {
                ImageName = "M1117",
                Title = "Pursued by Shadow",
                Id = "51223bd0-ffd1-11df-a976-0801200c9117",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player raises his threat by 1 for each character he controls that is not currently committed to a quest.",
                Shadow = "Shadow: Defending player chooses and returns 1 exhausted ally he controls to its owner's hand. If he controls no exhausted allies, raise his threat by 3.",
                EncounterSet = "Sauron's Reach",
                Quantity = 2,
                Number = 117
            });
            Cards.Add(new Card() {
                ImageName = "M1118",
                Title = "Treacherous Fog",
                Id = "51223bd0-ffd1-11df-a976-0801200c9118",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each location in the staging area gets +1 Threat until the end of the phase. Then, each player with a threat of 35 or higher chooses and discards 1 card from his hand.",
                EncounterSet = "Sauron's Reach",
                Quantity = 2,
                Number = 118
            });
            Cards.Add(new Card() {
                ImageName = "M1119",
                Title = "Flies and Spiders",
                StageNumber = 1,
                Id = "51223bd0-ffd1-11df-a976-0801200c9119",
                CardType = CardType.Quest,
                Setup = "ss",
                Text = "Setup: Search the encounter deck for 1 copy of the Forest Spider and 1 copy of the Old Forest Road, and add them to the staging area. Then, shuffle the encounter deck.",
                FlavorText = "You are traveling through Mirkwood forest, carrying an urgent message from King Thanduil to the Lady Galadriel of Lorien. As you move along the dark trail, the spiders gather around you...",
                OppositeFlavorText = "The nastiest things they saw were the cobwebs; dark dense cobwebs, with threads extradionairly thick, often stretched from tree to tree, or tangled in the lower branches on either side of them. There were none stretched across the path, but whether because some magic kept it clear, or for what other reasons they could not guess. - The Hobbit",
                EncounterSet = "Passage Through Mirkwood",
                Quantity = 1,
                QuestPoints = 8,
                Number = 119
            });
            Cards.Add(new Card() {
                ImageName = "M1120",
                Title = "A Fork in the Road",
                StageNumber = 2,
                Id = "51223bd0-ffd1-11df-a976-0801200c9121",
                CardType = CardType.Quest,
                EncounterSet = "Passage Through Mirkwood",
                Quantity = 1,
                FlavorText = "As you move through Mirkwood, hounded by spiders, the forest path forks before you...",
                OppositeText = "Forced: When you defeat this stage, proceed to one of the 2 \"A Chosen Path\" stages, at random.",
                OppositeFlavorText = "Unsure of what lies ahead but spurred by the urgency of your message, you choose a path and proceed... ",
                QuestPoints = 2,
                Number = 120
            });
            Cards.Add(new Card() {
                ImageName = "M1121",
                Title = "A Chosen Path",
                StageNumber = 3,
                SlugIncludesOppositeTitle = true,
                OppositeTitle = "Don't Leave the Path",
                Id = "51223bd0-ffd1-11df-a976-0801200c9123",
                CardType = CardType.Quest,
                EncounterSet = "Passage Through Mirkwood",
                OppositeText = "When Revealed: Each player must search the encounter deck and discard pile for 1 Spider card of his choice, and add it to the staging area.\r\nThe players must find and defeat Ungoliant's Spawn to win this game.",
                Quantity = 1,
                QuestPoints = 0,
                Number = 121
            });
            Cards.Add(new Card() {
                ImageName = "M1122",
                Title = "A Chosen Path",
                StageNumber = 3,
                SlugIncludesOppositeTitle = true,
                OppositeTitle = "Beorn's Path",
                Id = "51223bd0-ffd1-11df-a976-0801200c9125",
                CardType = CardType.Quest,
                EncounterSet = "Passage Through Mirkwood",
                OppositeText = "Players cannot defeat this stage while Ungoliant's Spawn is in play. If players defeat this stage, they have won the game.",
                Quantity = 1,
                QuestPoints = 10,
                Number = 122
            });
            Cards.Add(new Card() {
                ImageName = "M1123",
                Title = "The Necromancer's Tower",
                StageNumber = 1,
                Id = "51223bd0-ffd1-11df-a976-0801200c9127",
                CardType = CardType.Quest,
                Setup = "ssst",
                Text = "Setup: Search the encounter deck for the 3 objective cards, reveal and place them in the staging area. Also, place the Nazgul of Dol Guldur face up but out of play, alongside the quest deck. Then, shuffle the encounter deck, and attach 1 encounter to each objective card.",
                FlavorText = "The Lady Galadriel of Lorien has asked you to investigate the area in the vicinity of Dol Guldar. While doing so, one of your allies was ambushed by Orcs, captured, and is now held in a dungeon cell...",
                OppositeText = "When Revealed: Randomly select 1 hero card (among all the heroes controlled by the players) and turn it facedown. The hero is now considered a \"prisoner\", cannot be used, cannot be damaged, and does not collect resources, until it is \"rescued\" (as instructed by card effects) later in this quest.\r\nThe players, as a group, cannot play more than 1 ally card each round.\r\nPlayers cannot advance to the next stage of this quest unless they have at least 1 objective card.",
                EncounterSet = "Escape from Dol Guldur",
                Quantity = 1,
                QuestPoints = 9,
                Number = 123
            });
            Cards.Add(new Card() {
                ImageName = "M1124",
                Title = "Through the Caverns",
                StageNumber = 2,
                Id = "51223bd0-ffd1-11df-a976-0801200c9129",
                CardType = CardType.Quest,
                EncounterSet = "Escape from Dol Guldur",
                OppositeText = "Response: After placing any number of progress tokens on this card, flip the \"prisoner\" hero card face-up, and place 1 damage toke on it. The hero has been \"rescued\" and may now be used by its controller.\r\nThe players, as a group, cannot play more than 1 ally card each round.\r\nPlayers cannot advance to the next stage of the quest unless they have rescued the prisoner and have all 3 \"Escape from Dol Guldur\" objective cards.",
                Quantity = 1,
                QuestPoints = 15,
                Number = 124
            });
            Cards.Add(new Card() {
                ImageName = "M1125",
                Title = "Out of the Dungeons",
                StageNumber = 3,
                Id = "51223bd0-ffd1-11df-a976-0801200c9131",
                CardType = CardType.Quest,
                EncounterSet = "Escape from Dol Guldur",
                OppositeText = "Forced: At the beginning of each quest phase, each player places the top card of his deck, face down in front of him as if it just engaged him from the staging area. These cards are called \"Orc Guard\" and act as enemies with: 1 hit point, 1 Attack, and 1 Defense.\r\nPlayers cannot defeat this stage while Nazgul of Dol Guldur is in play. If this stage is defeated and Nazgul of Dol Guldur is not in play, the players have won the game.",
                Quantity = 1,
                QuestPoints = 7,
                Number = 125
            });
            Cards.Add(new Card() {
                ImageName = "M1126",
                Title = "To the River...",
                StageNumber = 1,
                Id = "51223bd0-ffd1-11df-a976-0801200c9133",
                CardType = CardType.Quest,
                Text = "Setup: Each player reveals 1 card from the top of the encounter deck, and adds it to the staging area.",
                OppositeText = "When Revealed: Search the encounter deck for 1 Hill Troll is one is not already in play, and place it in the staging area. Shuffle the encounter deck.\r\nPlayers cannot defeat this stage while any Hill Troll cards are in play.",
                EncounterSet = "Journey Down the Anduin",
                Quantity = 1,
                QuestPoints = 8,
                Number = 126
            });
            Cards.Add(new Card() {
                ImageName = "M1127",
                Title = "Anduin Passage",
                StageNumber = 2,
                Id = "51223bd0-ffd1-11df-a976-0801200c9135",
                CardType = CardType.Quest,
                EncounterSet = "Journey Down the Anduin",
                Quantity = 1,
                Text = "Reveal 1 additional card from the encounter deck each quest phase. Do not make engagement checks during the encounter phase. (Each player may still optionally engage 1 enemy each encounter phase.)",
                QuestPoints = 16,
                Number = 127
            });
            Cards.Add(new Card() {
                ImageName = "M1128",
                StageNumber = 3,
                Title = "Ambush on the Shore",
                Id = "51223bd0-ffd1-11df-a976-0801200c9137",
                CardType = CardType.Quest,
                EncounterSet = "Journey Down the Anduin",
                Quantity = 1,
                Text = "When Revealed: Reveal 2 encounter cards per player, and add them to the staging area.\r\nSkip the staging step of the quest phase for the remainder of the game.\r\nOnce there are no enemies in play, the players have won the game.",
                QuestPoints = 0,
                Number = 128
            });
        }
    }
}
