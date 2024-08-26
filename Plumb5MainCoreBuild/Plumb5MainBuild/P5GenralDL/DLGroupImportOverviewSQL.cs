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
    public class DLGroupImportOverviewSQL : CommonDataBaseInteraction, IDLGroupImportOverview
    {
        readonly CommonInfo connection;

        public DLGroupImportOverviewSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGroupImportOverviewSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(GroupImportOverview groupImportOverview)
        {
            string storeProcCommand = "Group_ImportOverview";
            object? param = new { Action = "Save", groupImportOverview.ContactImportOverviewId, groupImportOverview.GroupId, groupImportOverview.SuccessCount, groupImportOverview.RejectedCount, groupImportOverview.ContactErrorRejectedCount };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(GroupImportOverview groupImportOverview)
        {
            string storeProcCommand = "Group_ImportOverview";
            object? param = new { Action = "Update", groupImportOverview.ContactImportOverviewId, groupImportOverview.GroupId, groupImportOverview.SuccessCount, groupImportOverview.RejectedCount, groupImportOverview.ContactErrorRejectedCount };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<GroupImportOverview?> Get(GroupImportOverview groupImportOverview)
        {
            string storeProcCommand = "Group_ImportOverview";
            object? param = new { Action = "Get", groupImportOverview.ContactImportOverviewId, groupImportOverview.GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GroupImportOverview?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<GroupImportOverview>> GetList(GroupImportOverview groupImportOverview, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "Group_ImportOverview";
            object? param = new { Action = "GetList", groupImportOverview.ContactImportOverviewId, groupImportOverview.GroupId, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GroupImportOverview>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
