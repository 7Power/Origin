using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace Shaiya.Origin.Common.Service
{
    /// <summary>
    /// Represents a service instance, which is used to initialise each service, parse the configuration,
    /// and optionally, open and maintain a connection the the database server.
    /// </summary>
    public abstract class Service
    {
        private static JObject _config;

        /// <summary>
        /// Starts this <see cref="Service"/> instance, which should be overridden by each service
        /// implementation, for initialisation of service specific features.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Attempts to parse the configuration instance for this service.
        /// </summary>
        /// <param name="configPath">The path to the configuration file</param>
        /// <returns>If the configuration was successfully parsed</returns>
        public bool ParseConfig(string configPath)
        {
            // If the file doesn't exist, return false
            if (!File.Exists(configPath))
            {
                return false;
            }

            // The content of the configuration file
            string configString = File.ReadAllText(configPath, Encoding.UTF8);

            // The JObject instance
            JObject json = JObject.Parse(configString);

            // Set the configuration instance
            _config = json;

            // Return a successfully parsed configuration file
            return true;
        }

        /// <summary>
        /// Gets the configuration object in the form of an <see cref="JObject"/>, which
        /// includes the parsed configuration instance
        /// </summary>
        /// <returns>The configuration instance></returns>
        public static JObject GetConfig()
        {
            return _config;
        }

        /// <summary>
        /// Gets a value of a specified key, or the default value
        /// </summary>
        /// <param name="key">The specified key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value of the specified key, or the default value</returns>
        public static JToken GetValueOrDefault(string key, JToken defaultValue)
        {
            return GetConfig().TryGetValue(key, out JToken value) ? value : defaultValue;
        }
    }
}