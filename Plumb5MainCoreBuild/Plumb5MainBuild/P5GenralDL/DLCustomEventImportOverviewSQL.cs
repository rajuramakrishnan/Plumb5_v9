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
    public class DLCustomEventImportOverviewSQL : CommonDataBaseInteraction, IDLCustomEventImportOverview
    {
        CommonInfo connection = null;
        public DLCustomEventImportOverviewSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLCustomEventImportOverviewSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(CustomEventImportOverview customEventImportOverview)
        {
            string storeProcCommand = "CustomEvent_ImportOverview";

            object? param = new
            {   Action= "Save",
                customEventImportOverview.UserInfoUserId,
                customEventImportOverview.UserGroupId,
                customEventImportOverview.EventFileName,
                customEventImportOverview.SuccessCount,
                customEventImportOverview.RejectedCount,
                customEventImportOverview.IsCompleted,
                customEventImportOverview.ErrorMessage,
                customEventImportOverview.ImportSource,
                customEventImportOverview.ImportedFileName,
                customEventImportOverview.TotalInputRow,
                customEventImportOverview.TotalCompletedRow
            };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<Int32> MaxCount(CustomEventImportOverview customEventImportOverview)
        {
            string storeProcCommand = "CustomEvent_ImportOverview";
            object? param = new { Action = "MaxCount", customEventImportOverview.ImportedFileName };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CustomEventImportOverview>> GetReportData(CustomEventImportOverview customEventImportOverview, int OffSet, int FetchNext)
        {
            string storeProcCommand = "CustomEvent_ImportOverview";

            object? param = new { Action = "GetReportData", customEventImportOverview.ImportedFileName, OffSet, FetchNext };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<CustomEventImportOverview>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
