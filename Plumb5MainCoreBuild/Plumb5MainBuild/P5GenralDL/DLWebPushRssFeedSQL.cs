﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLWebPushRssFeedSQL : CommonDataBaseInteraction, IDLWebPushRssFeed
    {
        private CommonInfo connection;
        public DLWebPushRssFeedSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushRssFeedSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WebPushRssFeed webPushRssFeed)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "Save", webPushRssFeed.UserInfoUserId, webPushRssFeed.CampaignId, webPushRssFeed.CampaignName, webPushRssFeed.RssUrl, webPushRssFeed.GroupId, webPushRssFeed.CheckRssFeedEvery, webPushRssFeed.IsAdvancedOptions, webPushRssFeed.IsAutoHide, webPushRssFeed.IsAndroidBadgeDefaultOrCustom, webPushRssFeed.ImageUrl, webPushRssFeed.UploadedIconFileName, webPushRssFeed.UploadedIconUrl, webPushRssFeed.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(WebPushRssFeed webPushRssFeed)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "Update", webPushRssFeed.Id, webPushRssFeed.UserInfoUserId, webPushRssFeed.CampaignId, webPushRssFeed.CampaignName, webPushRssFeed.RssUrl, webPushRssFeed.GroupId, webPushRssFeed.CheckRssFeedEvery, webPushRssFeed.IsAdvancedOptions, webPushRssFeed.IsAutoHide, webPushRssFeed.IsAndroidBadgeDefaultOrCustom, webPushRssFeed.ImageUrl, webPushRssFeed.UploadedIconFileName, webPushRssFeed.UploadedIconUrl, webPushRssFeed.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> MaxCount(DateTime FromDate, DateTime ToDate, string CampaignName = null)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "MaxCount", CampaignName, FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<WebPushRssFeed>> GetList(int OffSet, int FetchNext, DateTime FromDate, DateTime ToDate, string CampaignName = null)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "GetList", CampaignName, FromDate, ToDate, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebPushRssFeed>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<WebPushRssFeed?> GeDetails(WebPushRssFeed webPushRssFeed)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "GeDetails", webPushRssFeed.Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebPushRssFeed>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        public async Task<List<WebPushRssFeed>> GetFeedList()
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "GetFeedList" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebPushRssFeed>(storeProcCommand, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<bool> UpdatePublishedDate(WebPushRssFeed webPushRssFeed)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "UpdatePublishedDate", webPushRssFeed.Id, webPushRssFeed.RssFeedPublishedDate, webPushRssFeed.Title, webPushRssFeed.Description, webPushRssFeed.RedirectTo };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> UpdateDate(int Id)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "UpdateDate", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> ChangeStatus(int Id, bool Status)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "ChangeStatus", Id, Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> Delete(int Id)
        {
            const string storeProcCommand = "WebPush_RssFeed";
            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
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
