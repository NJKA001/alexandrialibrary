﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IWorkLink
        : IRelationship<IWork, IMedia>
    {
    }
}
