﻿using Npgsql;
using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Database.Connector;
using System;

namespace Shaiya.Origin.Database.Server.Packets.Impl
{
    public class DeleteSessionRequestHandler : PacketHandler
    {
        public override bool Handle(ServerSession session, int length, int opcode, int requestId, byte[] data)
        {
            byte[] sessionKey = new byte[16];

            Array.Copy(data, sessionKey, 16);

            try
            {
                using (NpgsqlConnection connection = new DatabaseConnector().GetConnection("origin_userdata"))
                {
                    var cmd = new NpgsqlCommand("DELETE FROM sessions where identity_keys = @identity_keys", connection);
                    cmd.Parameters.AddWithValue("@identity_keys", sessionKey);

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }

            return true;
        }
    }
}