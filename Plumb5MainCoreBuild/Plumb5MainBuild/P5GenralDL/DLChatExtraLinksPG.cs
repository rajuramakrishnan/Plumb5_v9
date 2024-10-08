﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    public class DLChatExtraLinksPG : CommonDataBaseInteraction, IDLChatExtraLinks
    {
        CommonInfo connection = null;
        public DLChatExtraLinksPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatExtraLinksPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int16> Save(ChatExtraLinks ChatExtraLinks)
        {
            string storeProcCommand = "select chat_extralinks_save(@UserInfoUserId, @LinkType, @LinkUrl, @LinkUrlDescription)";
            object? param = new { ChatExtraLinks.UserInfoUserId, ChatExtraLinks.LinkType, ChatExtraLinks.LinkUrl, ChatExtraLinks.LinkUrlDescription };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param);
        }

        public async Task<bool> Update(ChatExtraLinks ChatExtraLinks)
        {
            string storeProcCommand = "select chat_extralinks_update(@UserInfoUserId, @LinkType, @LinkUrl, @Id, @LinkUrlDescription)";
            object? param = new { ChatExtraLinks.UserInfoUserId, ChatExtraLinks.LinkType, ChatExtraLinks.LinkUrl, ChatExtraLinks.Id, ChatExtraLinks.LinkUrlDescription };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<bool> Delete(Int16 Id)
        {
            string storeProcCommand = "select chat_extralinks_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }

        public async Task<List<ChatExtraLinks>> GET(bool? ToogleStatus = null)
        {
             
                string storeProcCommand = "select *  from chat_extralinks_get(@ToogleStatus)";
                object? param = new { ToogleStatus };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<ChatExtraLinks>(storeProcCommand, param)).ToList();
             
        }

        public async Task<bool> ToogleStatus(ChatExtraLinks ChatExtraLinks)
        {
            string storeProcCommand = "select chat_extralinks_tooglestatus(@Id,@ToogleStatus)";
            object? param = new { ChatExtraLinks.Id, ChatExtraLinks.ToogleStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
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
