﻿using DBInteraction;
using P5GenralML;
using System.Data;
using Dapper;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLChatUserSQL : CommonDataBaseInteraction, IDLChatUser
    {
        CommonInfo connection = null;
        public DLChatUserSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLChatUserSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> GetMaxCount(ChatUser chatuser, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "Chat_User";
            object? param = new { @Action = "MaxCount", chatuser.ChatId, chatuser.ContactId, chatuser.IsAdmin, chatuser.IsBlockUser, chatuser.IpAddress, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public void UpdateName(string userId, string name, int contactId = 0)
        {
            string storeProcCommand = "Chat_User";
            object? param = new { @Action = "UpdateName", userId, name, contactId };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(ChatUser chatuser)
        {
            string storeProcCommand = "Chat_User";
            object? param = new { @Action = "Update", chatuser.Id, chatuser.ChatId, chatuser.IpAddress, chatuser.Name, chatuser.ContactId, chatuser.IsAdmin, chatuser.ChatRepeatTime, chatuser.IsBlockUser, chatuser.Comments, chatuser.SoundNotify, chatuser.SoundNewVisitorNotify, chatuser.DesktopNotifyForNewVisitor, chatuser.InstantMesgOption, chatuser.IMNetWork, chatuser.IMEmailId, chatuser.IMVerfication, chatuser.City, chatuser.StateName, chatuser.Country, chatuser.WhoBlocked, chatuser.AgentProfileImageUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<ChatUser>> GetList(ChatUser chatuser, int OffSet, int FetchNext, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "Chat_User";
            object? param = new { @Action = "GetList", chatuser.ChatId, chatuser.ContactId, chatuser.IsAdmin, chatuser.IsBlockUser, chatuser.IpAddress, OffSet, FetchNext, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatUser>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<ChatUser?> Get(ChatUser chatuser)
        {
            string storeProcCommand = "Chat_User";
            object? param = new { @Action = "Get", chatuser.ChatId, chatuser.ContactId, chatuser.IsAdmin, chatuser.Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ChatUser?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
