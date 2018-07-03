using Shaiya.Origin.Common.Logging;
using System;

namespace Shaiya.Origin.Game
{
    internal class Program
    {
        /// <summary>
        /// The entry point for the Shaiya Origin Game server. This is used to initialise the
        /// <see cref="GameService"/> instance.
        /// </summary>
        /// <param name="args">The command-line arguments</param>
        private static void Main(string[] args)
        {
            // The game service instance
            GameService service = new GameService();

            // Initialise the logger
            Logger.Initialise(Environment.CurrentDirectory + "/logs/");

            if (!service.ParseConfig(args.Length >= 1 ? args[0] : "./Data/config.json"))
            {
                Logger.Error("Failed to parse configuration file! Exiting...");
                return;
            }

            Logger.Info("Game server configuration has been parsed successfully!");

            // Start the service instance
            service.Start();

            Console.ReadLine();
        }
    }
}