using Shaiya.Origin.Common.Networking.Server;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Game.Server.Packets;
using System;

namespace Shaiya.Origin.Game.Server
{
    public class SocketServer
    {
        private readonly PacketManager _packetManager = new PacketManager();

        // Initialise the SocketServer instance, and listen on a specified address and port
        public bool Initialise(int port)
        {
            var server = new OriginServer();

            server.OnConnect(OnConnect);
            server.OnRecieve(OnRecieve);
            server.OnTerminate(OnTerminate);

            return server.Bind(port);
        }

        /// <summary>
        /// Called whenever an OnConnect event is received by the server.
        /// </summary>
        /// <param name="session">The session instance</param>
        private void OnConnect(ServerSession session)
        {
        }

        /// <summary>
        /// Called whenever an OnReceive event is received by the server, which signifies
        /// an incoming packet from an existing connection.
        /// </summary>
        /// <param name="session">The connection's session</param>
        /// <param name="data"></param>
        /// <param name="bytesRead"></param>
        private bool OnRecieve(ServerSession session, byte[] data, int bytesRead)
        {
            int packetLength = ((data[0] & 0xFF) + ((data[1] & 0xFF) << 8));
            int packetOpcode = ((data[2] & 0xFF) + ((data[3] & 0xFF) << 8));

            byte[] packetData = new byte[packetLength - 4];

            Array.Copy(data, 4, packetData, 0, packetLength - 4);

            var handler = _packetManager.GetHandler(packetOpcode);

            // Handle the incoming packet
            return handler.Handle(session, packetLength - 4, packetOpcode, packetData);
        }

        /// <summary>
        /// Called whenever an OnSend event is received by the server, which signifies
        /// an outgoing packet from an existing connection.
        /// </summary>
        /// <param name="name"></param>
        private void OnSend(byte[] name)
        {
            Console.WriteLine("sending, ", name);
        }

        /// <summary>
        /// Called whenever an OnTerminate event is received by the server, which signifies
        /// an existing connection that has had it's connection terminated.
        /// </summary>
        /// <param name="session">The connection's session</param>
        private void OnTerminate(ServerSession session)
        {
        }
    }
}