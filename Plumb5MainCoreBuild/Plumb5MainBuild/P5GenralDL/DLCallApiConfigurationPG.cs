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
    public class DLCallApiConfigurationPG : CommonDataBaseInteraction, IDLCallApiConfiguration
    {
        CommonInfo connection;
        public DLCallApiConfigurationPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLCallApiConfigurationPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int16> Save(MLCallApiConfiguration callApiConfiguration)
        {
            string storeProcCommand = "select * from callapi_configuration_save(@UserInfoUserId, @ProviderName, @ConfigurationUrl, @ApiKey, @AccountName, @CallerId, @ApiToken, @SubDomain, @CountryCode)";
            object? param = new { callApiConfiguration.UserInfoUserId, callApiConfiguration.ProviderName, callApiConfiguration.ConfigurationUrl, callApiConfiguration.ApiKey, callApiConfiguration.AccountName, callApiConfiguration.CallerId, callApiConfiguration.ApiToken, callApiConfiguration.SubDomain, callApiConfiguration.CountryCode };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param);

        }

        public async Task<bool> Update(MLCallApiConfiguration callApiConfiguration)
        {
            string storeProcCommand = "select * from callapi_configuration_update(@Id,@UserInfoUserId, @ProviderName, @ConfigurationUrl, @ApiKey, @AccountName, @CallerId, @ApiToken, @SubDomain, @CountryCode)";
            object? param = new { callApiConfiguration.Id, callApiConfiguration.UserInfoUserId, callApiConfiguration.ProviderName, callApiConfiguration.ConfigurationUrl, callApiConfiguration.ApiKey, callApiConfiguration.AccountName, callApiConfiguration.CallerId, callApiConfiguration.ApiToken, callApiConfiguration.SubDomain, callApiConfiguration.CountryCode };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<MLCallApiConfiguration?> GetCallConfigurationDetails(string ProviderName = null)
        {
            string storeProcCommand = "select * from callapi_configuration_get(@ProviderName)";
            object? param = new { ProviderName };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLCallApiConfiguration>(storeProcCommand, param);

        }

        public async Task<bool> ToogleStatus(MLCallApiConfiguration callApiConfiguration)
        {
            string storeProcCommand = "select * from callapi_configuration_activestatus(@Id, @ActiveStatus, @ProviderName)";
            object? param = new { callApiConfiguration.Id, callApiConfiguration.ActiveStatus, callApiConfiguration.ProviderName };

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
