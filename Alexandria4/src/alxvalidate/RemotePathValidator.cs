using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Gnosis.Alexandria.Validation
{
    public class RemotePathValidator
        : IPathValidator
    {
        public PathValidation Validate(IMediaPath path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            Uri location;

            try
            {
                location = new Uri(path.AbsolutePath, UriKind.Absolute);
            }
            catch (Exception locationError)
            {
                return new PathValidation(false, false, locationError);
            }

            try
            {
                var request = HttpWebRequest.Create(location);
                var response = request.GetResponse();
                return new PathValidation(true, true);
            }
            catch (Exception requestError)
            {
                return new PathValidation(true, false, requestError);
            }
        }
    }
}
