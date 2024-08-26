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
    internal class DLGoogleImportJobsResponseSQL : CommonDataBaseInteraction, IDLGoogleImportJobsResponse
    {
        CommonInfo connection = null;
        public DLGoogleImportJobsResponseSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGoogleImportJobsResponseSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(GoogleImportJobsResponse googleresponse)
        {
            string storeProcCommand = "Google_ImportJobsResponse";
            object? param = new { Action = "Save", googleresponse.GoogleImportsettingsId, googleresponse.ResourceName, googleresponse.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
        public async Task<int> ResponsesMaxCount(int Googleimportsettingsid, DateTime fromDateTime, DateTime toDateTime)
        {
            string storeProcCommand = "Google_ImportJobsResponse";
            object? param = new { Action = "GetCount", Googleimportsettingsid };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
        public async Task<List<GoogleImportJobsResponse?>> GetGoogleAdsResponses(int Googleimportsettingsid, DateTime fromDateTime, DateTime toDateTime, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Google_ImportJobsResponse";
            object? param = new { Action = "Get", Googleimportsettingsid, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleImportJobsResponse?>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<Int32> Update(int Id, string status)
        {
            string storeProcCommand = "Google_ImportJobsResponse";
            object? param = new {Action= "Update", Id, status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

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
