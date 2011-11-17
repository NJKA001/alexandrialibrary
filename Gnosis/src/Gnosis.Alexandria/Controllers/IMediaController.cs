﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface IMediaController
    {
        ITask<IEnumerable<IMediaDetail>> Search(string pattern);
    }
}