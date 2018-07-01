using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Server;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Database.Server.Packets;
using System;

namespace Shaiya.Origin.Database.Server
{
    public class SocketServer
    {
        private readonly PacketManager _packetManager = new PacketManager();

        // Initialise the IoServer instance, and listen on a specified address and port
        public bool Initialise(int port)
        {
            var server = new OriginServer(30820);

            server.OnConnect(OnConnect);
            server.OnRecieve(OnRecieve);

            return server.Bind(port);
        }

        /// <summary>
        /// Called whenever an onConnect event is received by the server.
        /// </summary>
        /// <param name="session">The session instance</param>
        private void OnConnect(ServerSession session)
        {
            // The list of allowed host
            const string host = "127.0.0.1";

            // If the host matches
            bool matches = session.GetRemoteAdress() == host;

            if (!matches)
            {
                Logger.Error("Connection denied from address: {0}. Not whitelisted!", session.GetRemoteAdress());
                session.Close();
                return;
            }

            // If the host does match
            Logger.Info("Accepted connection from address: {0}", session.GetRemoteAdress());
        }

        /// <summary>
        /// Called whenever an onReceive event is received by the server, which signifies
        /// an incoming packet from an existing connection.
        /// </summary>
        /// <param name="session">The connection's session</param>
        /// <param name="data"></param>
        /// <param name="bytesRead"></param>
        private bool OnRecieve(ServerSession session, byte[] data, int bytesRead)
        {
            int packetLength = ((data[0] & 0xFF) + ((data[1] & 0xFF) << 8));
            int packetOpcode = ((data[2] & 0xFF) + ((data[3] & 0xFF) << 8));

            // The request id
            int requestId = ((data[4] & 0xFF) + ((data[5] & 0xFF) << 8) + ((data[6] & 0xFF) << 16) + ((data[7] & 0xFF) << 24));

            byte[] temp = new byte[2048];

            Array.Copy(data, 8, temp, 0, data.Length - 8);

            // Trim the packet from ending 0's
            var packetData = Trim(temp);

            var handler = _packetManager.GetHandler(packetOpcode);

            // Handle the incoming packet
            return handler.Handle(session, packetLength - 8, packetOpcode, requestId, packetData);
        }

        /// <summary>
        /// Called whenever an onSend event is received by the server, which signifies
        /// an outgoing packet from an existing connection.
        /// </summary>
        /// <param name="name"></param>
        private void OnSend(byte[] name)
        {
            Console.WriteLine("sending, ", name);
        }

        /// <summary>
        /// Called whenever an onTerminate event is received by the server, which signifies
        /// an existing connection that has had it's connection terminated.
        /// </summary>
        /// <param name="session">The connection's session</param>
        private void OnTerminate(ServerSession session)
        {
        }

        public byte[] Trim(byte[] packet)
        {
            var i = packet.Length - 1;
            while (packet[i] == 0)
            {
                --i;
            }
            var temp = new byte[i + 1];
            Array.Copy(packet, temp, i + 1);
            return temp;
        }
    }
}