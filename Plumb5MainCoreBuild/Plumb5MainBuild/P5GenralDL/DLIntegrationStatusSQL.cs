﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLIntegrationStatusSQL : CommonDataBaseInteraction, IDLIntegrationStatus
    {
        CommonInfo connection = null;
        public DLIntegrationStatusSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLIntegrationStatusSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> GetWebTracking()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetEventTracking()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetEmailSetup()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetSiteSearch()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetEmailVerification()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetSpamTester()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetSmsSetup()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<WebPushSubscriptionSetting> GetWebPushTracking()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebPushSubscriptionSetting>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetMobileSdkTracking()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetClickToCallSetup()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetWhatsAppSetup()
        {
            string storeProcCommand = "Integration_Status";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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

