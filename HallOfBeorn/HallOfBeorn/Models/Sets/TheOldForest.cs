using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheOldForest : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Old Forest ";
            Abbreviation = "TOF";
            Number = 3004;
            SetType = Models.SetType.GenConSaga_Expansion;
            Cycle = "GenCon";

            Cards.Add(new Card()
            {
                Title = "The Old Forest Introduction",
                Id = "1C97EC47-0AF3-4038-9FE1-FC308E965487",
                CardType = CardType.GenCon_Setup,
                CampaignCardType = CampaignCardType.Introduction,
                OppositeText = 
@"~Escape the agents of ~Mordor searching for you in the ~Shire by braving a trip through the strange woods on its border in The Old ~Forest, a scenario designed for The Lord of the ~Rings: The Card Game special event at Gen Con 2014! This scenario can be played as a stand-alone adventure or as part of your Lord of the Rings campaign. To play The Old ~Forest in campaign mode, see page 4/4 of this expansion.

Expansion Symbol

The cards in The Old ~Forest scenario can be identified by this symbol before each card's collector number.",
                OppositeFlavorText = "\"But you won't have any luck in the Old Forest,\" objected Fedegar, \"No one ever had luck there. You'll get lost.\"\r\n-The Fellowship of the Ring",
                Number = 0,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "The Old Forest Scenario Rules",
                Id = "D54CFC5A-86CD-4CDD-BB8F-91FEE2D19171",
                CardType = CardType.GenCon_Setup,
                CampaignCardType = CampaignCardType.Scenario_Rules,
                Text =
@"New Staging Rules

When playing The Old Forest, players reveal encounter cards individually in player order during the Staging step of the Quest phase. If an encounter card has and effect that uses the word 'you' then the encounter card is referring to the player who revealed the card. If the revealed card has the Surge keyword, the player who revealed it reveals an additional card card. Encounter cards with the Doomed X keyword still affect each player.

Peril Keyword

When a player reveals an encounter card with the Peril keyword, he must resolve the staging of that card on his own without conferring with the other players. The other players cannot take any actions or trigger any responses during the resolution of that card's staging.
",
                OppositeText = 
@"Stage 2 Quest Cards

When Stage 2B instructs the players to advance to a different random stage 2A, the first player shuffles each stage 2 (except for the one currently in play) together and chooses one of them at random. Then, he replaced the current stage 2 with the chosen one. The previous stage 2 is placed back in the quest deck with the other unused stage 2 cards. Discard any progress or attachments that were on the previous stage 2.

Immune to Player Card Effects

Cards with the text 'Immune to player card effects' ignore the effects of all player cards. Additionally, cards that are immune to player card effects cannot be chosen as targets of player card effects.

Indestructible

An enemy with the indestructible keyword cannot be destroyed by damage, even when it has damage on it equal to its hit points.",
                Number = 0,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "The Old Forest Campaign Rules",
                Id = "13A4D055-9C6B-4ADF-A3D5-2EAEBC309B97",
                CardType = CardType.GenCon_Setup,
                Text = 
@"The Old Forest can be played as part of The Lord of the Rings campaign. It should be played after A Shadow of the Past, found in The Black Riders Saga Expansion. To play The Old Forest in campaign mode, follow the setup instructions found on page 2 of The Black Riders rules insert.

Campaign Mode Components

The Old Forest expansion includes 2 cards that can only be used when playing the scenario in campaign mode: one double-sided campaign card and one boon card. The description for each of these card types can be found on page 4 of The Black Riders rules insert.

The Lord of the Rings: The Black Riders Saga Expansion is required to play The Old Forest in campaign mode.",
                Number = 0,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Farewell to the Shire",
                Id = "1351C3CA-4CC0-445E-BF8F-7FBE21E3F407",
                CardType = Models.CardType.Quest,
                StageNumber = 1,
                QuestPoints = 0,
                Text = 
@"Setup: Set Old Man Willow and Withywindle aside, out of play. Each player searches the encounter deck and adds 1 different location to the staging area. Shuffle the encounter deck.",
                FlavorText = "After being pursued through the Shire by mysterious riders in black, Frodo and his friends decide that the safest way for them to continue their journey to Rivendell is to travel through the Old Forest.",
                OppositeText = "When Revealed: Each player draws 1 card. Advance to a random stage 2A.",
                OppositeFlavorText = "\"You have left the Shire, and are now outside, and on the edge of the Old Forest.\" -Merry, The Fellowship of the Ring",
                EncounterSet = "The Old Forest",
                Number = 1,
                Quantity = 1,
                Artist = Artist.Brian_Valenzuela
            });
            Cards.Add(new Card()
            {
                Title = "The Living Forest",
                Id = "F63FC7D8-D1BD-46FD-8E3C-8DB75003E524",
                OppositeTitle = "Dark Bad Place",
                SlugIncludesOppositeTitle = true,
                CardType = CardType.Quest,
                StageNumber = 2,
                QuestPoints = null,
                Text = 
@"When Revealed: If the number of locations in the staging area is less than the number of players in the game, search the encounter deck and discard pile for a location and add it to the staging area. Shuffle the encounter deck.",
                FlavorText = "\"Everything in it is very much more alive, more aware of what is going on, so to speak, than things are in the Shire. And the trees do not like strangers.\"\r\n-Merry, The Fellowship of the Ring",
                EncounterSet = "The Old Forest",
                Number = 2,
                Quantity = 1,
                Artist = Artist.Silver_Saaremael,
                SecondArtist = Artist.Nathalia_Gomes
            });
            Cards.Add(new Card()
            {
                Title = "The Living Forest",
                Id = "CA2C0050-2E27-49BE-B539-880551CE3983",
                OppositeTitle = "Choked with Brambles",
                SlugIncludesOppositeTitle = true,
                CardType = CardType.Quest,
                StageNumber = 2,
                QuestPoints = null,
                Text =
@"When Revealed: If the number of locations in the staging area is less than the number of players in the game, search the encounter deck and discard pile for a location and add it to the staging area. Shuffle the encounter deck.",
                FlavorText = "\"Everything in it is very much more alive, more aware of what is going on, so to speak, than things are in the Shire. And the trees do not like strangers.\"\r\n-Merry, The Fellowship of the Ring",
                EncounterSet = "The Old Forest",
                Number = 3,
                Quantity = 1,
                Artist = Artist.Silver_Saaremael,
                SecondArtist = Artist.Silver_Saaremael
            });
            Cards.Add(new Card()
            {
                Title = "The Living Forest",
                Id = "2C31C1F5-2AA5-46A7-8641-767A6D8B16E6",
                OppositeTitle = "Shifting Trees",
                SlugIncludesOppositeTitle = true,
                CardType = CardType.Quest,
                StageNumber = 2,
                QuestPoints = null,
                Text =
@"When Revealed: If the number of locations in the staging area is less than the number of players in the game, search the encounter deck and discard pile for a location and add it to the staging area. Shuffle the encounter deck.",
                FlavorText = "\"Everything in it is very much more alive, more aware of what is going on, so to speak, than things are in the Shire. And the trees do not like strangers.\"\r\n-Merry, The Fellowship of the Ring",
                EncounterSet = "The Old Forest",
                Number = 4,
                Quantity = 1,
                Artist = Artist.Jose_Vega,
                SecondArtist = Artist.Nathalia_Gomes
            });
            Cards.Add(new Card()
            {
                Title = "The Living Forest",
                Id = "64FFA0F3-FDF4-4694-B20E-DC079F6DD958",
                OppositeTitle = "Closing in Around Them",
                SlugIncludesOppositeTitle = true,
                CardType = CardType.Quest,
                StageNumber = 2,
                QuestPoints = null,
                Text =
@"When Revealed: If the number of locations in the staging area is less than the number of players in the game, search the encounter deck and discard pile for a location and add it to the staging area. Shuffle the encounter deck.",
                FlavorText = "\"Everything in it is very much more alive, more aware of what is going on, so to speak, than things are in the Shire. And the trees do not like strangers.\"\r\n-Merry, The Fellowship of the Ring",
                EncounterSet = "The Old Forest",
                Number = 5,
                Quantity = 1,
                Artist = Artist.Silver_Saaremael,
                SecondArtist = Artist.Joel_Hustak
            });
            Cards.Add(new Card()
            {
                Title = "The Wicked Willow",
                Id = "A4F9F2CD-4D9A-46FB-A247-B97FE669CAF8",
                CardType = CardType.Quest,
                StageNumber = 3,
                QuestPoints = 18,
                Text =
@"When Revealed: Add Old Man Willow and Withywindle to the staging area. Reveal X encounter cards where X is the number of players in the game minus 1.",
                FlavorText = "He lifted his heavy eyes and saw leaning over him a huge willow-tree, old and hoary. Enormous it looked, its sprawling branches going up like reaching arms with many long-fingered hands... -The Fellowship of the Ring",
                OppositeFlavorText = "\"I don't like this great big tree. I don't trust it. Hark at it singing about sleep now!\" -Sam, The Fellowship of the Ring",
                OppositeText = 
@"Old Man Willow cannot leave the staging area but is considered to be engaged with each player and attacks each player in turn order during the combat phase (deal and resolve a shadow card for each attack).

If the players defeat this stage, they win the game.",
                EncounterSet = "The Old Forest",
                Number = 6,
                Quantity = 1,
                Artist = Artist.Ignacio_Bazan_Lazcano
            });
            Cards.Add(new Card()
            {
                Title = "Old Man Willow",
                Id = "985C2E66-3A70-41E7-8126-B10A5C3E94AA",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 3,
                Attack = 5,
                Defense = 4,
                HitPoints = 12,
                Traits = new List<string> { "Tree." },
                Keywords = new List<string> { "Indestructible.", "Immune to player card effects." },
                Text = "Forced: When Old Man Willow attacks, discard a random location from the victory display. The defending player raises his threat by X, where X is that location's victory point value.",
                EncounterSet = "The Old Forest",
                Number = 7,
                Quantity = 1,
                Artist = Artist.Silver_Saaremael
            });
        }
    }
}