﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWhatsAppNotificationTemplateSQL : CommonDataBaseInteraction, IDLWhatsAppNotificationTemplate
    {
        CommonInfo connection;
        public DLWhatsAppNotificationTemplateSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsAppNotificationTemplateSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<WhatsAppNotificationTemplate?> GetByIdentifier(string Identifier)
        {
            string storeProcCommand = "WhatsAppNotification_Template";
            object? param = new { Action = "GetByIdentifier", Identifier };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppNotificationTemplate>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<int> GetMaxCount()
        {
            string storeProcCommand = "WhatsAppNotification_Template";
            object? param = new { Action = "MaxCount" };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<WhatsAppNotificationTemplate>> Get(int OffSet, int FetchNext)
        {
            string storeProcCommand = "WhatsAppNotification_Template";
            object? param = new { Action = "Get", OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppNotificationTemplate>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<WhatsAppNotificationTemplate?> GetById(int Id)
        {
            string storeProcCommand = "WhatsAppNotification_Template";
            object? param = new { Action = "GetById", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppNotificationTemplate>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> UpdateStatus(bool IsWhatsAppNotificationEnabled)
        {
            string storeProcCommand = "WhatsAppNotification_Template";
            object? param = new { Action = "UpdateStatus", IsWhatsAppNotificationEnabled };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> Update(WhatsAppNotificationTemplate notificationTemplate)
        {
            string storeProcCommand = "WhatsAppNotification_Template";
            object? param = new { Action = "Update", notificationTemplate.Id, notificationTemplate.WhiteListedTemplateName, notificationTemplate.TemplateName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

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
