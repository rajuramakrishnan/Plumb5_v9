﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;

namespace P5GenralDL
{
    public class DLChatRoomPG : CommonDataBaseInteraction, IDLChatRoom
    {
        CommonInfo connection = null;
        public DLChatRoomPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatRoomPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> BlockParticularUser(int chatId, string ChatUserId, int UserId)
        {
            string storeProcCommand = "select chat_userdetails_blockuser(@ChatId, ChatUserId, UserId)";
            object? param = new { chatId, ChatUserId, UserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> UpdateNote(int chatId, string UserId, string comments)
        {
            string storeProcCommand = "select chat_userdetails_updatenote(@ChatId,  @UserId, @Comments)";
            object? param = new { chatId, UserId, comments };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> UpdateContactId(string UserId, int ContactId, string UtmTagSource = null)
        {
            string storeProcCommand = "select chat_userdetails_updatecontactid(@UserId, @ContactId, @UtmTagSource)";
            object? param = new { UserId, ContactId, UtmTagSource };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<bool> CityAndNames(MLChatRoom chatRoom)
        {
            string storeProcCommand = "select chat_userdetails_cityandname(@ChatId, @UserId, @Name, @City)";
            object? param = new { chatRoom.ChatId, chatRoom.UserId, chatRoom.Name, chatRoom.City };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }
        public async Task<bool> DesktopNotification(MLChatRoom chatRoom)
        {
            string storeProcCommand = "select chat_userdetails_activedesktopnotifyagent(@ChatId, @UserId, @DesktopNotifyForNewVisitor)";
            object? param = new { chatRoom.ChatId, chatRoom.UserId, chatRoom.DesktopNotifyForNewVisitor };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<bool> SoundNotify(MLChatRoom chatRoom)
        {
            string storeProcCommand = "select chat_userdetails_activesoundnotifyagent(@ChatId, @UserId, @SoundNotify, @SoundNewVisitorNotify, @SoundNotificationOnVisitorConnect)";
            object? param = new { chatRoom.ChatId, chatRoom.UserId, chatRoom.SoundNotify, chatRoom.SoundNewVisitorNotify, chatRoom.SoundNotificationOnVisitorConnect };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<MLChatRoom?> GetAgentData(MLChatRoom chatRoom)
        {
            string storeProcCommand = "select *  from chat_userdetails_getagentdata(@ChatId, @UserId, @Name)";
            object? param = new { chatRoom.ChatId, chatRoom.UserId, chatRoom.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLChatRoom?>(storeProcCommand, param);
        }

        public async Task<List<ChatFullTranscipt>> GetTranscriptAdmin(int chatId, string UserId)
        {
            string storeProcCommand = "select *  from chat_userdetails_fulltranscript(@ChatId,  @UserId)";
            object? param = new { chatId, UserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatFullTranscipt>(storeProcCommand, param)).ToList();
        }

        public async Task<List<ChatFullTranscipt>> GetPastChat(int chatId, string UserId)
        {
            string storeProcCommand = "select *  from chat_userdetails_pastchatdetails(@ChatId,  @UserId)";
            object? param = new { chatId, UserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatFullTranscipt>(storeProcCommand, param)).ToList();
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

