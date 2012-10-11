using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Quest;
using LotR.States;

namespace LotR.Effects.Costs
{
    //public class ChooseHeroCommitedToTheQuest
    //    : CostBase, ICost
    //{
    //    public ChooseHeroCommitedToTheQuest(ISource source, ICommitToQuestStep step)
    //        : base("Choose a hero committed to the quest", source)
    //    {
    //        this.state = state;
    //    }

    //    private ICommitToQuestStep step;

    //    public override bool IsMetBy(IPayment payment)
    //    {
    //        if (payment == null)
    //            return false;

    //        var choice = payment as IChooseCharacterPayment;
    //        if (choice == null)
    //            return false;

    //        var character = choice.Character as ICardInPlay<ICharacterCard>;
    //        if (character == null || !(character is IHeroCard))
    //            return false;

    //        if (!step.CommitedCharacters.Contains(character))
    //            return false;

    //        return true;
    //    }
    //}

}
