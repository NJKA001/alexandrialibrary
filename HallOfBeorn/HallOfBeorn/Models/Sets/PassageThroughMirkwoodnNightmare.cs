using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class PassageThroughMirkwoodnNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Passage Through Mirkwood Nightmare";
            Abbreviation = "PTMN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";

            Cards.Add(new Card()
            {
                Title = "Passage Through Mirkwood Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Jpg,
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5001",
                CardType = CardType.Nightmare_Setup,
                //Keywords = new List<string>() { "Setup." },
                //Text = "The Quest Deck has been modified for Nightmare Mode. Flip this card over and keep it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",
                Text = 
@"You are playing in Nightmare mode.

Forced: During setup, each player reveals 1 card from the encounter deck and adds it to the staging area.",
                OppositeText =
@"Begin wit the standard quest deck and encounter deck for the Passage Through Mirkwood scenario found in the LOTR LCG core set.

Remove the following cards, in the specified quantities, from the standard encounter deck:

Ungoliant's Spawn x1
Black ~Forest Bats x1
~Forest ~Spider x3
Dol Guldur Orcs x3
Old ~Forest ~Road x1
~Forest ~Gate x2
Mountains of Mirkwood x3
Caught in a Web x2

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard Passage Through Mirkwood encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effec remains active throughout the game, which is now ready to begin.",

                EncounterSet = "Passage Through Mirkwood Nightmare",
                Quantity = 1,
                Number = 1
            });
        }
    }
}