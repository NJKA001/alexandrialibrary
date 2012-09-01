using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Serialization;

namespace Gnosis.Alexandria.Echo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var converter = new MessageConverter();

                Console.WriteLine("Alexandria Echo");

                var input = Console.In.ReadToEnd();

                var message = converter.DeserializeFromJson<Message>(input);

                Console.WriteLine("Message Received");
                Console.WriteLine("Type: {0}", message.Type);
                Console.WriteLine("Header: {0}", message.Header);
                Console.WriteLine("Body: {0}", message.Body);

                //Console.WriteLine("Press Enter to close");
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message Error: " + ex.Message);
            }
        }
    }
}
