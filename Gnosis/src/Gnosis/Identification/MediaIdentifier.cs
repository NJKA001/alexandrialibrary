using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public class MediaIdentifier
        : IMediaIdentifier
    {
        public MediaIdentifier()
        {
            examiner = new MediaHasLocation();
            //examiner.AddChild(new 
        }

        private IMediaExaminer examiner;

        public IMediaInfo Identify(Uri location)
        {
            var info = new MediaInfo(location);

            return examiner.CanExamine(info) ?
                examiner.Examine(info)
                : info;
        }
    }
}
