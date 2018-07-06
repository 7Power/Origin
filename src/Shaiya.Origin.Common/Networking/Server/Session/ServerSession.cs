using Shaiya.Origin.Common.Networking.Packets;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Shaiya.Origin.Common.Networking.Server.Session
{
    /// <summary>
    /// Represents a session that is currently connected to a server instance.
    /// </summary>
    public class ServerSession
    {
        // The socket connection instance
        private Socket _socket;

        // The session index (used by the game server)
        private int _index;

        // The identity keys for this session
        private byte[] _identitykeys = new byte[16];

        // Size of the data
        public const int dataSize = 1024;

        // The array that the session will receive data to
        public byte[] data = new byte[dataSize];

        public ServerSession(Socket socket)
        {
            _socket = socket;
        }

        /// <summary>
        /// Gets the socket instance used by this session
        /// </summary>
        /// <returns>The socket</returns>
        public Socket GetSocket()
        {
            return _socket;
        }

        /// <summary>
        /// Gets the remote address that this session is connecting from
        /// </summary>
        /// <returns>The remote address</returns>
        public string GetRemoteAdress()
        {
            return ((IPEndPoint)_socket.RemoteEndPoint).Address.ToString();
        }

        /// <summary>
        /// Closes the socket instance
        /// </summary>
        public void Close()
        {
            if (GetSocket().Connected)
            {
                GetSocket().Close();
            }
        }

        /// <summary>
        /// Writes a packet to this session's socket
        /// </summary>
        /// <param name="packet">The packet instance to write</param>
        public void Write(Packet packet)
        {
            // The array to write to
            byte[] data = new byte[packet.length];

            // Write the length
            data[0] = (byte)packet.length;
            data[1] = (byte)(packet.length >> 8);

            // Write the opcode
            data[2] = (byte)packet.opcode;
            data[3] = (byte)(packet.opcode >> 8);

            // Write the data
            for (int i = 0; i < packet.payload.Count; i++)
            {
                data[4 + i] = packet.payload.ElementAt(i);
            }

            _socket.Send(data);
        }

        /// <summary>
        /// Sets the identity keys for this session
        /// </summary>
        /// <param name="keys">The keys</param>
        public void SetIdentityKeys(byte[] keys)
        {
            for (int i = 0; i < _identitykeys.Length; i++)
            {
                _identitykeys[i] = keys[i];
            }
        }

        /// <summary>
        /// Clear the identity keys for this session
        /// </summary>
        public void ClearIdentityKeys()
        {
            for (int i = 0; i < _identitykeys.Length; i++)
            {
                _identitykeys[i] = 0;
            }
        }

        /// <summary>
        /// Gets the identity keys for this session
        /// </summary>
        /// <returns>This session's identity keys</returns>
        public byte[] GetIdentityKeys()
        {
            return _identitykeys;
        }

        /// <summary>
        /// Gets the packet buffer for this session
        /// </summary>
        /// <returns>This session's buffer</returns>
        private byte[] GetBuffer()
        {
            return data;
        }

        /// <summary>
        /// Sets the game player index for this session
        /// </summary>
        /// <param name="index">The index</param>
        public void SetGameIndex(int index)
        {
            _index = index;
        }

        /// <summary>
        /// Gets the index of the game player
        /// </summary>
        public int GetGameIndex()
        {
            return _index;
        }
    }
}