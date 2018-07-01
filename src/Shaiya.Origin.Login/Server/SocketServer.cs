using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Networking.Server;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Common.Packets;
using Shaiya.Origin.Login.Server.Packets;
using System;

namespace Shaiya.Origin.Login.Server
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

            return server.Bind(port);
        }

        /// <summary>
        /// Called whenever an onConnect event is received by the server.
        /// </summary>
        /// <param name="session">The session instance</param>
        private void OnConnect(ServerSession session)
        {
            Logger.Info("New connection has been established from: {0}", session.GetRemoteAdress());

            var bldr = new PacketBuilder(Opcodes.LOGIN_HANDSHAKE);

            // Write the 3 header bytes
            bldr.WriteByte(0);
            bldr.WriteByte(64);
            bldr.WriteByte(128);

            // The public key array
            byte[] publicKey = new byte[192];

            Random rand = new Random();

            for (int i = 0; i < publicKey.Length; i++)
            {
                bldr.WriteByte((byte)(rand.Next(1, 9)));
            }

            session.Write(bldr.ToPacket());
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

            byte[] temp = new byte[2048];

            Array.Copy(data, 4, temp, 0, data.Length - 4);

            // Trim the packet from ending 0's
            var packetData = Trim(temp);

            PacketHandler handler = _packetManager.GetHandler(packetOpcode);

            // Handle the incoming packet
            return handler.Handle(session, packetLength - 4, packetOpcode, packetData);
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
            // Set the null identity keys
            session.ClearIdentityKeys();
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