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
    public class DLEventImportOverviewSQL : CommonDataBaseInteraction, IDLEventImportOverview
    {
        CommonInfo connection;
        public DLEventImportOverviewSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLEventImportOverviewSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> Save(EventImportOverview eventImportOverview)
        {
            string storeProcCommand = "CustomEvent_ImportOverview";
            object? param = new { Action= "Save", eventImportOverview.UserInfoUserId, eventImportOverview.UserGroupId, eventImportOverview.EventFileName, eventImportOverview.SuccessCount, eventImportOverview.RejectedCount, eventImportOverview.IsCompleted, eventImportOverview.ErrorMessage, eventImportOverview.ImportSource, eventImportOverview.ImportedFileName, eventImportOverview.TotalInputRow, eventImportOverview.TotalCompletedRow };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(EventImportOverview eventImportOverview)
        {
            string storeProcCommand = "CustomEvent_ImportOverview";
            object? param = new { Action = "Update", eventImportOverview.Id, eventImportOverview.SuccessCount, eventImportOverview.RejectedCount, eventImportOverview.ErrorMessage, eventImportOverview.IsCompleted, eventImportOverview.TotalInputRow, eventImportOverview.TotalCompletedRow };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;

        }
        public async Task<EventImportOverview?> GetRunningDetails()
        {
            string storeProcCommand = "CustomEvent_ImportOverview";
            object? param = new { Action= "GetRunningDetails" };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<EventImportOverview>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<EventImportOverview?> GetDetailsToImport()
        {
            string storeProcCommand = "CustomEvent_ImportOverview";
            object? param = new { Action = "GetDetailsToImport" };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<EventImportOverview>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
    }
}
