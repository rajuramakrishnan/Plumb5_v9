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
    public class DLCustomEventImportOverviewPG : CommonDataBaseInteraction, IDLCustomEventImportOverview
    {
        CommonInfo connection = null;
        public DLCustomEventImportOverviewPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLCustomEventImportOverviewPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(CustomEventImportOverview customEventImportOverview)
        {
            string storeProcCommand = "select customevent_importoverview_save(@UserInfoUserId, @UserGroupId, @EventFileName,  @SuccessCount, @RejectedCount, @IsCompleted, @ErrorMessage, @ImportSource, @ImportedFileName,  @TotalInputRow, @TotalCompletedRow)";

            object? param = new {
                customEventImportOverview.UserInfoUserId, customEventImportOverview.UserGroupId, customEventImportOverview.EventFileName,
                customEventImportOverview.SuccessCount, customEventImportOverview.RejectedCount, customEventImportOverview.IsCompleted,
                customEventImportOverview.ErrorMessage, customEventImportOverview.ImportSource, customEventImportOverview.ImportedFileName,
                customEventImportOverview.TotalInputRow, customEventImportOverview.TotalCompletedRow };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<Int32> MaxCount(CustomEventImportOverview customEventImportOverview)
        {
            string storeProcCommand = "select customevent_importoverview_maxcount(@ImportedFileName)"; 
            object? param = new { customEventImportOverview.ImportedFileName };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<CustomEventImportOverview>>  GetReportData(CustomEventImportOverview customEventImportOverview, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from customevent_importoverview_getreportdata(@ImportedFileName, @OffSet, @FetchNext)";
            
            object? param = new { customEventImportOverview.ImportedFileName, OffSet, FetchNext };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<CustomEventImportOverview>(storeProcCommand, param);
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
