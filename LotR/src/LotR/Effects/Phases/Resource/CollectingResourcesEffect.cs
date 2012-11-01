using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Phases.Resource;

namespace LotR.Effects.Phases.Resource
{
    public class CollectingResourcesEffect
        : FrameworkEffectBase
    {
        public CollectingResourcesEffect(IGame game, ICollectingResources collectingResources)
            : base(GetDescription(collectingResources), game)
        {
            if (collectingResources == null)
                throw new ArgumentNullException("collectingResources");

            this.collectingResources = collectingResources;
        }

        private readonly ICollectingResources collectingResources;

        private static string GetDescription(ICollectingResources collectingResources)
        {
            if (!collectingResources.IsCollectingResources || collectingResources.ResourcesToCollect == 0)
                return string.Format("Character {0} does not collect any resources", collectingResources.Character.Title);

            return collectingResources.ResourcesToCollect > 1 ?
                string.Format("Character {0} collects {1} resources", collectingResources.Character.Title, collectingResources.ResourcesToCollect)
                : string.Format("Character {0} collects 1 resource", collectingResources.Character.Title);
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            if (!collectingResources.IsCollectingResources || collectingResources.ResourcesToCollect == 0)
                return;

            collectingResources.Character.Resources += collectingResources.ResourcesToCollect;
        }
    }
}
