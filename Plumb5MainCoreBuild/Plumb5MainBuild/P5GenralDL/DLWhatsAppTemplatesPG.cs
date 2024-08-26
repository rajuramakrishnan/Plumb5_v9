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
    public class DLWhatsAppTemplatesPG : CommonDataBaseInteraction, IDLWhatsAppTemplates
    {
        CommonInfo connection;

        public DLWhatsAppTemplatesPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<int> GetMaxCount(WhatsAppTemplates whatsAppTemplate)
        {
            string storeProcCommand = "select whatsapp_templates_maxcount(@Name)";
            object? param = new { whatsAppTemplate.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<MLWhatsAppTemplates>> GetList(WhatsAppTemplates whatsAppTemplate, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from whatsapp_templates_getlist(@Name, @OffSet, @FetchNext)";
            object? param = new { whatsAppTemplate.Name, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppTemplates>(storeProcCommand, param)).ToList();
        }

        public async Task<Int32> Save(WhatsAppTemplates whatsAppTemplate)
        {
            string storeProcCommand = "select whatsapp_templates_savetemplate(@UserInfoUserId,@UserGroupId,@WhatsAppCampaignId,@Name,@TemplateDescription,@TemplateType,@WhitelistedTemplateName,@TemplateContent,@TemplateLanguage,@UserAttributes,@IsButtonAdded,@ButtonOneAction,@ButtonOneText,@ButtonOneType,@ButtonOneURLType,@ButtonOneDynamicURLSuffix,@ButtonTwoAction,@ButtonTwoText,@ButtonTwoType,@ButtonTwoURLType,@ButtonTwoDynamicURLSuffix,@MediaFileURL,@TemplateFooter,@ConvertLinkToShortenUrl)";
            object? param = new
            {
                whatsAppTemplate.UserInfoUserId,
                whatsAppTemplate.UserGroupId,
                whatsAppTemplate.WhatsAppCampaignId,
                whatsAppTemplate.Name,
                whatsAppTemplate.TemplateDescription,
                whatsAppTemplate.TemplateType,
                whatsAppTemplate.WhitelistedTemplateName,
                whatsAppTemplate.TemplateContent,
                whatsAppTemplate.TemplateLanguage,
                whatsAppTemplate.UserAttributes,
                whatsAppTemplate.IsButtonAdded,
                whatsAppTemplate.ButtonOneAction,
                whatsAppTemplate.ButtonOneText,
                whatsAppTemplate.ButtonOneType,
                whatsAppTemplate.ButtonOneURLType,
                whatsAppTemplate.ButtonOneDynamicURLSuffix,
                whatsAppTemplate.ButtonTwoAction,
                whatsAppTemplate.ButtonTwoText,
                whatsAppTemplate.ButtonTwoType,
                whatsAppTemplate.ButtonTwoURLType,
                whatsAppTemplate.ButtonTwoDynamicURLSuffix,
                whatsAppTemplate.MediaFileURL,
                whatsAppTemplate.TemplateFooter,
                whatsAppTemplate.ConvertLinkToShortenUrl
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<List<WhatsAppTemplates>> GetAllTemplate(int UserInfoUserId = 0, List<int> UserInfoUserIdList = null, int? IsSuperAdmin = null)
        {
            string storeProcCommand = "select * from whatsapp_templates_getalltemplate()";

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppTemplates>(storeProcCommand)).ToList();
        }
        public async Task<List<WhatsAppTemplates>> GetAllTemplate(IEnumerable<int> TemplateList)
        {
            string storeProcCommand = "select * from whatsapp_templates_get(@TemplateList)";
            object? param = new { TemplateList = string.Join(",", TemplateList) };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppTemplates>(storeProcCommand, param)).ToList();
        }

        public async Task<List<WhatsAppTemplates>> GetTemplateDetails(WhatsAppTemplates whatsappTemplate, int OffSet = 0, int FetchNext = 0)
        {
            string storeProcCommand = "select * from whatsapp_template_get(@Name, @WhatsAppCampaignId, @OffSet, @FetchNext)";
            object? param = new { whatsappTemplate.Name, whatsappTemplate.WhatsAppCampaignId, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppTemplates>(storeProcCommand, param)).ToList();
        }

        public async Task<WhatsAppTemplates?> GetSingle(int Id)
        {
            string storeProcCommand = "select * from whatsapp_templates_getsingle(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppTemplates>(storeProcCommand, param);
        }

        public async Task<bool> Update(WhatsAppTemplates whatsAppTemplate)
        {
            string storeProcCommand = "select whatsapp_templates_updatetemplate(@Id,@UserInfoUserId,@UserGroupId,@WhatsAppCampaignId,@Name,@TemplateDescription,@TemplateType,@WhitelistedTemplateName,@TemplateContent,@TemplateLanguage,@UserAttributes,@IsButtonAdded,@ButtonOneAction,@ButtonOneText,@ButtonOneType,@ButtonOneURLType,@ButtonOneDynamicURLSuffix,@ButtonTwoAction,@ButtonTwoText,@ButtonTwoType,@ButtonTwoURLType,@ButtonTwoDynamicURLSuffix,@MediaFileURL,@TemplateFooter,@ConvertLinkToShortenUrl)";

            object? param = new
            {
                whatsAppTemplate.Id,
                whatsAppTemplate.UserInfoUserId,
                whatsAppTemplate.UserGroupId,
                whatsAppTemplate.WhatsAppCampaignId,
                whatsAppTemplate.Name,
                whatsAppTemplate.TemplateDescription,
                whatsAppTemplate.TemplateType,
                whatsAppTemplate.WhitelistedTemplateName,
                whatsAppTemplate.TemplateContent,
                whatsAppTemplate.TemplateLanguage,
                whatsAppTemplate.UserAttributes,
                whatsAppTemplate.IsButtonAdded,
                whatsAppTemplate.ButtonOneAction,
                whatsAppTemplate.ButtonOneText,
                whatsAppTemplate.ButtonOneType,
                whatsAppTemplate.ButtonOneURLType,
                whatsAppTemplate.ButtonOneDynamicURLSuffix,
                whatsAppTemplate.ButtonTwoAction,
                whatsAppTemplate.ButtonTwoText,
                whatsAppTemplate.ButtonTwoType,
                whatsAppTemplate.ButtonTwoURLType,
                whatsAppTemplate.ButtonTwoDynamicURLSuffix,
                whatsAppTemplate.MediaFileURL,
                whatsAppTemplate.TemplateFooter,
                whatsAppTemplate.ConvertLinkToShortenUrl
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "select whatsapp_templates_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<WhatsAppTemplates?> GetDetails(int WhatsAppTemplateId)
        {
            string storeProcCommand = "select * from whatsapp_templates_gettemplatebyid(@WhatsAppTemplateId)";
            object? param = new { WhatsAppTemplateId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppTemplates>(storeProcCommand, param);
        }
        public async Task<WhatsAppTemplates?> GetTemplateArchive(string Name)
        {
            string storeProcCommand = "select * from whatsapp_templates_gettemplatearchive(@Name)";
            object? param = new { Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppTemplates>(storeProcCommand, param);
        }

        public async Task<bool> UpdateTemplateStatus(int TemplateId)
        {
            string storeProcCommand = "select whatsapp_templates_updatetemplatestatus(@TemplateId)";
            object? param = new { TemplateId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<int> GetArchiveMaxCount(WhatsAppTemplates whatsAppTemplate)
        {
            string storeProcCommand = "select whatsapp_archivetemplates_maxcount(@Name)";
            object? param = new { whatsAppTemplate.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<List<MLWhatsAppTemplates>> GetArchiveReport(WhatsAppTemplates whatsAppTemplate, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from whatsapp_archivetemplates_getlist(@Name, @OffSet, @FetchNext)";
            object? param = new { whatsAppTemplate.Name, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppTemplates>(storeProcCommand, param)).ToList();
        }
        public async Task<bool> UnArchive(int Id)
        {
            string storeProcCommand = "select whatsapp_templates_unarchive(@Id)";
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
