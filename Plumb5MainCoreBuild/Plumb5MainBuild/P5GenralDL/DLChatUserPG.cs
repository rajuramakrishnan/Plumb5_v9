﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;

namespace P5GenralDL
{
    public class DLChatUserPG : CommonDataBaseInteraction, IDLChatUser
    {
        CommonInfo connection = null;
        public DLChatUserPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLChatUserPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> GetMaxCount(ChatUser chatuser, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "select * from chat_user_maxcount(@ChatId, @ContactId, @IsAdmin, @IsBlockUser, @IpAddress, @FromDateTime, @ToDateTime)";
            object? param = new { chatuser.ChatId, chatuser.ContactId, chatuser.IsAdmin, chatuser.IsBlockUser, chatuser.IpAddress, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public void UpdateName(string userId, string name, int contactId = 0)
        {
            string storeProcCommand = "select chat_user_updatename(@UserId, @Name, @ContactId)";
            object? param = new { userId, name, contactId };

            using var db = GetDbConnection(connection.Connection);
            db.ExecuteScalarAsync<Int16>(storeProcCommand, param);
        }

        public async Task<bool> Update(ChatUser chatuser)
        {
            string storeProcCommand = "select chat_user_update(@Id, @ChatId, @IpAddress, @Name, @ContactId, @IsAdmin, @ChatRepeatTime, @IsBlockUser, @Comments, @SoundNotify, @SoundNewVisitorNotify, @DesktopNotifyForNewVisitor, @InstantMesgOption, @IMNetWork, @IMEmailId, @IMVerfication, @City, @StateName, @Country, @WhoBlocked, @AgentProfileImageUrl)";
            object? param = new { chatuser.Id, chatuser.ChatId, chatuser.IpAddress, chatuser.Name, chatuser.ContactId, chatuser.IsAdmin, chatuser.ChatRepeatTime, chatuser.IsBlockUser, chatuser.Comments, chatuser.SoundNotify, chatuser.SoundNewVisitorNotify, chatuser.DesktopNotifyForNewVisitor, chatuser.InstantMesgOption, chatuser.IMNetWork, chatuser.IMEmailId, chatuser.IMVerfication, chatuser.City, chatuser.StateName, chatuser.Country, chatuser.WhoBlocked, chatuser.AgentProfileImageUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<List<ChatUser>> GetList(ChatUser chatuser, int OffSet, int FetchNext, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "select *  from chat_user_getlist(@ChatId, @ContactId, @IsAdmin, @IsBlockUser, @IpAddress, OffSet, FetchNext, FromDateTime, ToDateTime)";
            object? param = new { chatuser.ChatId, chatuser.ContactId, chatuser.IsAdmin, chatuser.IsBlockUser, chatuser.IpAddress, OffSet, FetchNext, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatUser>(storeProcCommand, param)).ToList();
        }

        public async Task<ChatUser?> Get(ChatUser chatuser)
        {
            string storeProcCommand = "select *  from chat_user_get(ChatId, @ContactId, @IsAdmin, @Id)";
            object? param = new { chatuser.ChatId, chatuser.ContactId, chatuser.IsAdmin, chatuser.Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ChatUser?>(storeProcCommand, param);
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
