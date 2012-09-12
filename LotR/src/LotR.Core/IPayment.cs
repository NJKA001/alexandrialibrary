﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IPayment
    {
        string Description { get; }

        IEnumerable<IPayment> Children { get; }
    }
}
