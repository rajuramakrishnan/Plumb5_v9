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
    public class DLWebHookDetailsSQL : CommonDataBaseInteraction, IDLWebHookDetails
    {
        CommonInfo connection;
        public DLWebHookDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebHookDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WebHookDetails webHookDetails)
        {
            string storeProcCommand = "WebHook_Details";
            object? param = new { Action = "Save", webHookDetails.RequestURL, webHookDetails.MethodType, webHookDetails.ContentType, webHookDetails.FieldMappingDetails, webHookDetails.Headers, webHookDetails.BasicAuthentication, webHookDetails.RawBody };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(WebHookDetails webHookDetails)
        {
            string storeProcCommand = "WebHook_Details";
            object? param = new { Action = "Update", webHookDetails.WebHookId, webHookDetails.RequestURL, webHookDetails.MethodType, webHookDetails.ContentType, webHookDetails.FieldMappingDetails, webHookDetails.Headers, webHookDetails.BasicAuthentication, webHookDetails.RawBody };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<WebHookDetails?> GetWebHookDetails(int WebHookId)
        {
            string storeProcCommand = "WebHook_Details";
            object? param = new { Action = "GetWebHookDetails", WebHookId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WebHookDetails?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int WebHookId)
        {
            string storeProcCommand = "WebHook_Details";
            object? param = new { Action = "Delete", WebHookId };

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
