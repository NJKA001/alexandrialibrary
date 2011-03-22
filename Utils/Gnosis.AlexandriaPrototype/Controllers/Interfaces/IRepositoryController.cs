using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers.Interfaces
{
    public interface IRepositoryController : IController
    {
        void Initialize();
    }
}
