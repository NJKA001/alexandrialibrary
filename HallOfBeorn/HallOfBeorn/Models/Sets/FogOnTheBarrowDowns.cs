using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class FogOnTheBarrowDowns : CardSet
    {
        protected override void Initialize()
        {
            Name = "Fog on the Barrow-downs";
            Abbreviation = "FotBD";
            Number = 3005;
            SetType = Models.SetType.Fellowship_Deck;
            Cycle = "GenCon";
            DisablePublicImages = true;

            Cards.Add(new Card()
            {
                Title = "Fog on the Barrow-downs Scenario Rules",
                Id = "BE094FA5-B976-4888-AB7A-D3E257E390C9",
                ImageType = Models.ImageType.Jpg,
                HasSecondImage = true,
                CardType = CardType.GenCon_Setup,
                CampaignCardType = CampaignCardType.Scenario_Rules,
                Text = 
@"New Staging Rules

When playing Fog on the Barrow-downs, players reveal encounter cards individually in player order during the Staging step of the Quest phase. If an encounter card has an effect that uses the word 'you' then the encounter card is referring to the player who revealed the card. If the revealed card has the Surge keyword, the player who revealed it reveals an additional card. Encounter cards with the Doomed X keyword still affect each player.

Peril Keyword

When a player reveals an encounter card with the the Peril keyword, he must resolve that staging of that card on his own without conferring with ther other players. The other players cannot take any actions or trigger any responses during the resolution of that card's staging.",
                OppositeText =
@"Creating a Staging Area

When a player is instructed to create his own staging area, he sets aside an area in front of himself to serve as his own separate staging area. Players continue to resolve each phase of the game in turn order, starting with the first player, but the resolution of each phase occurs as if only the player or players that share any given staging area are present in the game.

Players cannot affect players or cards that do not share a common staging area. The players as a group still cannot have more than 1 copy of a unique card in play.

During the encounter phase, players only reveal 1 card per player that shares their staging area. Encounter card effects are limited to players and cards at that stage. Effects that reference 'each player' only affect each player at that staging area.",
                Number = 0,
                Quantity = 1,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Fog on the Barrow-downs Campaign Rules",
                Id = "E953D925-0DA7-4AA4-AE85-A98A8C1815DA",
                ImageType = Models.ImageType.Jpg,
                HasSecondImage = true,
                CardType = CardType.GenCon_Setup,
                CampaignCardType = CampaignCardType.Scenario_Rules,
                Text =
@"Stage 3B - Lost in the Fog

Stage 3B (and its staging area) must remain in play until it is defeated. If there are no players at stage 3B, skip each phase at that stage.

Combining Staging Areas

When a player is instructed to combine staging areas with another staging area, each enemy and location card in that player's staging area is added to the other staging area. Enemies engaged with a player remain engaged with him.

Immune to Play Card Effects

Cards with the text 'Immune to player card effects' ignore the effects of all player cards. Additionally, cards that are immune to player card effects cannot be chosen as targets of player card effects.",
                OppositeText =
@"Campaign Mode

Fog on the Barrow-downs can be played as part of The Lord of the Rings campaign. It should be played before A Knife in the ~Dark, found in The Black Riders Saga Expansion. To play Fog on the Barrow-downs in campaign mode, follow the setup instructions found on page 2 of The Black Riders rules insert.

Campaign Mode Components

Fog on the Barrow-downs expansion includes 2 cards that can only be used when playing the scenario in campaign mode: one double-sided campaign card and one boon card. The description for each of these card types can be found on page 4 of The Black Riders rules insert.

The Lord of the ~Rings: The Black Riders Saga Expansion is required to play Fog on the Barrow-downs in campaign mode.",
                Number = 0,
                Quantity = 1,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                ImageType = Models.ImageType.Png,
                Title = "Aragorn",
                Id = "AAF34EE7-24B9-4962-AE9F-079772089407",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 12,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                Willpower = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Dúnedain.", "Noble.", "Ranger." },
                NormalizedTraits = new List<string> { "Dunedain." },
                Text = "Response: After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him.",
                FlavorText = "\"I am Aragorn son of Arathorn; and if by life or death I can save you, I will.\" -The Fellowship of the Ring",
                Keywords = new List<string>() { "Sentinel." },
                Quantity = 1,
                Year = 2014,
                Artist = Artist.Sebastian_Giacobino,
                Number = 1
            });
            Cards.Add(new Card()
            {
                Title = "The House of Tom Bombadil",
                Id = "BACA2A71-4CE6-4003-B8D1-8279869DF137",
                CardType = CardType.Quest,
                HasSecondImage = true,
                ScenarioNumber = 5,
                StageNumber = 1,
                QuestPoints = 1,
                FlavorText = "After rescuing the hobbits from Old Man Willow, Tom Bombadil has invited Frodo and his friends to stay the night in his house. There they are greeted warmly by his wife, Goldberry, the River-daughter.",
                Text = "Setup: Set each copy of Great ~Barrow, Standing Stones, and Hollow Circle aside, out of play. Shuffle the encounter deck.",
                OppositeFlavorText = "\"Let us shit out the night!\" she said. \"For you are still afraid, perhaps, of mist and tree-shadows and deep water, and untame things. Fear nothing! For tonight you are under the roof of Tom Bombadil.\"\r\n-The Fellowship of the Ring",
                OppositeText = "Skip the quest phase.\r\nForced: At the end of the round, place 1 progress on this stage.",
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Emrah_Elmasli
            });
            Cards.Add(new Card()
            {
                Title = "Across the Downs",
                Id = "E15090E3-CF35-4BF5-9B89-38D09CA8A9E0",
                HasSecondImage = true,
                CardType = Models.CardType.Quest,
                ScenarioNumber = 5,
                StageNumber = 2,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 2,
                Artist = Artist.Unknown

            });
            Cards.Add(new Card()
            {
                Title = "Lost in the Fog",
                Id = "56D70012-B295-468A-9064-4E1C22BF9C9D",
                HasSecondImage = true,
                CardType = Models.CardType.Quest,
                ScenarioNumber = 5,
                StageNumber = 3,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 3,
                Artist = Artist.Unknown

            });
            Cards.Add(new Card()
            {
                Title = "Trapped Inside a Barrow",
                Id = "E84070BC-F6B6-4ED3-8CAC-158CE4268696",
                HasSecondImage = true,
                CardType = Models.CardType.Quest,
                ScenarioNumber = 5,
                StageNumber = 4,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 4,
                Number = 4,
                Artist = Artist.Unknown

            });
            Cards.Add(new Card()
            {
                Title = "Hollow Circle",
                IsUnique = true,
                Id = "5FC12A2C-1F79-4EEB-BC43-8432797DCCBC",
                CardType = Models.CardType.Location,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 5,
                Artist = Artist.Unknown

            });
            Cards.Add(new Card()
            {
                Title = "Standing Stones",
                Id = "80AA9A99-4DB0-403E-B013-557406F9C4AC",
                CardType = Models.CardType.Location,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 6,
                Artist = Artist.Unknown

            });
            Cards.Add(new Card()
            {
                Title = "Great Barrow",
                Id = "20749647-0C3C-4BB8-ABD5-C9466452FC60",
                CardType = Models.CardType.Location,
                VictoryPoints = 5,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 7,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Ancient Barrow",
                Id = "DCECC2B9-7AAE-4E77-8268-07DD49FCFFF9",
                CardType = Models.CardType.Location,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 8,
                Artist = Artist.Unknown

            });
            Cards.Add(new Card()
            {
                Title = "Stone Ring",
                Id = "3861F516-CB2D-407C-854D-3590572D69FC",
                CardType = Models.CardType.Location,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 9,
                Artist = Artist.Unknown

            });
            Cards.Add(new Card()
            {
                Title = "North Downs",
                Id = "A98F4E8A-B2DC-48BA-B0A1-FA7AA53A5597",
                CardType = Models.CardType.Location,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 10,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Barrow-wight",
                Id = "74D9131D-BED7-4E9A-B649-05D4344ED339",
                CardType = Models.CardType.Enemy,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 11,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Cold-wight",
                Id = "BE22869A-14ED-45ED-90E1-AA0D3A19A1FF",
                CardType = Models.CardType.Enemy,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 12,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Dark-wight",
                Id = "6F3E5C9B-22B7-409A-B20C-578B49E419C7",
                CardType = Models.CardType.Enemy,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 13,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "I'm Waiting for You!",
                Id = "044CBEF3-2DFF-4153-9D8E-170826EB1B50",
                CardType = Models.CardType.Treachery,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 14,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Long Groping Arm",
                Id = "5CC082D1-028A-40B6-BA2B-551E8AC3AB34",
                CardType = Models.CardType.Treachery,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 15,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Chill Fog",
                Id = "38E841B2-29E8-496C-93A9-13C96031FC2E",
                CardType = Models.CardType.Treachery,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 16,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Frozen by Fear",
                Id = "35971087-EE5B-4075-AE12-4B730AA873EB",
                CardType = Models.CardType.Treachery,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 17,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Dreadful Song",
                Id = "A625C893-4B7B-4765-9E2B-FE403DD5548B",
                CardType = Models.CardType.Treachery,
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 18,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Tom Bombadil",
                IsUnique = true,
                Id = "A611EE75-F6C3-4BC6-97E2-B897F6A36DAC",
                CardType = Models.CardType.Objective_Ally,
                Willpower = 3,
                Attack = 3,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string> { "Legend." },
                Text = "When Revealed: Choose a player. That player gains control of Tom Bombadil, ready and committed to the quest. At the end of the round, discard Tom Bombadil. Then shuffle the encounter discard pile into the encounter deck. This effect cannot be canceled.",
                FlavorText = "\"Tom Bom, Jolly Tom, Tom Bombadillo!\" -Tom Bombadil, The Fellowship of the Ring",
                EncounterSet = "Fog on the Barrow-downs",
                Quantity = 1,
                Number = 19,
                Artist = Artist.Unknown
            });
            Cards.Add(new Card()
            {
                Title = "Fog on the Barrow-downs",
                OppositeTitle = "The Lord of the Rings Part 1.2",
                SlugIncludesType = true,
                HasSecondImage = true,
                Id = "ACC07870-CD7F-4B88-A8A5-6C1FBFF6A55D",
                CardType = CardType.Campaign,
                Text =
@"You are playing Campaign Mode.

Setup: While any player is at stage 4B, the first player cannot lose control of the first player token.",
                FlavorText = "Even in the Shire the rumour of the Barrow-wights of the Barrow-downs beyond the Forest had been heard. But it was not a tale than any hobbit liked to listen to, even by a comfortable fireside far away. -The Fellowship of the Ring",
                OppositeText = "Resolution: The players have earned the Ho! Tom Bombadil! boon card.",
                OppositeFlavorText = "At last they set off. They led their ponies down the hill; and then mounting they trotted quickly along the valley. They looked back and saw the top of the old mound on the hill, and from it the sunlight on the gold went up like a yellow flame. Then they turned a shoulder of the Downs and it was hidden from view.\r\n-The Fellowship of the Ring",
                EncounterSet = "Fog on the Barrow-downs",
                Number = 20,
                Quantity = 1,
                Artist = Artist.Emrah_Elmasli
            });
            Cards.Add(new Card()
            {
                Title = "Ho! Tom Bombadil!",
                Id = "82249D64-2D0A-484D-BB52-AA4F32D98A3E",
                CardType = CardType.Event,
                CampaignCardType = CampaignCardType.Boon,
                ResourceCost = 0,
                Traits = new List<string> { "Song." },
                Text = "Setup: The first player adds this card to his hand.\r\nResponse: Add this card to the victory display and remove it from the campaign pool to cancel the \"when revealed\" effects of an encounter card just revealed from the encounter deck.",
                FlavorText = "By fire, sun and moon, harken now and hear us!\r\nCome, Tom Bombadil, for our need is near us!\r\n-Frodo, The Fellowship of the Ring",
                VictoryPoints = 1,
                Number = 21,
                Quantity = 1,
                Artist = Artist.Romana_Kendelic
            });
        }
    }
}