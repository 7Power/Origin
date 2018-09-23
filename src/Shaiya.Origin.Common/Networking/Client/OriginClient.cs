using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Shaiya.Origin.Common.Networking.Client
{
    public class OriginClient
    {
        // The socket instance
        private Socket _socket;

        // The array that the client will receive data to
        private byte[] data = new byte[1024];

        // The queue of pending callbacks
        private Queue<Action> callbackQueue = new Queue<Action>();

        // A dictionary containing the request ids, and their callbacks
        private Dictionary<int, Action<byte[], int>> requestDictionary = new Dictionary<int, Action<byte[], int>>();

        private static readonly object _syncObject = new object();

        private IPAddress _ipAddress;
        private int _port;

        // ManualResetEvent instances signal completion.

        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);

        public OriginClient(IPAddress ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;

            // Create a TCP/IP socket.
            _socket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public bool Connect()
        {
            IPEndPoint remoteEP = new IPEndPoint(_ipAddress, _port);

            // Attempt to catch any exceptions thrown
            try
            {
                // Connect to the server
                _socket.Connect(remoteEP);

                // The callback processing thread
                Thread callbackProcess = new Thread(new ThreadStart(ProcessCallbacks));

                // Start the thread
                callbackProcess.Start();

                // Begin reading some data
                _socket.BeginReceive(data, 0, data.Length, 0, new AsyncCallback(HandleRead), _socket);

                // Return if the socket is connected
                return _socket.Connected;
            }
            // Catch an exception
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return false;
            }
        }

        private void HandleRead(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.
            _socket = (Socket)ar.AsyncState;

            // Read data from the client socket.
            int bytesRead = _socket.EndReceive(ar);

            // The packet length
            int packetLength = ((data[0] & 0xFF) + ((data[1] & 0xFF) << 8));

            // The packet opcode
            int packetOpcode = ((data[2] & 0xFF) + ((data[3] & 0xFF) << 8));

            // The request id
            int requestId = ((data[4] & 0xFF) + ((data[5] & 0xFF) << 8) + ((data[6] & 0xFF) << 16) + ((data[7] & 0xFF) << 24));

            byte[] packetData = new byte[packetLength - 8];

            Array.Copy(data, 8, packetData, 0, packetLength - 8);

            lock (_syncObject)
            {
                // Call the recieve function
                if (requestDictionary.ContainsKey(requestId))
                {
                    // Define the callback
                    var callback = requestDictionary[requestId];

                    // Remove the request
                    requestDictionary.Remove(requestId);

                    // Add the callback to the queue
                    callbackQueue.Enqueue(() => { callback(packetData, packetLength - 8); });
                }
            }

            // Begin reading some data
            _socket.BeginReceive(data, 0, data.Length, 0, new AsyncCallback(HandleRead), _socket);
        }

        /// <summary>
        /// Writes a packet to this session's socket
        /// </summary>
        /// <param name="packet">The packet instance to write</param>
        public void Write(Packet packet)
        {
            // The length of the packet
            int length = packet.length + 4;

            // The array to write to
            byte[] data = new byte[length];

            Random rand = new Random();

            // The request id
            int requestId = rand.Next();

            // Write the length
            data[0] = (byte)length;
            data[1] = (byte)(length >> 8);

            // Write the opcode
            data[2] = (byte)packet.opcode;
            data[3] = (byte)(packet.opcode >> 8);

            // Write the request id
            data[4] = (byte)requestId;
            data[5] = (byte)(requestId >> 8);
            data[6] = (byte)(requestId >> 16);
            data[7] = (byte)(requestId >> 24);

            // Write the data
            for (int i = 0; i < packet.payload.Count; i++)
            {
                data[8 + i] = packet.payload.ElementAt(i);
            }

            // Send the data
            _socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallBack), _socket);
        }

        /// <summary>
        /// Writes a packet to this session's socket
        /// </summary>
        /// <param name="packet">The packet instance to write</param>
        /// <param name="callback">The callback to execute</param>
        public void Write(Packet packet, Action<byte[], int> callback)
        {
            // The length of the packet
            int length = packet.length + 4;

            // The array to write to
            byte[] data = new byte[length];

            Random rand = new Random();

            // The request id
            int requestId = rand.Next();

            // Store the request
            requestDictionary[requestId] = callback;

            // Write the length
            data[0] = (byte)length;
            data[1] = (byte)(length >> 8);

            // Write the opcode
            data[2] = (byte)packet.opcode;
            data[3] = (byte)(packet.opcode >> 8);

            // Write the request id
            data[4] = (byte)requestId;
            data[5] = (byte)(requestId >> 8);
            data[6] = (byte)(requestId >> 16);
            data[7] = (byte)(requestId >> 24);

            // Write the data
            for (int i = 0; i < packet.payload.Count; i++)
            {
                data[8 + i] = packet.payload.ElementAt(i);
            }

            // Send the data
            _socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallBack), _socket);
        }

        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                _socket = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = _socket.EndSend(ar);
                Logger.Info("Sent {0} bytes to server.", bytesSent - 4);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }

        public void ProcessCallbacks()
        {
            // While the client is running
            while (true)
            {
                lock (_syncObject)
                {
                    // If there are tasks to be processed
                    if (callbackQueue.Any())
                    {
                        // Get the callback and remove it
                        var callback = callbackQueue.Dequeue();

                        // Execute the task
                        callback();
                    }
                }
            }
        }
    }
}