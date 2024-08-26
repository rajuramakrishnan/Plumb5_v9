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
    public class DLWhatsappTemplateUrlSQL : CommonDataBaseInteraction, IDLWhatsappTemplateUrl
    {
        CommonInfo connection = null;
        public DLWhatsappTemplateUrlSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsappTemplateUrlSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> SaveWhatsappTemplateUrl(WhatsAppTemplateUrl TemplateUrls)
        {
            string storeProcCommand = "Whatsapp_TemplateUrl";
            object? param = new { Action = "Save", TemplateUrls.UrlContent, TemplateUrls.WhatsAppTemplatesId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<WhatsAppTemplateUrl>> GetDetail(int WhatsAppTemplateId)
        {
            string storeProcCommand = "Whatsapp_TemplateUrl";
            object? param = new { Action = "GetDetailsByTemplateId", WhatsAppTemplateId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppTemplateUrl>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<WhatsAppTemplateUrl?> GetDetailByUrl(string Url)
        {
            string storeProcCommand = "Whatsapp_TemplateUrl";
            object? param = new { Action = "GET", Url };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppTemplateUrl>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetUrlByIdUrl(int WhatsAppTemplatesId, string UrlContent)
        {
            string storeProcCommand = "Whatsapp_TemplateUrl";
            object? param = new { Action = "GetTemplateUrlByIdUrl", WhatsAppTemplatesId, UrlContent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> Update(WhatsAppTemplateUrl TemplateUrls)
        {
            string storeProcCommand = "Whatsapp_TemplateUrl";
            object? param = new { Action = "Update", TemplateUrls.Id, TemplateUrls.UrlContent, TemplateUrls.WhatsAppTemplatesId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<string?> GetUrlAsync(int smsTemplateUrlId)
        {
            string storeProcCommand = "Whatsapp_TemplateUrl";
            object? param = new { Action = "GetTemplateUrlById", smsTemplateUrlId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<string>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> Delete(WhatsAppTemplateUrl TemplateUrls)
        {
            string storeProcCommand = "Whatsapp_TemplateUrl";
            object? param = new { Action = "Delete", TemplateUrls.Id, TemplateUrls.WhatsAppTemplatesId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
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
