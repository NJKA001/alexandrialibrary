﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.DublinCore
{
    public class DcAccrualPeriodicity
        : DublinCoreElement, IDcAccrualPeriodicity
    {
        public DcAccrualPeriodicity(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}