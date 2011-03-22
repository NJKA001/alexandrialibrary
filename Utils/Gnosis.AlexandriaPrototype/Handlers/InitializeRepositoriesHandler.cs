using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public class InitializeRepositoriesHandler : Handler<IInitializeRepositoriesMessage>, IInitializeRepositoriesHandler
    {
        protected override void HandleMessage(IInitializeRepositoriesMessage message)
        {
            Controller.Initialize();
        }

        public IRepositoryController Controller { get; set; }
    }
}
