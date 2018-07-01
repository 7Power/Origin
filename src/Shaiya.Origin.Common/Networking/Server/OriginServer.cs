using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Server.Session;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Shaiya.Origin.Common.Networking.Server
{
    public class OriginServer
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private readonly Socket _listener;
        private static Func<ServerSession, byte[], int, bool> _recieveFunction;
        private Action<ServerSession> _connectFunction;
        private Action<ServerSession> _terminateFunction;
        private IPAddress _ipAddress = Dns.GetHostEntry("localhost").AddressList[1];

        public OriginServer()
        {
            _listener = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Binds the OriginServer instance to a specified port, and begin receiving incoming
        /// network events
        /// </summary>
        /// <param name="port">The port to listen on</param>
        /// <returns>If the server successfully bound to a socket</returns>
        public bool Bind(int port)
        {
            // Attempt to catch any exceptions thrown
            try
            {
                IPEndPoint localEndPoint = new IPEndPoint(_ipAddress, port);
                _listener.Bind(localEndPoint);
                // Begin listening for incoming connections
                _listener.Listen(100);
                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    Logger.Info("Waiting for a connection...");

                    // Begin accepting incoming connections
                    _listener.BeginAccept(
                        new AsyncCallback(HandleAccept),
                        _listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Handles the accepting of an incoming connection, and the session creation.
        /// </summary>
        /// <param name="ar"></param>
        public void HandleAccept(IAsyncResult ar)
        {
            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the session.
            ServerSession session = new ServerSession(handler);

            // Call the connect function
            _connectFunction(session);

            // Signal the main thread to continue.
            allDone.Set();

            handler.BeginReceive(session.data, 0, ServerSession.dataSize, 0,
                new AsyncCallback(HandleRead), session);
        }

        public void HandleRead(IAsyncResult ar)
        {
            // Retrive the session from the asynchronous session
            ServerSession session = (ServerSession)ar.AsyncState;

            // Read data from the client socket
            int bytesRead = session.GetSocket().EndReceive(ar);

            if (bytesRead == 0)
            {
                _terminateFunction(session);
            }

            if (bytesRead > 0)
            {
                // Pass the data to the recieve function
                if (_recieveFunction(session, session.data, bytesRead))
                {
                    // Begin reading incoming packets
                    session.GetSocket().BeginReceive(session.data, 0, ServerSession.dataSize, 0,
                        new AsyncCallback(HandleRead), session);
                }
            }
        }

        /// <summary>
        /// Defines the function to be called whenever a new connection is made
        /// </summary>
        /// <param name="connectFunction">The function to call</param>
        public void OnConnect(Action<ServerSession> connectFunction)
        {
            _connectFunction = connectFunction;
        }

        /// <summary>
        /// Defines the function to be called whenever a new packet is received
        /// </summary>
        /// <param name="recieveFunction">The function to call</param>
        /// <returns></returns>
        public void OnRecieve(Func<ServerSession, byte[], int, bool> recieveFunction)
        {
            _recieveFunction = recieveFunction;
        }

        /// <summary>
        /// Defines the function to be called whenever a connection is terminated
        /// </summary>
        /// <param name="terminateFunction">The function to call</param>
        public void OnTerminate(Action<ServerSession> terminateFunction)
        {
            _terminateFunction = terminateFunction;
        }
    }
}