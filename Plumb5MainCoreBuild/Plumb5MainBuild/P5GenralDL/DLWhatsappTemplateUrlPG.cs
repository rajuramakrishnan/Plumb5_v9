﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP5GenralDL;
using Dapper;

namespace P5GenralDL
{
    public class DLWhatsappTemplateUrlPG : CommonDataBaseInteraction, IDLWhatsappTemplateUrl
    {
        CommonInfo connection = null;
        public DLWhatsappTemplateUrlPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsappTemplateUrlPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> SaveWhatsappTemplateUrl(WhatsAppTemplateUrl TemplateUrls)
        {
            string storeProcCommand = "select whatsapp_templateurl_save(@UrlContent,@WhatsAppTemplatesId)";
            object? param = new { TemplateUrls.UrlContent, TemplateUrls.WhatsAppTemplatesId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<List<WhatsAppTemplateUrl>> GetDetail(int WhatsAppTemplateId)
        {
            string storeProcCommand = "select * from whatsapp_templateurl_getdetailsbytemplateid(@WhatsAppTemplateId)";
            object? param = new { WhatsAppTemplateId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppTemplateUrl>(storeProcCommand, param)).ToList();
        }

        public async Task<WhatsAppTemplateUrl?> GetDetailByUrl(string Url)
        {
            string storeProcCommand = "select * from whatsapp_templateurl_get(@Url)";
            object? param = new { Url };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppTemplateUrl>(storeProcCommand, param);
        }

        public async Task<int> GetUrlByIdUrl(int WhatsAppTemplatesId, string UrlContent)
        {
            string storeProcCommand = "select whatsapp_templateurl_gettemplateurlbyidurl(@WhatsAppTemplatesId, @UrlContent)";
            object? param = new { WhatsAppTemplatesId, UrlContent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<bool> Update(WhatsAppTemplateUrl TemplateUrls)
        {
            string storeProcCommand = "select whatsapp_templateurl_update(@Id, @UrlContent, @WhatsAppTemplatesId)";
            object? param = new { TemplateUrls.Id, TemplateUrls.UrlContent, TemplateUrls.WhatsAppTemplatesId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<string?> GetUrlAsync(int smsTemplateUrlId)
        {
            string storeProcCommand = "select whatsapp_templateurl_gettemplateurlbyid(@smsTemplateUrlId)";
            object? param = new { smsTemplateUrlId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<string>(storeProcCommand, param);
        }
        public async Task<bool> Delete(WhatsAppTemplateUrl TemplateUrls)
        {
            string storeProcCommand = "select whatsapp_templateurl_delete(@Id,@WhatsAppTemplatesId)";
            object? param = new { TemplateUrls.Id, TemplateUrls.WhatsAppTemplatesId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
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
                    connection = null;
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
