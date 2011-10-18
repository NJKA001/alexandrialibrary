using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class MediaSummaryRequest
        : IMediaSummaryRequest
    {
        public MediaSummaryRequest(string pattern, Action<IMediaSummary> itemCallback, Action completedCallback)
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
        private readonly Action<IMediaSummary> itemCallback;
        private readonly Action completedCallback;

        public string Pattern
        {
            get { return pattern; }
        }

        public Action<IMediaSummary> ItemCallback
        {
            get { return itemCallback; }
        }

        public Action CompletedCallback
        {
            get { return completedCallback; }
        }
    }
}
