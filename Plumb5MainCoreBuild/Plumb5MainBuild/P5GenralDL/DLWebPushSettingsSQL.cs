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
    public class DLWebPushSettingsSQL : CommonDataBaseInteraction, IDLWebPushSettings
    {
        CommonInfo connection;
        public DLWebPushSettingsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushSettingsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(WebPushSettings webpushSettings)
        {
            const string storeProcCommand = "WebPush_Settings";
            object? param = new { Action = "Save", webpushSettings.ProviderName, webpushSettings.FCMProjectNo, webpushSettings.FCMApiKey, webpushSettings.Status, webpushSettings.VapidPublicKey, webpushSettings.VapidPrivateKey, webpushSettings.VapidSubject };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<WebPushSettings?> GetWebPushSettings()
        {
            const string storeProcCommand = "select * from webpush_settings_getwebpushsettings()";
            object? param = new { Action = "GetWebPushSettings" };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebPushSettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        #region Dispose Method
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }
}
