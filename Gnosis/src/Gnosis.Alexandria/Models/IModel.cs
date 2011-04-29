using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IModel
    {
        IModelContext ModelContext { get; }
        Guid Id { get; }
        bool IsNew { get; }
    }
}
