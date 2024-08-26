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
    internal class DLLmsContactRemoveOverviewPG : CommonDataBaseInteraction, IDLLmsContactRemoveOverview
    {
        CommonInfo connection;
        public DLLmsContactRemoveOverviewPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsContactRemoveOverviewPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(LmsContactRemoveOverview contactRemoveOverview)
        {
            string storeProcCommand = "select * from lmscontact_removeoverview_save(@UserInfoUserId, @UserGroupId, @ContactFileName, @SuccessCount, @RejectedCount, @IsCompleted, @ErrorMessage, @ImportedFileName, @TotalInputRow, @TotalCompletedRow, @FileContent)";
            object? param = new { contactRemoveOverview.UserInfoUserId, contactRemoveOverview.UserGroupId, contactRemoveOverview.ContactFileName, contactRemoveOverview.SuccessCount, contactRemoveOverview.RejectedCount, contactRemoveOverview.IsCompleted, contactRemoveOverview.ErrorMessage, contactRemoveOverview.ImportedFileName, contactRemoveOverview.TotalInputRow, contactRemoveOverview.TotalCompletedRow, contactRemoveOverview.FileContent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Update(LmsContactRemoveOverview contactImportOverview)
        {
            string storeProcCommand = "select * from lmscontact_removeoverview_update(@ FromDateTime, @ToDateTime, @UserIdList)";
            object? param = new { contactImportOverview.Id, contactImportOverview.SuccessCount, contactImportOverview.RejectedCount, contactImportOverview.ErrorMessage, contactImportOverview.IsCompleted, contactImportOverview.TotalInputRow, contactImportOverview.TotalCompletedRow };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<LmsContactRemoveOverview?> Get(LmsContactRemoveOverview contactImportOverview)
        {
            string storeProcCommand = "select * from lmscontact_removeoverview_get(@Id, @UserInfoUserId, @IsCompleted)";
            object? param = new { contactImportOverview.Id, contactImportOverview.UserInfoUserId, contactImportOverview.IsCompleted };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<LmsContactRemoveOverview?>(storeProcCommand, param);

        }

        public async Task<List<LmsContactRemoveOverview>> GetList(LmsContactRemoveOverview contactImportOverview, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "select * from LmsContact_RemoveOverview(@action,@Id, @UserInfoUserId, @IsCompleted, FromDateTime, ToDateTime)";
            object? param = new { Action = "GetList", contactImportOverview.Id, contactImportOverview.UserInfoUserId, contactImportOverview.IsCompleted, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactRemoveOverview>(storeProcCommand, param)).ToList();

        }

        public async Task<List<LmsContactRemoveOverview>> GetDetailsToImport()
        {
            string storeProcCommand = "select * from lmscontact_removeoverview_getdetailstoimport()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactRemoveOverview>(storeProcCommand, param)).ToList();

        }

        public async Task<List<LmsContactRemoveOverview>> GetAllDetails(DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext, string UserIdList)
        {
            string storeProcCommand = "select * from lmscontact_removeoverview_getalldetails(@FromDateTime, @ToDateTime, @OffSet, @FetchNext, @UserIdList)";
            object? param = new { FromDateTime, ToDateTime, OffSet, FetchNext, UserIdList };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactRemoveOverview>(storeProcCommand, param)).ToList();

        }

        public async Task<int> GetAllDetailsMaxCount(DateTime FromDateTime, DateTime ToDateTime, string UserIdList)
        {
            string storeProcCommand = "select * from lmscontact_removeoverview_getalldetailscount(@FromDateTime, @ToDateTime, @UserIdList)";
            object? param = new { FromDateTime, ToDateTime, UserIdList };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

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
