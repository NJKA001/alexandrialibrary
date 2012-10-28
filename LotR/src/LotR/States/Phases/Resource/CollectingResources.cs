using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Resource
{
    public class CollectingResources
        : StateBase, ICollectingResources
    {
        public CollectingResources(IGame game, ICharacterInPlay character, byte resourcesToCollect)
            : base(game)
        {
            this.Character = character;
            this.resourcesToCollect = resourcesToCollect;
        }

        private bool isCollectingResources = true;
        private byte resourcesToCollect;

        public ICharacterInPlay Character
        {
            get;
            private set;
        }

        public bool IsCollectingResources
        {
            get { return isCollectingResources; }
            set
            {
                if (isCollectingResources == value)
                    return;

                isCollectingResources = value;
                OnPropertyChanged("IsCollectingResources");
            }
        }

        public byte ResourcesToCollect
        {
            get { return resourcesToCollect; }
            set
            {
                if (resourcesToCollect == value)
                    return;

                resourcesToCollect = value;
                OnPropertyChanged("ResourcesToCollect");
            }
        }
    }
}
