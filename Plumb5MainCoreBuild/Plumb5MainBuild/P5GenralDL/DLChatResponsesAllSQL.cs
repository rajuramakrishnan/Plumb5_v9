﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;
using Dapper;

namespace P5GenralDL
{
    public class DLChatResponsesAllSQL : CommonDataBaseInteraction, IDLChatResponsesAll
    {
        CommonInfo connection = null;
        public DLChatResponsesAllSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatResponsesAllSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> GetCountOfSelecCamp(int ChatId, string IpAddress, string SearchContent, int MinChatRepeatTime, int MaxChatRepeatTime, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "Chat_ReportSP_AllResponses";
            object? param = new { @Action= "AllChatCount", ChatId, IpAddress, SearchContent, FromDateTime, ToDateTime, MinChatRepeatTime, MaxChatRepeatTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<ChatAllResponses>> AllChat(int ChatId, string IpAddress, string SearchContent, int MinChatRepeatTime, int MaxChatRepeatTime, int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "Chat_ReportSP_AllResponses";
            object? param = new { @Action = "AllChatResp", ChatId, IpAddress, SearchContent, OffSet, FetchNext, FromDateTime, ToDateTime, MinChatRepeatTime, MaxChatRepeatTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatAllResponses>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<ChatAllResponsesForExport>> GetAllChatToExport(int ChatId, string IpAddress, string SearchContent, int MinChatRepeatTime, int MaxChatRepeatTime, int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "Chat_ReportSP_AllResponses";
            object? param = new { @Action = "GetAllChatToExport", ChatId, IpAddress, SearchContent, OffSet, FetchNext, FromDateTime, ToDateTime, MinChatRepeatTime, MaxChatRepeatTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatAllResponsesForExport>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        #region Dispose Method
        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion End of Dispose Method
    }
}


