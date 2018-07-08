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

            // Write a few newlines to the log files
            using (_errorFile = new StreamWriter(File.Open(_logDirectory + "error.log", FileMode.Create)))
            {
                _errorFile.WriteLine("#### Initialising ###");
                _errorFile.WriteLine(Environment.NewLine);
            }

            using (_infoFile = new StreamWriter(File.Open(_logDirectory + "info.log", FileMode.Create)))
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
        /// <param name="message">The message to write</param>
        /// <returns></returns>
        public static void Error(string message)
        {
            lock (_syncObject)
            {
                // Create a logger instance
                Logger logger = new Logger();

                StringBuilder stringBuilder = new StringBuilder("[ERROR]");

                stringBuilder.AppendFormat("[{0}] ", DateTime.Now.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture));

                stringBuilder.Append(message);

                Console.WriteLine(stringBuilder.ToString());

                using (_errorFile = new StreamWriter(_logDirectory + "error.log", true))
                {
                    _errorFile.WriteLine(message);
                    _errorFile.Close();
                }
            }
        }

        /// <summary>
        /// Logs an error message to both the console, and the log file specified.
        /// </summary>
        /// <param name="format">The specified format</param>
        /// <param name="arg">The object to pass in</param>
        /// <returns></returns>
        public static void Error(string format, params object[] arg)
        {
            lock (_syncObject)
            {
                // Create a logger instance
                Logger logger = new Logger();

                StringBuilder stringBuilder = new StringBuilder("[ERROR]");

                stringBuilder.AppendFormat("[{0}] ", DateTime.Now.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture));

                stringBuilder.AppendFormat(format, arg);

                Console.WriteLine(stringBuilder.ToString());

                using (_errorFile = new StreamWriter(_logDirectory + "error.log", true))
                {
                    _errorFile.WriteLine(format, arg);
                    _errorFile.Close();
                }
            }
        }

        /// <summary>
        /// Logs an information message to both the console, and the log file specified.
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <returns></returns>
        public static void Info(string message)
        {
            lock (_syncObject)
            {
                // Create a logger instance
                Logger logger = new Logger();

                StringBuilder stringBuilder = new StringBuilder("[INFO]");

                stringBuilder.AppendFormat("[{0}] ", DateTime.Now.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture));

                stringBuilder.Append(message);

                Console.WriteLine(stringBuilder.ToString());

                using (_infoFile = new StreamWriter(_logDirectory + "info.log", true))
                {
                    _infoFile.WriteLine(message);
                    _infoFile.Close();
                }
            }
        }

        /// <summary>
        /// Logs an information message to both the console, and the log file specified.
        /// </summary>
        /// <param name="format">The specified format</param>
        /// <param name="arg">The object to pass in</param>
        /// <returns></returns>
        public static void Info(string format, params object[] arg)
        {
            lock (_syncObject)
            {
                // Create a logger instance
                Logger logger = new Logger();

                StringBuilder stringBuilder = new StringBuilder("[INFO]");

                stringBuilder.AppendFormat("[{0}] ", DateTime.Now.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture));

                stringBuilder.AppendFormat(format, arg);

                Console.WriteLine(stringBuilder.ToString());

                using (_infoFile = new StreamWriter(_logDirectory + "info.log", true))
                {
                    _infoFile.WriteLine(format, arg);
                    _infoFile.Close();
                }
            }
        }
    }
}