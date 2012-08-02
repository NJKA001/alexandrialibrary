using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Validation
{
    public interface IPathValidator
    {
        PathValidation Validate(IMediaPath path);
    }
}
