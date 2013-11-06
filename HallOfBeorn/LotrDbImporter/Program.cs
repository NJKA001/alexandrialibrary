using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LotrDbImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var directory = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lotr-img");

                for (var number = 1001; number < 1971; number++)
                {
                    var url = string.Format("http://www.lotrlcg.com/Images/Cards/Medium/M{0}.png", number);
                    var filename = Path.Combine(directory, "M" + number + ".png");

                    System.Console.WriteLine("Getting image: " + url);

                    try
                    {
                        var request = System.Net.HttpWebRequest.Create(url);

                        using (var response = request.GetResponse())
                        {
                            if (response == null)
                            {
                                Console.WriteLine("Could not get image: " + url);
                                continue;
                            }

                            using (var stream = response.GetResponseStream())
                            {
                                var image = Image.FromStream(stream);
                                image.Save(filename);

                                Console.WriteLine("Image saved: " + filename);
                            }
                        }
                    }
                    catch (Exception reqEx)
                    {
                        Console.WriteLine("Request failed for: " + url);
                        Console.WriteLine(reqEx.Message);
                    }

                    System.Threading.Thread.Sleep(30000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Completed. Press ENTER to close.");
            Console.ReadLine();
        }
    }
}
