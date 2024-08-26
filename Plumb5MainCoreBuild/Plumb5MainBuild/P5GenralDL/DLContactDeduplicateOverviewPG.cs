﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLContactDeduplicateOverviewPG : CommonDataBaseInteraction, IDLContactDeduplicateOverview
    {
        readonly CommonInfo connection;

        public DLContactDeduplicateOverviewPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactDeduplicateOverviewPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> GetMaxCount(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_maxcount(@FromDateTime,@ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }
        public async Task<IEnumerable<ContactDeDuplicateOverView>> GetDetails(int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_getdetails(@OffSet, @FetchNext, @FromDateTime, @ToDateTime)";
            object? param = new { OffSet, FetchNext, FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<ContactDeDuplicateOverView>(storeProcCommand, param);
        }

        public async Task<IEnumerable<ContactDeDuplicateOverView>> GetContactDeduplicateOverviewDetail()
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_getdetailstoverify()";
            object? param = new { };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<ContactDeDuplicateOverView>(storeProcCommand, param);
        }

        public async Task<DataSet> GetVerifiedExistingContactData(DataTable dt)
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_getverifieddata(@dt)";
            object? param = new { dt };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }
        public async Task<DataSet> GetVerifiedUniqueContactData()
        {
            string storeProcCommand = "select * from contactdeduplicateoverviewuniquedata_get()";
            object? param = new { };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<bool> Update(ContactDeDuplicateOverView contactdeduplicateImportOverview)
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_update(@Id, @IsCompleted, @TotalCounts,@TotalCompleted,  @ExistingCounts, @UniqueCounts, @DuplicateCounts, @ExistingFileContent, @UniqueFileContent,@DuplicateFileContent,@ErrorMessage)";
            object? param = new
            {
                contactdeduplicateImportOverview.Id,
                contactdeduplicateImportOverview.IsCompleted,
                contactdeduplicateImportOverview.TotalCounts,
                contactdeduplicateImportOverview.TotalCompleted,
                contactdeduplicateImportOverview.ExistingCounts,
                contactdeduplicateImportOverview.UniqueCounts,
                contactdeduplicateImportOverview.DuplicateCounts,
                contactdeduplicateImportOverview.ExistingFileContent,
                contactdeduplicateImportOverview.UniqueFileContent,
                contactdeduplicateImportOverview.DuplicateFileContent,
                contactdeduplicateImportOverview.ErrorMessage
            };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param) > 0;
        }

        public async Task<ContactDeDuplicateOverView?> GetFileContentToDownload(int Id, string ContactFileType)
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_getfilecontenttodownload(@Id,@ContactFileType)";
            object? param = new { Id, ContactFileType };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ContactDeDuplicateOverView?>(storeProcCommand, param);
        }
        public async Task<Int32> Save(int UserInfoUserId, string ImportedFileName, byte[] ImportedFileBinaryData)
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_save(@UserInfoUserId, @ImportedFileName, @ImportedFileBinaryData )";
            object? param = new { UserInfoUserId, ImportedFileName, ImportedFileBinaryData };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }
        public async Task<ContactDeDuplicateOverView?> Get(ContactDeDuplicateOverView contactdeduplicateImportOverview)
        {
            string storeProcCommand = "select * from contactdeduplicate_overview_get(@Id,@IsCompleted)";
            object? param = new { contactdeduplicateImportOverview.Id, contactdeduplicateImportOverview.IsCompleted };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ContactDeDuplicateOverView?>(storeProcCommand, param);
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
