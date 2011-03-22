using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Messages
{
    public class NewSearchTabRequestedMessage : INewSearchTabRequestedMessage
    {
        public NewSearchTabRequestedMessage()
        {
        }

        public string Search { get; set; }
    }
}
