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
    public class DLChatInteractionSQL : CommonDataBaseInteraction, IDLChatInteraction
    {
        readonly CommonInfo connection;
        public DLChatInteractionSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatInteractionSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public void InsertMessage(int chatId, string userId, string chatRoom, string message, string sessionRefer)
        {
            string storeProcCommand = "Chat_RoomMesg";
            object? param = new { @Action= "Chat_RoomMesg", chatId, userId, chatRoom, message, sessionRefer };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public void UpdateDetails(int chatId, string userId, string userIdName, string name, string emailId, string message, string phoneNumber, string SessionRefeer)
        {
            string storeProcCommand = "Chat_RoomMesg";
            object? param = new { @Action = "Chat_RoomMesg", chatId, userId, userIdName, message, SessionRefeer };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<ChatVisitorDetails?> GetVisitorDetails(int chatId, string userId, string sessionRefer)
        {
            string storeProcCommand = "Chat_UserDetails";
            object? param = new { Action = "GetChatVisitor", chatId, userId, sessionRefer };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ChatVisitorDetails?>(storeProcCommand, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<ChatFullTranscipt>> GetTodayMessage(int chatId, string userId)
        {
            string storeProcCommand = "Chat_UserDetails";
            object? param = new { Action = "TodayMesg", chatId, userId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatFullTranscipt>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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

