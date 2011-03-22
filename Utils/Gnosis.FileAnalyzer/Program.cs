using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Gnosis.Core;

namespace Gnosis.FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter writer;

            var settings = GetSettings(args);
            if (settings == null)
            {
                Console.WriteLine("Usage: -s:<search directory> -d:<log file directory> -n:<log file name> -i:<include subdirectories (true|false)> -o:<overwrite log (true|false)");
                return;
            }

            if (!Directory.Exists(settings.SearchDirectory))
            {
                Console.WriteLine("Search directory does not exist: {0}", settings.SearchDirectory);
                return;
            }

            if (File.Exists(settings.LogFilePath))
            {
                if (settings.OverwriteLog)
                {
                    File.Delete(settings.LogFilePath);
                    writer = new StreamWriter(settings.LogFilePath);
                }
                writer = new StreamWriter(settings.LogFilePath, true);
            }
            else
            {
                writer = new StreamWriter(settings.LogFilePath);
            }

            Console.WriteLine("Analysis Started");

            using (writer)
            {
                var directory = new DirectoryInfo(settings.SearchDirectory);
                LogDuplicates(writer, directory, settings);
            }

            Console.WriteLine("Analysis Completed");
            Console.WriteLine("See {0} for results", settings.LogFilePath);
        }

        private static string GetFileExtension(FileInfo file)
        {
            var index = file.Name.LastIndexOf('.');
            if (index > -1 && index < file.Name.Length -1)
                return file.Name.Substring(index + 1);

            return string.Empty;
        }

        private static string GetFileHash(FileInfo file)
        {
            return file.Name.AsDoubleMetaphone();
        }

        private static string GetFileDuplicate(string hash, IDictionary<string, string> map)
        {
            if (map.ContainsKey(hash))
                return map[hash];

            return string.Empty;
        }

        private static void AddFileToExtMap(string hash, FileInfo file, IDictionary<string, string> extMap)
        {
            extMap.Add(hash, file.Name);
        }

        private static void LogDuplicates(StreamWriter writer, DirectoryInfo directory, Settings settings)
        {
            Console.WriteLine("  {0}", directory.FullName);
            var map = new Dictionary<string, Dictionary<string, string>>();
            var writeDirectoryName = true;

            foreach (var file in directory.GetFiles())
            {
                var ext = GetFileExtension(file);
                var hash = GetFileHash(file);
                if (map.ContainsKey(ext))
                {
                    var duplicate = GetFileDuplicate(hash, map[ext]);
                    if (!string.IsNullOrEmpty(duplicate))
                    {
                        if (writeDirectoryName)
                        {
                            writer.WriteLine(directory.FullName);
                            writeDirectoryName = false;
                        }

                        writer.WriteLine("  {0}\t\t\t\t{1}", file.Name, duplicate);
                        Console.WriteLine("    ====> {0} duplicates {1}", file.Name, duplicate);
                    }
                    else
                    {
                        AddFileToExtMap(hash, file, map[ext]);
                        Console.WriteLine("  {0}", file.Name);
                    }
                }
                else
                {
                    var extMap = new Dictionary<string, string>();
                    AddFileToExtMap(hash, file, extMap);
                    map.Add(ext, extMap);
                }
            }

            writer.Flush();

            if (settings.IncludeSubdirectories)
            {
                foreach (var subDirectory in directory.GetDirectories())
                {
                    LogDuplicates(writer, subDirectory, settings);
                }
            }
        }

        private static Settings GetSettings(string[] args)
        {
            var settings = new Settings();

            try
            {
                foreach (var arg in args)
                {
                    var tokens = arg.Split(':');
                    if (tokens.Length != 2)
                        return null;

                    switch (tokens[0])
                    {
                        case "-s":
                            settings.SearchDirectory = tokens[1].Replace("\"", "");
                            break;
                        case "-d":
                            settings.LogFileDirectory = tokens[1].Replace("\"", "");
                            break;
                        case "-n":
                            settings.LogFileName = tokens[1].Replace("\"", "");
                            break;
                        case "-i":
                            settings.IncludeSubdirectories = bool.Parse(tokens[1]);
                            break;
                        case "-o":
                            settings.OverwriteLog = bool.Parse(tokens[1]);
                            break;
                        default:
                            return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return settings;
        }
    }
}
