﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;

namespace P5GenralDL
{
    public class DLChatLoadCloseResponsePG : CommonDataBaseInteraction, IDLChatLoadCloseResponse
    {
        CommonInfo connection;
        public DLChatLoadCloseResponsePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<int> SaveUpdateForImpression(int ChatId, string TrackIp, string MachineId, string SessionRefeer, string City, string State, string Country)
        {
            string storeProcCommand = "select * from chat_loadcloseresponse_saveupdateforimpression(@ChatId, @TrackIp, @MachineId, @SessionRefeer, @City, @State, @Country)";
            object? param = new { ChatId, TrackIp, MachineId, SessionRefeer, City, State, Country };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public void UpdateFormClose(int ChatId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "select * from chat_loadcloseresponse_updateformclose(@ChatId, @TrackIp,@MachineId, @SessionRefeer)";
            object? param = new { ChatId, TrackIp, MachineId, SessionRefeer };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
    }
}
