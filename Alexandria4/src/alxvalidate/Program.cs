using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Logging;

namespace Gnosis.Alexandria.Validation
{
    public class Program
    {
        private static ILogger logger = new Log4NetLogger(typeof(Program));
        
        private struct Options
        {
            public Options(string path, bool isVerbose)
            {
                this.path = path;
                this.isVerbose = isVerbose;
            }

            private string path;
            private bool isVerbose;

            public string Path
            {
                get { return path; }
            }

            public bool IsVerbose
            {
                get { return isVerbose; }
            }
        }

        static void Main(string[] args)
        {
            logger.Info("alxvalidate starting");
            try
            {
                Options options;

                if (ArgumentsAreValid(args))
                {
                    try
                    {
                        options = GetOptions(args);
                    }
                    catch (Exception optEx)
                    {
                        logger.Error("  invalid arguments: " + string.Join(" ", args), optEx);
                        Console.WriteLine(GetUsageString());
                        return;
                    }

                    var validation = Validate(options.Path);

                    if (!validation.IsValid)
                    {
                        Console.WriteLine("  path is not valid: " + options.Path);
                        if (validation.Error != null)
                            Console.WriteLine("    error: " + validation.Error.Message);

                        return;
                    }

                    if (!validation.Exists)
                    {
                        Console.WriteLine("  path does not exist: " + options.Path);
                        if (validation.Error != null)
                            Console.WriteLine("    error: " + validation.Error.Message);

                        return;
                    }

                    Console.WriteLine("  media validated successfully: " + options.Path);
                }
                else
                {
                    Console.WriteLine(GetUsageString());
                }
            }
            catch (Exception ex)
            {
                logger.Error("  alxvalidate error", ex);
                Console.WriteLine("An error occurred in alxvalidate:" + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace);
            }
#if DEBUG
            finally
            {
                Console.WriteLine("[enter] to close");
                Console.ReadLine();
            }
#endif
        }

        private static bool ArgumentsAreValid(string[] args)
        {
            if (args == null || args.Length == 0)
                return false;

            return true;
        }

        private static string GetUsageString()
        {
            var usage = new StringBuilder();
            usage.AppendLine("alxvalidate <path> [-v]");
            usage.AppendLine("  <path>         path of the media to validate (required)");
            usage.AppendLine("  -v -verbose    display verbose output (default is false)");
            
            return usage.ToString();
        }

        private static Options GetOptions(string[] args)
        {
            var path = ".";
            var isVerbose = false;

            for (var i = 0; i < args.Length; i++)
            {
                if (i == 0)
                {
                    path = args[i];
                }
                else
                {
                    switch (args[i])
                    {
                        case "-v":
                        case "-verbose":
                            isVerbose = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            return new Options(path, isVerbose);
        }

        private static PathValidation Validate(string path)
        {
            var validator = new UniversalPathValidator();
            
            return validator.Validate(path);
        }
    }
}
