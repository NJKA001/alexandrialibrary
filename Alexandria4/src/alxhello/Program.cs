using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Serialization;

namespace Gnosis.Alexandria.Hello
{
    class Program
    {
        static void Main(string[] args)
        {
            var converter = new MessageConverter();
            var body = "Hello Alexandria";
            
            if (args != null && args.Length > 0)
            {
                if (!string.IsNullOrEmpty(args[0]))
                {
                    body = args[0];
                }
            }

            var message = new Message() { Type = MessageType.Json, Header = "Test Message", Body = body };
            var output = converter.SeralizeToJson(message);

            Console.WriteLine(output);
        }
    }
}
