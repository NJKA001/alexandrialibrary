using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Messages
{
    public class SearchRequestedMessage : TargetedMessage, ISearchRequestedMessage
    {
        public SearchRequestedMessage()
        {
        }

        public string Search { get; set; }
    }
}
