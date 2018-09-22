using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Shaiya.Origin.Common.Logging
{
    public class Logger
    {
        // The log files
        private static StreamWriter _errorFile;

        private static StreamWriter _infoFile;

        // The log directory
        private static string _logDirectory;

        private static readonly object _syncObject = new object();

        /// <summary>
        /// Initialises the logger instance with a specific log directory, and opens the log files
        /// for writing
        /// </summary>
        /// <param name="logDirectory"></param>
        public static void Initialise(string logDirectory)
        {
            // Set the log directory
            _logDirectory = logDirectory;

            // Create the log directory if it doesn't exist
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            // Create the log files if they don't exist and write a few newlines to them
            using (_errorFile = new StreamWriter(File.Exists(_logDirectory + "error.log") ? File.Open(_logDirectory + "error.log", FileMode.Create) : File.Open(_logDirectory + "error.log", FileMode.CreateNew)))
            {
                _errorFile.WriteLine("#### Initialising ###");
                _errorFile.WriteLine(Environment.NewLine);
            }

            using (_infoFile = new StreamWriter(File.Exists(_logDirectory + "info.log") ? File.Open(_logDirectory + "info.log", FileMode.Create) : File.Open(_logDirectory + "info.log", FileMode.CreateNew)))
            {
                _infoFile.WriteLine("#### Initialising ###");
                _infoFile.WriteLine(Environment.NewLine);
            }

            const string banner = "\n\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\n\x23\x20\x20\x20\x5f\x5f\x5f\x20\x20\x20\x20\x20\x20\x20\x5f\x20\x20\x20\x20\x20\x20\x20\x5f\x20\x20\x20\x20\x20\x20\x20\x20\x23\n\x23\x20\x20\x2f\x20\x5f\x20\x5c\x20\x5f\x20\x5f\x5f\x28\x5f\x29\x20\x5f\x5f\x20\x5f\x28\x5f\x29\x5f\x20\x5f\x5f\x20\x20\x20\x23\n\x23\x20\x7c\x20\x7c\x20\x7c\x20\x7c\x20\x27\x5f\x5f\x7c\x20\x7c\x2f\x20\x5f\x60\x20\x7c\x20\x7c\x20\x27\x5f\x20\x5c\x20\x20\x23\n\x23\x20\x7c\x20\x7c\x5f\x7c\x20\x7c\x20\x7c\x20\x20\x7c\x20\x7c\x20\x28\x5f\x7c\x20\x7c\x20\x7c\x20\x7c\x20\x7c\x20\x7c\x20\x23\n\x23\x20\x20\x5c\x5f\x5f\x5f\x2f\x7c\x5f\x7c\x20\x20\x7c\x5f\x7c\x5c\x5f\x5f\x2c\x20\x7c\x5f\x7c\x5f\x7c\x20\x7c\x5f\x7c\x20\x23\n\x23\x20\x20\x20\x20\x20\x20\x20\x20\x20\x20\x20\x20\x20\x20\x20\x7c\x5f\x5f\x5f\x2f\x20\x20\x20\x20\x20\x20\x20\x20\x20\x20\x23\n\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\x23\xd\xa";

            // Print the banner
            Console.Write(banner);
        }

        /// <summary>
        /// Logs an error message to both the console, and the log file specified.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="args">The arguments to pass in, if any</param>
        /// <returns></returns>
        public static void Error(string message, params object[] args)
        {
            lock (_syncObject)
            {
                StringBuilder stringBuilder = new StringBuilder("[ERROR]");

                stringBuilder.AppendFormat("[{0}] ", DateTime.Now.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture));

                stringBuilder.AppendFormat(message, args);

                Console.WriteLine(stringBuilder.ToString());

                using (_errorFile = new StreamWriter(_logDirectory + "error.log", true))
                {
                    _errorFile.WriteLine(message, args);
                    _errorFile.Close();
                }
            }
        }

        /// <summary>
        /// Logs an information message to both the console, and the log file specified.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="arg">The arguments to pass in, if any</param>
        /// <returns></returns>
        public static void Info(string message, params object[] args)
        {
            lock (_syncObject)
            {
                StringBuilder stringBuilder = new StringBuilder("[INFO]");

                stringBuilder.AppendFormat("[{0}] ", DateTime.Now.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture));

                stringBuilder.AppendFormat(message, args);

                Console.WriteLine(stringBuilder.ToString());

                using (_infoFile = new StreamWriter(_logDirectory + "info.log", true))
                {
                    _infoFile.WriteLine(message, args);
                    _infoFile.Close();
                }
            }
        }
    }
}