using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Logging;
using Gnosis.Logging.Log4Net;

namespace Gnosis.Ebla
{
    class Program
    {
        private static ILogger logger;
        
        const string prompt = "ebla>";
        const string commandExit = ".exit";
        const string commandHelp = ".help";
        const string commandImport = ".import";

        static void Main(string[] args)
        {
            logger = Log4NetLogger.GetDefaultLogger(typeof(Program));

            try
            {
                logger.Info("Ebla Started");

                Console.WriteLine("Ebla version 3.0.0.0");
                Console.WriteLine("Enter \"{0}\" for instructions", commandHelp);

                var exit = false;
                while (!exit)
                {
                    Console.Write(prompt);
                    exit = Execute(Console.ReadLine());
                }
            }
            catch (Exception ex)
            {
                logger.Error("Program.Main", ex);

                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static bool Execute(string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;

            var tokens = line.Split(' ');
            if (tokens == null || tokens.Length == 0)
                return false;

            var command = tokens[0];
            var options = new List<string>();
            if (tokens.Length > 1)
            {
                options = tokens.ToList();
                options.RemoveAt(0);
            }

            switch (command)
            {
                case commandExit:
                    return true;
                case commandHelp:
                    DisplayHelp();
                    break;
                case commandImport:
                    Import(options);
                    break;
                default:
                    break;
            }

            return false;
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("{0,-16}Exit this program", commandExit);
            Console.WriteLine("{0,-16}Show this message", commandHelp);
            Console.WriteLine("{0,-16}Import media of type ALL*|DOCUMENTS|MUSIC|PICTURES|VIDEOS|PATH", commandImport + " TYPE");
        }

        private static void Import(List<string> options)
        {
            const string importTypeAll = "ALL";
            const string importTypeDocuments = "DOCUMENTS";
            const string importTypeMusic = "MUSIC";
            const string importTypePictures = "PICTURES";
            const string importTypeVideos = "VIDEOS";
            const string importTypePath = "PATH";

            var type = (options != null && options.Count > 0 && options[0] != null) ? options[0].ToUpper() : importTypeAll;
            switch (type)
            {
                case importTypeAll:
                    ImportAll();
                    break;
                case importTypeDocuments:
                    ImportMyDocuments();
                    break;
                case importTypeMusic:
                    ImportMyMusic();
                    break;
                case importTypePath:
                    Console.WriteLine(">path: ");
                    var path = Console.ReadLine();

                    if (!string.IsNullOrEmpty(path) && System.IO.Directory.Exists(path))
                        ImportPath(path);
                    else
                        Console.WriteLine("ERROR: Path not found");
                        break;
                case importTypePictures:
                    ImportMyPictures();
                    break;
                case importTypeVideos:
                    ImportMyVideos();
                    break;
                default:
                    Console.WriteLine("Unrecognized import type. Valid types are: ALL, DOCUMENTS, MUSIC, PICTURES, VIDEOS, PATH");
                    break;
            }
        }

        private static void ImportAll()
        {
            ImportMyDocuments();
            ImportMyMusic();
            ImportMyPictures();
            ImportMyVideos();
        }

        private static void ImportMyDocuments()
        {
            Console.WriteLine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        private static void ImportMyMusic()
        {
            Console.WriteLine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
        }

        private static void ImportMyPictures()
        {
            Console.WriteLine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }

        private static void ImportMyVideos()
        {
            Console.WriteLine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
        }

        private static void ImportPath(string path)
        {
            Console.WriteLine("Importing Path: \"{0}\"", path);
        }
    }
}
