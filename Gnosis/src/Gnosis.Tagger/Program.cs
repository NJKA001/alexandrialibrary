using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tags.TagLib;

namespace Gnosis.Tagger
{
    class Program
    {
        private enum Mode
        {
            Read = 0,
            Write = 1
        }

        private static bool isValidUsage;
        private static Mode mode = Mode.Read;
        private static string file;
        private static bool all = true;
        private static string tag;
        private static string value;

        #region Constants

        const string fileShort = "-f";
        const string fileLong = "-file";
        const string readShort = "-r";
        const string readLong = "-read";
        const string writeShort = "-w";
        const string writeLong = "-write";
        const string allShort = "-a";
        const string allLong = "-all";
        const string tagShort = "-t";
        const string tagLong = "-tag";
        const string valueShort = "-v";
        const string valueLong = "-value";

        #endregion

        private static void Initialize(string[] args)
        {
            bool readFlag = false;
            bool writeFlag = false;
            bool fileFlag = false;
            bool tagFlag = false;
            bool valueFlag = false;
            foreach (var arg in args)
            {
                if (fileFlag)
                {
                    file = arg;
                    fileFlag = false;
                    continue;
                }
                else if (tagFlag)
                {
                    tag = arg;
                    tagFlag = false;
                    continue;
                }
                else if (valueFlag)
                {
                    value = arg;
                    valueFlag = false;
                    continue;
                }

                switch (arg)
                {
                    case fileShort:
                    case fileLong:
                        fileFlag = true;
                        break;
                    case readShort:
                    case readLong:
                        if (writeFlag)
                        {
                            isValidUsage = false;
                            return;
                        }
                        readFlag = true;
                        mode = Mode.Read;
                        break;
                    case writeShort:
                    case writeLong:
                        if (readFlag)
                        {
                            isValidUsage = false;
                            return;
                        }
                        writeFlag = true;
                        mode = Mode.Write;
                        break;
                    case tagShort:
                    case tagLong:
                        if (!string.IsNullOrEmpty(tag))
                        {
                            isValidUsage = false;
                            return;
                        }
                        tagFlag = true;
                        all = false;
                        break;
                    case valueShort:
                    case valueLong:
                        if (!string.IsNullOrEmpty(value))
                        {
                            isValidUsage = false;
                            return;
                        }
                        break;
                    default:
                        isValidUsage = false;
                        break;
                }
            }

            if (!readFlag && !writeFlag)
                mode = Mode.Read;

            if (string.IsNullOrEmpty(file))
                isValidUsage = false;

            if (mode == Mode.Read)
            {
                if (string.IsNullOrEmpty(tag) && !all)
                    isValidUsage = false;
            }
            else if (mode == Mode.Write)
            {
                if (string.IsNullOrEmpty(tag) || string.IsNullOrEmpty(value))
                    isValidUsage = false;
            }

            isValidUsage = true;
        }

        private static void DisplayUsage()
        {
            //Console.WriteLine();
            //Console.WriteLine("-f {0} -t {1} mode {2}", file, tag, mode);
            Console.WriteLine();
            Console.WriteLine("Usage: tagger -f [PATH] (-r|-w) (-a) (-t [NAME]) (-v [VALUE])");
            Console.WriteLine("  -f -file : the path of the file with embedded metadata tags. REQUIRED");
            Console.WriteLine("  -r -read : read tags from the given file. OPTIONAL, DEFAULT");
            Console.WriteLine("  -w -write: write tags to the given file. OPTIONAL");
            Console.WriteLine("  -a -all  : read all tags from the given file. OPTIONAL, DEFAULT, NEEDS -r");
            Console.WriteLine("  -t -tag  : read or write the tag with the given name. OPTIONAL");
            Console.WriteLine("  -v -value: write the given value to the given tag. OPTIONAL, NEEDS -w and -t");
        }

        private static object GetTagValue(Tag t, string tag)
        {
            switch (tag)
            {
                case "TIT2":
                    return t.Title;
                case "TALB":
                    return t.Album;
                default:
                    return null;
            }
        }

        static int Main(string[] args)
        {
            Console.WriteLine("Gnosis Media Tagger v. 1.0.0.0");

            try
            {
                Initialize(args);

                if (!isValidUsage)
                {
                    DisplayUsage();
                    return 0;
                }

                var tagFile = File.Create(file);
                var tagContainer = tagFile.GetTag(TagTypes.Id3v2);
                if (tagContainer == null)
                {
                    Console.WriteLine("Could not load tags for {0}", file);
                    return 0;
                }

                if (mode == Mode.Read)
                {
                    if (all)
                        Console.WriteLine("  read all tags");
                    else
                    {
                        var value = GetTagValue(tagContainer, tag);
                        if (value != null)
                            Console.WriteLine("{0}: {1}", tag, value);
                        else
                            Console.WriteLine("{0} is not defined", tag);
                    }
                }
                else if (mode == Mode.Write)
                {
                    Console.WriteLine("  wrote to tag: {0}", tag);
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return 1;
            }
        }
    }
}
