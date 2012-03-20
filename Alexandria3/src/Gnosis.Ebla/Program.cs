using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;
using Gnosis.Data.SQLite;
using Gnosis.Logging;
using Gnosis.Logging.Log4Net;

namespace Gnosis.Ebla
{
    class Program
    {
        private static ILogger logger;
        private static IEntityCache<IArtist> artistCache;
        private static IEntityCache<IWork> workCache;
        private static IEntityStore<IArtist> artistStore;
        private static IEntityStore<IWork> workStore;
        private static IEntityRepository repository;
        private static IMediaTypeFactory mediaTypeFactory;

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

                artistCache = new EntityCache<IArtist>();
                workCache = new EntityCache<IWork>();
                artistStore = new SQLiteArtistDatabase();
                workStore = new SQLiteWorkDatabase(artistCache, workCache);
                repository = new EntityRepository(logger, artistCache, artistStore, workCache, workStore);
                repository.Initialize();
                mediaTypeFactory = new MediaTypeFactory(logger);

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

            var command = tokens[0] ?? string.Empty;
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
                    Console.WriteLine("Unrecognized Command: {0}", command);
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

            logger.Debug("Import: " + type);

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
                    Console.Write("Path: ");
                    ImportPath(Console.ReadLine());
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
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ImportPath(path);
        }

        private static void ImportMyMusic()
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            ImportPath(path);
        }

        private static void ImportMyPictures()
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ImportPath(path);
        }

        private static void ImportMyVideos()
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            ImportPath(path);
        }

        private static int importCount;

        private static void ImportPath(string path)
        {
            if (string.IsNullOrEmpty(path) || !System.IO.Directory.Exists(path))
            {
                Console.WriteLine("ERROR: Path not found");
                return;
            }

            Console.WriteLine("Importing: \"{0}\"", path);
            Console.WriteLine();

            importCount = 0;
            var start = DateTime.Now;

            ProcessDirectory(path);

            var finished = string.Format("Importing Finished: {0} files in {1} seconds", importCount, DateTime.Now.Subtract(start).TotalSeconds);
            logger.Info(finished);
            Console.WriteLine(finished);
        }

        private static void ProcessDirectory(string path)
        {
            logger.Debug("Directory: " + path);
            Console.WriteLine("Directory: {0}", path);

            foreach (var file in System.IO.Directory.GetFiles(path))
            {
                importCount++;
                var mediaType = mediaTypeFactory.GetMediaType(new Uri(file));
                var fileDebug = string.Format("  {0, -20} {1}", mediaType.Name, file);

                logger.Debug(fileDebug);
                Console.WriteLine(fileDebug);
            }

            foreach (var child in System.IO.Directory.GetDirectories(path))
            {
                ProcessDirectory(child);
            }
        }
    }
}
