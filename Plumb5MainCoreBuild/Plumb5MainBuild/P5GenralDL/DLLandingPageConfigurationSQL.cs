﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;

namespace P5GenralDL
{
    public class DLLandingPageConfigurationSQL : CommonDataBaseInteraction, IDLLandingPageConfiguration
    {
        CommonInfo connection;
        public DLLandingPageConfigurationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLandingPageConfigurationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(LandingPageConfiguration landingPageConfiguration)
        {
            string storeProcCommand = "LandingPage_Configuration";
            object? param = new { @Action = "Save", landingPageConfiguration.IsLandingPageConfigEnabled, landingPageConfiguration.LandingPageName, landingPageConfiguration.BucketUrl, landingPageConfiguration.CloudFrontUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<LandingPageConfiguration> GetLandingPageConfiguration()
        {
            string storeProcCommand = "LandingPage_Configuration";
            object? param = new { @Action = "Get" };


            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LandingPageConfiguration>(storeProcCommand);
        }
        public async Task<LandingPageConfiguration?> GetConfigByLandingPage(string LandingPageName)
        {
            string storeProcCommand = "LandingPage_Configuration";
            object? param = new { @Action = "GetByLandingPage", LandingPageName };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LandingPageConfiguration?>(storeProcCommand, param);
        }
        public async Task<bool> UpdateStatus(bool IsLandingPageConfigEnabled, string LandingPageName)
        {
            string storeProcCommand = "LandingPage_Configuration";
            object? param = new { @Action = "Update", LandingPageName, IsLandingPageConfigEnabled };

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


