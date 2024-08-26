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
    internal class DLCallApiConfigurationSQL : CommonDataBaseInteraction, IDLCallApiConfiguration
    {
        CommonInfo connection;
        public DLCallApiConfigurationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLCallApiConfigurationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int16> Save(MLCallApiConfiguration callApiConfiguration)
        {
            string storeProcCommand = "CallApi_Configuration";
            object? param = new { Action = "Save", callApiConfiguration.UserInfoUserId, callApiConfiguration.ProviderName, callApiConfiguration.ConfigurationUrl, callApiConfiguration.ApiKey, callApiConfiguration.AccountName, callApiConfiguration.CallerId, callApiConfiguration.CountryCode, callApiConfiguration.ApiToken, callApiConfiguration.SubDomain };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(MLCallApiConfiguration callApiConfiguration)
        {
            string storeProcCommand = "CallApi_Configuration";
            object? param = new { Action = "Update", callApiConfiguration.Id, callApiConfiguration.UserInfoUserId, callApiConfiguration.ProviderName, callApiConfiguration.ConfigurationUrl, callApiConfiguration.ApiKey, callApiConfiguration.AccountName, callApiConfiguration.CallerId, callApiConfiguration.CountryCode, callApiConfiguration.ApiToken, callApiConfiguration.SubDomain };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<MLCallApiConfiguration?> GetCallConfigurationDetails(string ProviderName)
        {
            string storeProcCommand = "SelectVisitorAutoSuggest";
            object? param = new { Action = "GET", ProviderName };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLCallApiConfiguration>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> ToogleStatus(MLCallApiConfiguration callApiConfiguration)
        {
            string storeProcCommand = "CallApi_Configuration";
            object? param = new { Action = "ActiveStatus", callApiConfiguration.Id, callApiConfiguration.ActiveStatus, callApiConfiguration.ProviderName };

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
