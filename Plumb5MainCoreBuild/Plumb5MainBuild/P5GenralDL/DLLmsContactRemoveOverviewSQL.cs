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
    internal class DLLmsContactRemoveOverviewSQL : CommonDataBaseInteraction, IDLLmsContactRemoveOverview
    {
        CommonInfo connection;
        public DLLmsContactRemoveOverviewSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsContactRemoveOverviewSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> Save(LmsContactRemoveOverview contactRemoveOverview)
        {
            string storeProcCommand = "LmsContact_RemoveOverview";
            object? param = new { Action = "Save", contactRemoveOverview.UserInfoUserId, contactRemoveOverview.UserGroupId, contactRemoveOverview.ContactFileName, contactRemoveOverview.SuccessCount, contactRemoveOverview.RejectedCount, contactRemoveOverview.IsCompleted, contactRemoveOverview.ErrorMessage, contactRemoveOverview.ImportedFileName, contactRemoveOverview.TotalInputRow, contactRemoveOverview.TotalCompletedRow, contactRemoveOverview.FileContent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Update(LmsContactRemoveOverview contactImportOverview)
        {
            string storeProcCommand = "LmsContact_RemoveOverview";
            object? param = new { Action = "Update", contactImportOverview.Id, contactImportOverview.SuccessCount, contactImportOverview.RejectedCount, contactImportOverview.ErrorMessage, contactImportOverview.IsCompleted, contactImportOverview.TotalInputRow, contactImportOverview.TotalCompletedRow };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<LmsContactRemoveOverview?> Get(LmsContactRemoveOverview contactImportOverview)
        {
            string storeProcCommand = "LmsContact_RemoveOverview";
            object? param = new { Action = "Get", contactImportOverview.Id, contactImportOverview.UserInfoUserId, contactImportOverview.IsCompleted };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LmsContactRemoveOverview?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<LmsContactRemoveOverview>> GetList(LmsContactRemoveOverview contactImportOverview, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "LmsContact_RemoveOverview";
            object? param = new { Action = "GetList", contactImportOverview.Id, contactImportOverview.UserInfoUserId, contactImportOverview.IsCompleted, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactRemoveOverview>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<LmsContactRemoveOverview>> GetDetailsToImport()
        {
            string storeProcCommand = "LmsContact_RemoveOverview";
            object? param = new { Action = "GetDetailsToImport" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactRemoveOverview>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<LmsContactRemoveOverview>> GetAllDetails(DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext, string UserIdList)
        {
            string storeProcCommand = "LmsContact_RemoveOverview";
            object? param = new { Action = "GetAllDetails", FromDateTime, ToDateTime, OffSet, FetchNext, UserIdList };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactRemoveOverview>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<int> GetAllDetailsMaxCount(DateTime FromDateTime, DateTime ToDateTime, string UserIdList)
        {
            string storeProcCommand = "LmsContact_RemoveOverview";
            object? param = new { Action = "GetAllDetailsCount", FromDateTime, ToDateTime, UserIdList };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

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
