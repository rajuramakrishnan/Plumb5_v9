﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLGoogleImportJobsResponsePG : CommonDataBaseInteraction, IDLGoogleImportJobsResponse
    {
        CommonInfo connection = null;
        public DLGoogleImportJobsResponsePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGoogleImportJobsResponsePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<Int32> Save(GoogleImportJobsResponse googleresponse)
        {
            string storeProcCommand = "select * from googleimportjobsresponse_save(@GoogleImportsettingsId, @ResourceName, @Status)";
            object? param = new { googleresponse.GoogleImportsettingsId, googleresponse.ResourceName, googleresponse.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
        public async Task<int> ResponsesMaxCount(int Googleimportsettingsid, DateTime fromDateTime, DateTime toDateTime)
        {
            string storeProcCommand = "select * from googleimportjobsresponse_count(@Googleimportsettingsid)";
            object? param = new { Googleimportsettingsid };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
        public async Task<List<GoogleImportJobsResponse?>> GetGoogleAdsResponses(int Googleimportsettingsid, DateTime fromDateTime, DateTime toDateTime, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from googleimportjobsresponse_getdetails(@Googleimportsettingsid, @OffSet, @FetchNext )";
            object? param = new { Googleimportsettingsid, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GoogleImportJobsResponse?>(storeProcCommand, param)).ToList();

        }

        public async Task<Int32> Update(int Id, string status)
        {
            string storeProcCommand = "select * from googleimportjobsresponse_update(@Id, @status)";
            object? param = new { Id, status };

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
