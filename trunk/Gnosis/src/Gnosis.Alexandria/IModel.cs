﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IModel
    {
        object Id { get; }
        bool IsDeleted { get; }
        void Delete();
        void Initialize(object id);
    }
}
