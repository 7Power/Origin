namespace Shaiya.Origin.Common.Database
{
    /// <summary>
    /// Contains all the opcodes, that are used for communication between the login
    /// and game servers, and the database server.
    /// </summary>
    public class Opcodes
    {
        /// <summary>
        /// Represents a packet sent from the login server, to the database server,
        /// requesting that a user login request is verified against the database details.
        ///
        /// The client sends the username requesting to login, and an MD5 hash of their password.
        /// The database server responds with an instance of <see cref="Structs.Login.LoginResponse"/>.
        /// </summary>
        public const int USER_LOGIN_REQUEST = 1;

        /// <summary>
        /// Represents a packet sent from the login server to the database server,
        /// requesting the server list.
        /// </summary>
        public const int SERVER_LIST = 2;

        /// <summary>
        /// Represents a packet sent from the login server, informing the
        /// database server that a session should be deleted
        /// </summary>
        public const int DELETE_SESSION = 3;

        /// <summary>
        /// Represents a packet sent from the game server, informing the database
        /// server that a user has connected to the game server, and that it should
        /// verify the session.
        /// </summary>
        public const int USER_GAME_CONNECT = 4;

        /// <summary>
        /// Represents a packet sent from the game server, requesting the details
        /// of a game account to be loaded from the database, and sent to the game server.
        /// </summary>
        public const int GAME_USER_LOAD = 5;
    }
}