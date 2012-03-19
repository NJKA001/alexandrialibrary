using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Culture
{
    /// <summary>
    /// Defines a written script based on ISO 15924
    /// </summary>
    public interface IScript
    {
        string Code { get; }
        int Number { get; }
        string Name { get; }
    }
}
