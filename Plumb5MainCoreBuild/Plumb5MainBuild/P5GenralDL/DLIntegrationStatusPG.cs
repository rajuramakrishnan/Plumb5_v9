﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLIntegrationStatusPG : CommonDataBaseInteraction, IDLIntegrationStatus
    {
        CommonInfo connection = null;
        public DLIntegrationStatusPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLIntegrationStatusPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> GetWebTracking()
        {
            string storeProcCommand = "select integration_status_webtracking()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> GetEventTracking()
        {
            string storeProcCommand = "select integration_status_eventtracking()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<int> GetEmailSetup()
        {
            string storeProcCommand = "select integration_status_emailsetup()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<int> GetSiteSearch()
        {
            string storeProcCommand = "select integration_status_sitesearch()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<int> GetEmailVerification()
        {
            string storeProcCommand = "select integration_status_emailverification()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> GetSpamTester()
        {
            string storeProcCommand = "select integration_status_eventtracking()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<int> GetSmsSetup()
        {
            string storeProcCommand = "select integration_status_smssetup()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<WebPushSubscriptionSetting> GetWebPushTracking()
        {
            string storeProcCommand = "select * from integration_status_webpushtracking()";
            object? param = new {  };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebPushSubscriptionSetting>(storeProcCommand, param);
        }
        public async Task<int> GetMobileSdkTracking()
        {
            string storeProcCommand = "select integration_status_mobilesdktracking()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<int> GetClickToCallSetup()
        {
            string storeProcCommand = "select integration_status_clicktocallsetup()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> GetWhatsAppSetup()
        {
            string storeProcCommand = "select integration_status_whatsappsetup()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
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

