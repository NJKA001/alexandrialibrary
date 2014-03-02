using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class VoiceofIsengard : CardSet
    {
        protected override void Initialize()
        {
            Name = "Voice of Isengard";
            Abbreviation = "VoI";
            Number = 2;
            SetType = Models.SetType.Deluxe_Expansion;
            Cycle = "Ring-Maker";

            Cards.Add(new Card() {
                Title = "Éomer",
                ImageType = Models.ImageType.Png,
                NormalizedTitle = "Eomer",
                Id = "FE720A40-0522-4882-B8F4-5F3E4E120E67",
                CardType = CardType.Hero,
                ThreatCost = 10,
                Willpower = 1,
                Attack = 3,
                Defense = 2,
                HitPoints = 4,
                Sphere = Models.Sphere.Tactics,
                IsUnique = true,
                Quantity = 1,
                Traits = new List<string>() { "Rohan.", "Noble.", "Warrior." },
                Text = "Response: After a character leaves play, Éomer gets +2 Attack until the end of the round. (Limit once per round.)",
                FlavorText = "\"I am named Éomer son of Éomund, and am called the Third Marshal of Riddermark.\" -The Two Towers",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Gríma",
                ImageType = Models.ImageType.Png,
                NormalizedTitle = "Grima",
                Id = "2BECB7B6-5D0C-46D1-83C3-DDE505ABEB5E",
                CardType = CardType.Hero,
                ThreatCost = 9,
                Willpower = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                Sphere = Models.Sphere.Lore,
                IsUnique = true,
                Quantity = 1,
                Traits = new List<string>() { "Rohan.", "Isengard." },
                Text = "Action: Lower the cost of the next card you play from your hand this round by 1. That card gains Doomed 1. (Limit once per round.)",
                FlavorText = "\"Let your counsellor Gríma keep all things till your return - and I pray that we may see it, though no wise man will deem it hopeful.\" -The Two Towers",
                Number = 2,
                SlugIncludesType = true,
            });
            Cards.Add(new Card() {
                Title = "Saruman",
                ImageType = Models.ImageType.Png,
                Id = "E0C9F997-2FC0-46AA-9B50-A8F8CDC6C31B",
                CardType = CardType.Ally,
                ResourceCost = 3,
                Willpower = 3,
                Attack = 5,
                Defense = 4,
                HitPoints = 4,
                Sphere = Models.Sphere.Neutral,
                IsUnique = true,
                Quantity = 3,
                Traits = new List<string>() { "Istari.", "Isengard." },
                Keywords = new List<string> { "Doomed 3.", "At the end of the round, discard Saruman from play." },
                Text = "Response: After Saruman enters play, choose a non-unique enemy or location in the staging area. While Saruman is in play, the chosen enemy or location is considered to be out of play.",
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Orthanc Guard",
                ImageType = Models.ImageType.Png,
                Id = "9A940560-831D-4E4A-8C46-9BFA886B9465",
                CardType = CardType.Ally,
                ResourceCost = 2,
                Willpower = 1,
                Attack = 0,
                Defense = 2,
                HitPoints = 2,
                Sphere = Models.Sphere.Leadership,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Isengard." },
                Text = "Response: After you raise your threat from the Doomed keyword, ready Orthanc Guard.",
                FlavorText = "\"...the keepers of the gate were on the watch for me and told me that Saruman awaited me.\" -Gandalf, The Fellowship of the Ring",
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Isengard Messenger",
                ImageType = Models.ImageType.Png,
                Id = "70BF219C-AF95-42EE-A2CB-92448E63F276",
                CardType = CardType.Ally,
                ResourceCost = 2,
                Willpower = 1,
                Attack = 0,
                Defense = 1,
                HitPoints = 2,
                Sphere = Models.Sphere.Lore,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Isengard." },
                Text = "Response: After you raise your threat from the Doomed keyword, Isengard Messenger gets +1 Willpower until the end of the round. (Limit twice per round.)",
                FlavorText = "\"I have an urgent errand,\" he said. \"My news is evil.\" -Radagast, The Fellowship of the Ring",
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Westfold Outrider",
                ImageType = Models.ImageType.Png,
                Id = "76F84AE2-DDB9-4402-A7FA-7A3EA13F2190",
                CardType = CardType.Ally,
                ResourceCost = 2,
                Willpower = 0,
                Attack = 2,
                Defense = 1,
                HitPoints = 2,
                Sphere = Models.Sphere.Tactics,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Rohan.", "Scout." },
                Text = "Action: Discard Westfold Outdier to choose an enemy not engaged with you. Engage the chosen enemy.",
                FlavorText = "\"Erkenbrand of Westfold has drawn off those men he could gather towards his fastness at Helm's Deep. The rest are scattered.\"-Rider of Rohan, The Two Towers",
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Westfold Horse-breeder",
                ImageType = Models.ImageType.Png,
                Id = "C4287846-77C8-4685-A293-9165B180DBDD",
                CardType = CardType.Ally,
                ResourceCost = 1,
                Willpower = 1,
                Attack = 0,
                Defense = 0,
                HitPoints = 1,
                Sphere = Models.Sphere.Spirit,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Rohan." },
                Text = "Response: After Westfold Horse-breeder enters play, search the top 10 cards of your deck for a Mount attachment and add it to your hand. Shuffle your deck.",
                FlavorText = "\"They love their horses next to their kin.\" -Boromir, The Fellowship of the Ring",
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Rohan Warhorse",
                ImageType = Models.ImageType.Png,
                Id = "FEB80BF1-5F72-49B4-BFDD-7D125529C23F",
                CardType = CardType.Attachment,
                ResourceCost = 1,
                Sphere = Models.Sphere.Tactics,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Mount." },
                Keywords = new List<string> { "Attach to a Tactics or Rohan hero.", "Restricted." },
                Text = "Response: After attached hero participates in an attack that destroys an enemy, exhaust Rohan Warhorse to ready attached hero.",
                FlavorText = "Their horses were of great stature, strong and clean-limbed... -The Two Towers",
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Silver Lamp",
                ImageType = Models.ImageType.Png,
                Id = "0FF953B2-66CB-4760-9F30-56066D94448C",
                CardType = CardType.Attachment,
                ResourceCost = 2,
                Sphere = Models.Sphere.Spirit,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Keywords = new List<string> { "Attach to a Spirit hero." },
                Text = "While attached hero is ready, shadow cards dealt to enemies engaged with you are dealt face up. (Shadow card effects are still resolved when resolving enemy attacks.)",
                FlavorText = "...one of them uncovered a small lamp that gave out a slender silver beam. -The Fellowship of the Ring",
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Keys of Orthanc",
                ImageType = Models.ImageType.Png,
                Id = "F61BB4CD-9239-4AD4-9869-985DE696FFDF",
                CardType = CardType.Attachment,
                ResourceCost = 1,
                Sphere = Models.Sphere.Neutral,
                IsUnique = true,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Keywords = new List<string> { "Attach to a hero." },
                Text = "Response: After you raise your threat from the Doomed keyword, exhaust Keys of Othanc to add 1 resource to attached hero's resource pool.",
                FlavorText = "\"He has the Key of Orthanc\" -Gandalf, The Two Towers",
                Number = 10
            });
            Cards.Add(new Card(){
                Title = "Legacy of Númenor",
                NormalizedTitle = "Legacy of Numenor",
                ImageType = Models.ImageType.Png,
                Id = "9482929A-87C0-4993-8E76-D80E1A40C3DA",
                CardType = CardType.Event,
                ResourceCost = 0,
                Sphere = Models.Sphere.Leadership,
                IsUnique = false,
                Quantity = 3,
                Keywords = new List<string> { "Doomed 4." },
                Text = "Action: Add 1 resource to each hero's resource pool.",
                FlavorText = "\"...in the midst of that valley is a tower of stone called Orthanc. It was not made by Saruman, but by the Men of Númenor long ago: and it is very tall and has many secrets...\" -Gandalf, The Fellowship of the Ring",
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Deep Knowledge",
                ImageType = Models.ImageType.Png,
                Id = "E68FD84E-D79D-42D6-85B1-8DF6B7F5FDC8",
                CardType = CardType.Event,
                ResourceCost = 0,
                Sphere = Models.Sphere.Lore,
                IsUnique = false,
                Quantity = 3,
                Keywords = new List<string> { "Doomed 2." },
                Text = "Action: Each player draws 2 cards.",
                FlavorText = "\"His knowledge is deep, but his pride has grown with it...\" -Gandalf, The Fellowship of the Ring",
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "The Wizard's Voice",
                ImageType = Models.ImageType.Png,
                Id = "AB0B49A0-3A74-433F-AED3-206964FABD18",
                CardType = CardType.Event,
                ResourceCost = 0,
                Sphere = Models.Sphere.Tactics,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Spell." },
                Keywords = new List<string> { "Doomed 3." },
                Text = "Action: Each player chooses 1 enemy engaged with him. Until the end of the phase, each chosen enemy cannot attack the player that chose it.",
                FlavorText = "...it was a delight to hear the voice speaking, all that it said seemed wise and reasonable... -The Two Towers",
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Power of Orthanc",
                ImageType = Models.ImageType.Png,
                Id = "19FE9EB6-82EE-4967-9381-E9E7C97B2463",
                CardType = CardType.Event,
                ResourceCost = 0,
                Sphere = Models.Sphere.Spirit,
                IsUnique = false,
                Quantity = 3,
                Traits = new List<string>() { "Spell." },
                Keywords = new List<string> { "Doomed 2." },
                Text = "Action: Each player may choose and discard a Condition attachment from play.",
                FlavorText = "\"But Saruman has long studied the arts of the Enemy himself, and thus we have often been able to forestall him.\" -Gandalf, The Fellowship of the Ring",
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "The Seeing-stone",
                ImageType = Models.ImageType.Png,
                Id = "F54077B5-277B-4E73-ADE0-98D0EA9EC2AC",
                CardType = CardType.Event,
                ResourceCost = 0,
                Sphere = Models.Sphere.Neutral,
                IsUnique = false,
                Quantity = 3,
                Keywords = new List<string> { "Doomed 1." },
                Text = "Action: Search your deck for a card with the Doomed keyword and add it to your hand. Shuffle your deck.",
                FlavorText = "\"...alone it could do nothing but see small images of things far off and days remote.\" -Gandalf, The Two Towers",
                Number = 15
            });
        }
    }
}