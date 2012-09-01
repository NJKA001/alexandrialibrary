using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
