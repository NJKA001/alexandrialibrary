using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gnosis.Alexandria.Validation
{
    public class UniversalPathValidator
        : IPathValidator
    {
        public PathValidation Validate(IMediaPath path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var validator = path.IsLocal ?
                new LocalPathValidator() as IPathValidator
                : new RemotePathValidator() as IPathValidator;

            return validator.Validate(path);
        }
    }
}
