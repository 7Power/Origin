namespace Shaiya.Origin.Common.Packets
{
    /// <summary>
    /// Here we define all the various opcodes for the packets used throughout the Shaiya protocol.
    /// Theoretically, these values should never change, even throughout episodes.
    /// </summary>
    public class Opcodes
    {
        /// <summary>
        /// The ping packet sent by the client, which is used to ensure the client is still connected.
        /// </summary>
        public const int PING = 0x03;

        /// <summary>
        /// Called whenever a login session is terminated, either through ALT + F4, or the "Quit Game" button.
        /// </summary>
        public const int LOGIN_TERMINATE = 0x010B;

        /// <summary>
        /// The login handshake is the first packet sent from the server, to the client, and is responsible
        /// for defining the values used for the encryption algorithm.
        /// </summary>
        public const int LOGIN_HANDSHAKE = 0xA101;

        /// <summary>
        /// A standard login request, sent to the login server, which contains the username and password
        /// values. The server responds with the login result, and if the login result equals 0,
        /// then the server also sends the user id, privilege level, and session identity keys.
        /// </summary>
        public const int LOGIN_REQUEST = 0xA102;

        /// <summary>
        /// The server list packet sent by the server, which contains the various server ids,
        /// the server names, and the server status/population.
        /// </summary>
        public const int SERVER_LIST_DETAILS = 0xA201;

        /// <summary>
        /// Sent by the client when selecting the game server. The client sends the server id
        /// it wishes to connect to, as well as the client version. If the client version matches
        /// that of the server, the packet contains the ip address of the game server that the client
        /// should attempt to connect to.
        /// </summary>
        public const int SELECT_GAME_SERVER = 0xA202;

        /// <summary>
        /// The game handshake request sent by the client, which should contain the previously assigned
        /// user id and session identity keys from the login request.
        /// </summary>
        public const int GAME_HANDSHAKE = 0xA301;

        /// <summary>
        /// Contains the current faction of the game account (either Light, Fury, or undecided).
        /// </summary>
        public const int ACCOUNT_FACTION = 0x0109;
    }
}