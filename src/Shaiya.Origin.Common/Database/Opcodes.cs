namespace Shaiya.Origin.Common.Database
{
    /// <summary>
    /// Contains all the opcodes, that are used for communication between the login
    /// and game servers, and the database server.
    /// </summary>
    public class Opcodes
    {
        /// <summary>
        /// Represents a packet sent from the authentication server, to the database server,
        /// requesting that a user authentication request is verified against the database details.
        ///
        /// The client sends the username requesting to login, and an MD5 hash of their password.
        /// The database server responds with an instance of <see cref="Structs.Auth.AuthResponse"/>.
        /// </summary>
        public const int USER_AUTH_REQUEST = 1;

        /// <summary>
        /// Represents a packet sent from the authentication server to the database server,
        /// requesting the server list.
        /// </summary>
        public const int SERVER_LIST = 2;

        /// <summary>
        /// Represents a packet sent from the authentication server, informing the
        /// database server that a session should be deleted
        /// </summary>
        public const int DELETE_SESSION = 3;
    }
}