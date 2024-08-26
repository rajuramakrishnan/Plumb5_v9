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
    public class DLChatBannerSyncPG : CommonDataBaseInteraction, IDLChatBannerSync
    {
        CommonInfo connection = null;
        public DLChatBannerSyncPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatBannerSyncPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(ChatBanner chatbanner)
        {
            string storeProcCommand = "select chat_banners_save(@BannerContent, @RedirectUrl, @UserInfoUserId, @BannerTitle)";
            object? param = new { chatbanner.BannerContent, chatbanner.RedirectUrl, chatbanner.UserInfoUserId, chatbanner.BannerTitle };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Update(ChatBanner chatbanner)
        {
            string storeProcCommand = "select chat_banners_update(@Id, @RedirectUrl, @BannerTitle)";
            object? param = new { chatbanner.Id, chatbanner.RedirectUrl, chatbanner.BannerTitle };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "select chat_banners_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<List<ChatBanner>> GetList(int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from chat_banners_getlist(@OffSet, @FetchNext)";
            object? param = new { OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ChatBanner>(storeProcCommand, param)).ToList();
        }

        #region Dispose Method

        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connection = null;
                }
            }
            //dispose unmanaged ressources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }
}
