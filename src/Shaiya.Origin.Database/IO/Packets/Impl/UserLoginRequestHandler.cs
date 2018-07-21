﻿using Npgsql;
using Shaiya.Origin.Common.Database.Structs.Login;
using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Common.Serializer;
using Shaiya.Origin.Database.Connector;
using System.Text;

namespace Shaiya.Origin.Database.IO.Packets.Impl
{
    public class UserLoginRequestHandler : PacketHandler
    {
        public override bool Handle(ServerSession session, int length, int opcode, int requestId, byte[] data)
        {
            LoginRequest loginRequest = new LoginRequest();

            loginRequest = Serializer.Deserialize<LoginRequest>(data);

            // The username, password and ip address, as strings
            string username = Encoding.UTF8.GetString(loginRequest.username).TrimEnd('\0');
            string password = Encoding.UTF8.GetString(loginRequest.password).TrimEnd('\0');
            string ipAddress = Encoding.UTF8.GetString(loginRequest.ipAddress);

            Logger.Info("Revieved {0} bytes from the client!", data.Length);

            Logger.Info("Username: {0}", username);
            Logger.Info("Password: {0}", password);
            Logger.Info("Ip: {0}", ipAddress);

            using (NpgsqlConnection connection = new DatabaseConnector().GetConnection("origin_userdata"))
            {
                var cmd = new NpgsqlCommand("validate_login_request", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(":username", username);
                cmd.Parameters.AddWithValue(":password", password);
                cmd.Parameters.AddWithValue(":ip_address", ipAddress);

                connection.Open();

                // Execute the prepared statement
                var reader = cmd.ExecuteReader();

                // Loop through the results
                while (reader.Read())
                {
                    LoginResponse loginResponse = new LoginResponse();

                    int userId = reader.GetInt32(0);
                    int status = reader.GetInt16(1);
                    int privilegeLevel = reader.GetInt16(2);
                    byte[] identityKeys = new byte[16];
                    var bytesRead = reader.GetBytes(3, 0, identityKeys, 0, 16);

                    loginResponse.userId = userId;
                    loginResponse.status = status;
                    loginResponse.privilegeLevel = privilegeLevel;
                    loginResponse.identityKeys = identityKeys;
                    var array = Serializer.Serialize(loginResponse);

                    var bldr = new PacketBuilder(opcode);

                    bldr.WriteInt(requestId);
                    bldr.WriteBytes(array, array.Length);

                    session.Write(bldr.ToPacket());
                }
                reader.Close();
            }
            return true;
        }
    }
}