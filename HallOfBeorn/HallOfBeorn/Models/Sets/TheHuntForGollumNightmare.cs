using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheHuntForGollumNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Hunt for Gollum Nightmare";
            Abbreviation = "THfGN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";

            Cards.Add(new Card()
            {
                Title = "The Hunt for Gollum Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Jpg,
                Id = "",
                CardType = CardType.Nightmare_Setup,
                Text =
@"You are playing in Nightmare mode.

Forced: At the end of the refresh phase, choose an unclaimed Clue card not attached to a Mordor enemy and attach it to a Mordor enemy, if able. (If it was guarded, detach the encounter guarding it first. The newly attached Mordor enemy is now guarding it.)

If at any point there are four or more Clue cards attached to Mordor enemies, the players lose the game.",
                FlavorText = "\"Through Mirkwood and back again it led them, though they never caught him.\"\r\n-Gandalf, The Fellowship of the Ring",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for The Hunt for Gollum scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

2x River Ninglor
1x Eaves of Mirkwood
2x False Lead
2x Misty Mountain Goblins
2x Banks of the Anduin
3x Gladden Fields
3x Eastern Crows
2x Treacherous Fog
3x Evil Storm

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard The Hunt for Gollum encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",

                EncounterSet = "The Hunt for Gollum Nightmare",
                Quantity = 1,
                Number = 1
            });
        }
    }
}