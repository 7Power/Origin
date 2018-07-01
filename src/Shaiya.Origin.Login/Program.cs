using Shaiya.Origin.Common.Logging;
using System;

namespace Shaiya.Origin.Login
{
    internal class Program
    {
        /// <summary>
        /// The entry point for the Shaiya Origin Login Server. This is used to initialise the
        /// <see cref="LoginService"/> instance.
        /// </summary>
        /// <param name="args">The command-line arguments</param>
        private static void Main(string[] args)
        {
            // The login service instance
            LoginService service = new LoginService();

            // Initialise the logger
            Logger.Initialise(Environment.CurrentDirectory + "/logs/");

            if (!service.ParseConfig(args.Length >= 1 ? args[0] : "./Data/config.json"))
            {
                Logger.Error("Failed to parse configuration file! Exiting...");
                return;
            }

            Logger.Info("Login server configuration has been parsed successfully!");

            // Start the service instance
            service.Start();

            Console.ReadLine();
        }
    }
}