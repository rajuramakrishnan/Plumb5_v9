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
    public class DLGroupImportRejectDetailsSQL : CommonDataBaseInteraction, IDLGroupImportRejectDetails
    {
        readonly CommonInfo connection;

        public DLGroupImportRejectDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGroupImportRejectDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(GroupImportRejectDetails GroupImportRejectDetails)
        {
            string storeProcCommand = "Group_ImportRejectDetails";
            object? param = new { Action = "Save", GroupImportRejectDetails.GroupImportOverviewId, GroupImportRejectDetails.ContactImportOverviewId, GroupImportRejectDetails.GroupId, GroupImportRejectDetails.EmailId, GroupImportRejectDetails.PhoneNumber, GroupImportRejectDetails.FileRowNumber, GroupImportRejectDetails.RejectedReason, GroupImportRejectDetails.RejectionType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<GroupImportRejectDetails?> Get(GroupImportRejectDetails GroupImportRejectDetails)
        {
            string storeProcCommand = "Group_ImportRejectDetails";
            object? param = new { Action = "Get", GroupImportRejectDetails.GroupImportOverviewId, GroupImportRejectDetails.ContactImportOverviewId, GroupImportRejectDetails.GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GroupImportRejectDetails?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<GroupImportRejectDetails>> GetList(GroupImportRejectDetails GroupImportRejectDetails, DateTime? FromDateTime = null, DateTime? ToDateTime = null)
        {
            string storeProcCommand = "Group_ImportRejectDetails";
            object? param = new { Action = "GetList", GroupImportRejectDetails.GroupImportOverviewId, GroupImportRejectDetails.ContactImportOverviewId, GroupImportRejectDetails.GroupId, GroupImportRejectDetails.RejectionType, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GroupImportRejectDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
