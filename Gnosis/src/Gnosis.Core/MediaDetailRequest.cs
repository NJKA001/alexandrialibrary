using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class MediaDetailRequest
        : IMediaDetailRequest
    {
        public MediaDetailRequest(string pattern, Action<IMediaDetail> itemCallback, Action completedCallback)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");
            if (itemCallback == null)
                throw new ArgumentNullException("itemCallback");

            this.pattern = pattern;
            this.itemCallback = itemCallback;
            this.completedCallback = completedCallback;
        }

        private readonly string pattern;
        private readonly Action<IMediaDetail> itemCallback;
        private readonly Action completedCallback;

        public string Pattern
        {
            get { return pattern; }
        }

        public Action<IMediaDetail> ItemCallback
        {
            get { return itemCallback; }
        }

        public Action CompletedCallback
        {
            get { return completedCallback; }
        }
    }
}
