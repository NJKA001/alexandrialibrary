using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;
using Gnosis.Data.SQLite;
using Gnosis.Extensions;
using Gnosis.Importing;
using Gnosis.Logging;
using Gnosis.Logging.Log4Net;
using Gnosis.Tagging;
using Gnosis.Tagging.TagLib;

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
        private static IMediaFactory mediaFactory;
        private static IMediaImporter mediaImporter;
        private static ITagger tagger;

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

                tagger = new Tagger();

                mediaFactory = new MediaFactory(logger);
                mediaImporter = new MediaImporter(logger, mediaFactory, repository, tagger);

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
                Console.ReadLine();
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

        private static IList<string> ignoreExtensions = new List<string> { ".dll", ".exe", ".ini" };

        private static bool MediaPathIsValid(string path)
        {
            if (path == null)
                return false;

            foreach (var extension in ignoreExtensions)
                if (path.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase))
                    return false;

            return true;
        }

        private static bool MediaIsValid(IMedia media)
        {
            if (media == null)
                return false;

            if (media.Type.Name == mediaFactory.DefaultType.Name)
                return false;

            return true;
        }

        private static void MediaImported(IImportInfo importInfo)
        {
            importCount++;
            var entityType = "Unknown";
            if (importInfo.Entity != null)
            {
                if (importInfo.Entity is IArtist)
                    entityType = ((IArtist)importInfo.Entity).Type.ToString();
                else if (importInfo.Entity is IWork)
                    entityType = ((IWork)importInfo.Entity).Type.ToString();
            }
            Console.WriteLine("  {0,-20} {1,-10} {2}", importInfo.MediaType.Name, entityType, importInfo.Path.TruncatePath(46));
        }

        private static uint importCount;

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

            mediaImporter.SetMediaPathFilter(mediaPath => MediaPathIsValid(mediaPath));
            mediaImporter.SetMediaFilter(media => MediaIsValid(media));
            mediaImporter.SetDirectoryCallback(dir => Console.WriteLine(dir.TruncatePath(78)));
            mediaImporter.SetImportCallback(importInfo => MediaImported(importInfo));
            mediaImporter.SetCompletedCallback(() =>
            {
                var finished = string.Format("Importing Finished: {0} files in {1} seconds", importCount, DateTime.Now.Subtract(start).TotalSeconds);
                logger.Info(finished);
                Console.WriteLine(finished);
            });
            
            mediaImporter.Import(path);
        }
    }
}
