﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWebPushTemplatePG : CommonDataBaseInteraction, IDLWebPushTemplate
    {
        CommonInfo connection;

        public DLWebPushTemplatePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushTemplatePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(WebPushTemplate webpushTemplate)
        {
            const string storeProcCommand = "select * from webpush_template_save(@UserInfoUserId, @CampaignId, @TemplateName, @TemplateDescription, @NotificationType, @Title, @MessageContent, @IconImage, @OnClickRedirect, @BannerImage, @Button1_Label, @Button1_Redirect, @Button2_Label, @Button2_Redirect, @IsAutoHide, @IsCustomBadge, @BadgeImage)";
            object? param = new { webpushTemplate.UserInfoUserId, webpushTemplate.CampaignId, webpushTemplate.TemplateName, webpushTemplate.TemplateDescription, webpushTemplate.NotificationType, webpushTemplate.Title, webpushTemplate.MessageContent, webpushTemplate.IconImage, webpushTemplate.OnClickRedirect, webpushTemplate.BannerImage, webpushTemplate.Button1_Label, webpushTemplate.Button1_Redirect, webpushTemplate.Button2_Label, webpushTemplate.Button2_Redirect, webpushTemplate.IsAutoHide, webpushTemplate.IsCustomBadge, webpushTemplate.BadgeImage };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Update(WebPushTemplate webpushTemplate)
        {
            const string storeProcCommand = "select * from webpush_template_Update(@Id, @CampaignId, @TemplateName, @TemplateDescription, @NotificationType, @Title, @MessageContent, @IconImage, @OnClickRedirect, @BannerImage, @Button1_Label, @Button1_Redirect, @Button2_Label, @Button2_Redirect, @IsAutoHide, @IsCustomBadge, @BadgeImage)";
            object? param = new { webpushTemplate.Id, webpushTemplate.CampaignId, webpushTemplate.TemplateName, webpushTemplate.TemplateDescription, webpushTemplate.NotificationType, webpushTemplate.Title, webpushTemplate.MessageContent, webpushTemplate.IconImage, webpushTemplate.OnClickRedirect, webpushTemplate.BannerImage, webpushTemplate.Button1_Label, webpushTemplate.Button1_Redirect, webpushTemplate.Button2_Label, webpushTemplate.Button2_Redirect, webpushTemplate.IsAutoHide, webpushTemplate.IsCustomBadge, webpushTemplate.BadgeImage };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> GetMaxCount(WebPushTemplate webpushTemplate)
        {
            const string storeProcCommand = "select * from webpush_template_maxcount(@Id, @TemplateName, @CampaignId)";
            object? param = new { webpushTemplate.Id, webpushTemplate.TemplateName, webpushTemplate.CampaignId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> GetArchiveMaxCount(WebPushTemplate webpushTemplate)
        {
            try
            {
                const string storeProcCommand = "select * from webpush_template_archivemaxcount(@Id, @TemplateName, @CampaignId)";
                object? param = new { webpushTemplate.Id, webpushTemplate.TemplateName, webpushTemplate.CampaignId };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<WebPushTemplate>> GetAllTemplates(WebPushTemplate webpushTemplate, int OffSet = -1, int FetchNext = 0)
        {
            try
            {
                const string storeProcCommand = "select * from webpush_template_get(@TemplateName, @CampaignId, @OffSet, @FetchNext)";
                object? param = new { webpushTemplate.TemplateName, webpushTemplate.CampaignId, OffSet, FetchNext };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<WebPushTemplate>(storeProcCommand, param)).ToList();
            }
            catch (Exception ex)
            {
                return new List<WebPushTemplate>();
            }
        }

        public async Task<List<WebPushTemplate>> GetAllArchiveTemplates(WebPushTemplate webpushTemplate, int OffSet = -1, int FetchNext = 0)
        {
            try
            {
                const string storeProcCommand = "select * from webpush_template_getarchive(@TemplateName, @CampaignId, @OffSet, @FetchNext)";
                object? param = new { webpushTemplate.TemplateName, webpushTemplate.CampaignId, OffSet, FetchNext };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<WebPushTemplate>(storeProcCommand, param)).ToList();
            }
            catch (Exception ex)
            {
                return new List<WebPushTemplate>();
            }
        }

        public async Task<WebPushTemplate?> GetDetails(WebPushTemplate webpushTemplate)
        {
            const string storeProcCommand = "select* from webpush_templateid_get(@Id)";
            object? param = new { webpushTemplate.Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebPushTemplate>(storeProcCommand, param);
        }

        public WebPushTemplate? GetDetailsSync(WebPushTemplate webpushTemplate)
        {
            const string storeProcCommand = "select * from webpush_templateid_get(@Id)";
            object? param = new { webpushTemplate.Id };

            using var db = GetDbConnection(connection.Connection);
            return db.QueryFirstOrDefault<WebPushTemplate>(storeProcCommand, param);
        }

        public async Task<WebPushTemplate?> GetDetailsByName(string Name)
        {
            const string storeProcCommand = "select * from webpush_template_gettemplate(@Name)";
            object? param = new { Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebPushTemplate>(storeProcCommand, param);
        }

        public async Task<WebPushTemplate?> GetArchiveTemplate(string Name)
        {
            const string storeProcCommand = "select * from webpush_template_getarchivetemplate(@Name)";
            object? param = new { Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebPushTemplate>(storeProcCommand, param);
        }

        public async Task<bool> UpdateArchiveStatus(int Id)
        {
            const string storeProcCommand = "select * from webpush_template_updatearchivestatus(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }


        public async Task<bool> Delete(int Id)
        {
            const string storeProcCommand = "select * from webpush_template_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> RestoreTemplate(int Id)
        {
            const string storeProcCommand = "select * from webpush_template_restore(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
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
