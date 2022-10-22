using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Debugging;

namespace Utilities
{
    public static class Logger
    {
        private static ILogger _Logger = null;

        private static Boolean IsInitialized = false;

        public static ILogger ContextLog<T>() where T : class
        {
            if (_Logger == null)
            {
                InitLogger();
            }

            return _Logger.ForContext<T>();
        }


        public static ILogger ILogger
        {
            get { return _Logger; }
        }

        public static string CurrentFolder =>
            Path.GetDirectoryName(
                Uri.UnescapeDataString(
                    new UriBuilder(
                        Assembly.GetExecutingAssembly().CodeBase
                    ).Path
                )
            );

        /// <summary>Initializes the Logger based on the Logger Config file</summary>
        public static void InitLogger()
        {
            if (!IsInitialized)
            {
                //String folder = @"C:\Users\pucma\OneDrive - FH JOANNEUM\Master\Semester 2\AD_SWE\Puchas_AD_SWE\advswe1\Server\src\Utilities";
                String folder = CurrentFolder;

                SelfLog.Enable(message => { Console.WriteLine(message); });

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(folder)
                    .AddJsonFile("loggerconfig.json")
                    .AddJsonFile("loggerconfig.Development.json", true)
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom
                    .Configuration(configuration)
                    .CreateLogger();

                _Logger = Log.Logger;
                Log.Verbose("Logger initialized in Folder " + folder);
                IsInitialized = true;
            }
        }

        public static void Flush()
        {
            Log.CloseAndFlush();
        }


        public static void Warning(String message)
        {
            Log.Warning(message);
            Flush();
        }


        public static void Warning(String message, Exception e)
        {
            Log.Warning(message, e);
            Flush();
        }

        public static void Warn(String message)
        {
            Log.Warning(message);
            Flush();
        }

        public static void Warn(String message, Exception e)
        {
            Log.Warning(message, e);
            Flush();
        }


        public static void Fatal(String message)
        {
            Log.Fatal(message);
            Flush();
        }

        public static void Fatal(String message, Exception e)
        {
            Log.Fatal(message, e);
            Flush();
        }

        public static void Information(String message)
        {
            Log.Information(message);
            Flush();
        }

        public static void Information(String message, Exception e)
        {
            Log.Information(message, e);
            Flush();
        }

        public static void Info(String message)
        {
            Log.Information(message);
            Flush();
        }

        public static void Info(String message, Exception e)
        {
            Log.Information(message, e);
            Flush();
        }

        public static void Verbose(String message)
        {
            Log.Verbose(message);
            Flush();
        }

        public static void Verbose(String message, Exception e)
        {
            Log.Verbose(message, e);
            Flush();
        }


        public static void Debug(String message)
        {
            Log.Debug(message);
            Flush();
        }

        public static void Debug(String message, Exception e)
        {
            Log.Debug(message, e);
            Flush();
        }


        public static void Error(String message)
        {
            Log.Error(message);
            Flush();
        }

        public static void Error(String message, Exception e)
        {
            Log.Error(message, e);
            Flush();
        }
    }
}