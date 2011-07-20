using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.DublinCore
{
    public class DcAccrualMethod
        : DublinCoreElement, IQualifiedDublinCoreElement
    {
        public DcAccrualMethod(string content)
            : base("AccrualMethod", content)
        {
        }
    }
}
