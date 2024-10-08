﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLChatLoadCloseResponseSQL : CommonDataBaseInteraction, IDLChatLoadCloseResponse
    {
        CommonInfo connection;
        public DLChatLoadCloseResponseSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<int> SaveUpdateForImpression(int ChatId, string TrackIp, string MachineId, string SessionRefeer, string City, string State, string Country)
        {
            string storeProcCommand = "Chat_LoadCloseResponse";
            object? param = new { @Action = "SaveUpdateForImpression", ChatId, TrackIp, MachineId, SessionRefeer };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public void UpdateFormClose(int ChatId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "Chat_LoadCloseResponse";
            object? param = new { @Action = "UpdateFormClose", ChatId, TrackIp, MachineId, SessionRefeer };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
    }
}
