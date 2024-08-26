﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;

namespace P5GenralDL
{
    public class DLChatLoadCloseResponseCountPG : CommonDataBaseInteraction, IDLChatLoadCloseResponseCount
    {
        CommonInfo connection;
        public DLChatLoadCloseResponseCountPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<int> SaveUpdateForImpression(int ChatId)
        {
            string storeProcCommand = "select * from chat_loadcloseresponsecount_saveupdateforimpression(@ChatId)";
            object? param = new { ChatId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public void UpdateFormClose(int ChatId)
        {
            string storeProcCommand = "select * from chat_loadcloseresponsecount_updateformclose(@ChatId)";
            object? param = new { ChatId };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
    }
}
