using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Validation
{
    public class LocalPathValidator
        : IPathValidator
    {
        public PathValidation Validate(IMediaPath path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            try
            {
                foreach (var invalidChar in Path.GetInvalidPathChars())
                {
                    if (path.AbsolutePath.Contains(invalidChar))
                        return new PathValidation(false, false);
                }
            }
            catch (Exception validationError)
            {
                return new PathValidation(false, false, validationError);
            }

            try
            {
                if (Directory.Exists(path.AbsolutePath))
                    return new PathValidation(true, true);
                else if (File.Exists(path.AbsolutePath))
                    return new PathValidation(true, true);
                else
                    return new PathValidation(true, false);
            }
            catch (Exception existenceError)
            {
                return new PathValidation(true, false, existenceError);
            }
        }
    }
}
