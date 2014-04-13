using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class ConflictAtTheCarrockNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Conflict at the Carrock Nightmare";
            Abbreviation = "CatCN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";

            Cards.Add(new Card()
            {
                Title = "Conflict at the Carrock Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Png,
                Id = "3AB0C72E-CF30-4486-A8B4-EE816BBFC90F",
                CardType = CardType.Nightmare_Setup,
                Text =
@"You are playing in Nightmare mode.

" + "Stage 1A should read \"Remove 5 unique Troll cards\" instead of 4.\r\n\r\n" +

"Stage 2B should read. \"When Revealed: Place Louis, Morris, Rupert and Stuart into the staging area.\"\r\n\r\n" +

@"Forced: At the end of the quest phase, if no progress was placed on the current quest this phase, place 1 progress on the current quest (bypassing the active location).

" +

"Reponse: After defeating a unique Troll enemy, you may choose and discard 1 \"Sacked!\" card from play.",

                OppositeText =
@"Begin with the standard quest deck and encounter deck for the Conflict at the Carrock scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

1x Grimbeorn the Old
1x Louis
1x Morris
1x Stuart
1x Rupert
3x Bee Pastures
3x Oak-wood Grove
1x Roasted Slowly
3x Misty Mountain Goblins
2x Banks of the Anduin
1x Wolf Rider
2x Goblin Sniper
2x Wargs
2x Despair
2x The Brown Lands
2x The East Bight

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard Conflict at the Carrock encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",

                EncounterSet = "Conflict at the Carrock Nightmare",
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card()
            {
                Title = "",
                ImageType = Models.ImageType.Png,
                Id = "",
                CardType = CardType.Enemy,
                EngagementCost = 0,
                Threat = 0,
                Attack = 0,
                Defense = 0,
                HitPoints = 0,
                Traits = new List<string> { "Troll." },
                Text = "",
                Shadow = "Shadow:",
                EncounterSet = "Conflict at the Carrock Nightmare",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card()
            {
                Title = "",
                ImageType = Models.ImageType.Png,
                Id = "",
                CardType = CardType.Enemy,
                EngagementCost = 0,
                Threat = 0,
                Attack = 0,
                Defense = 0,
                HitPoints = 0,
                Traits = new List<string> { "Troll." },
                Text = "",
                Shadow = "Shadow:",
                EncounterSet = "Conflict at the Carrock Nightmare",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card()
            {
                Title = "",
                ImageType = Models.ImageType.Png,
                Id = "",
                CardType = CardType.Enemy,
                EngagementCost = 0,
                Threat = 0,
                Attack = 0,
                Defense = 0,
                HitPoints = 0,
                Traits = new List<string> { "Troll." },
                Text = "",
                Shadow = "Shadow:",
                EncounterSet = "Conflict at the Carrock Nightmare",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card()
            {
                Title = "",
                ImageType = Models.ImageType.Png,
                Id = "",
                CardType = CardType.Enemy,
                EngagementCost = 0,
                Threat = 0,
                Attack = 0,
                Defense = 0,
                HitPoints = 0,
                Traits = new List<string> { "Troll." },
                Text = "",
                Shadow = "Shadow:",
                EncounterSet = "Conflict at the Carrock Nightmare",
                Quantity = 1,
                Number = 5
            });
        }
    }
}