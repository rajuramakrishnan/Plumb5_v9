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
    public class DLLmsGroupImportRejectDetailsSQL : CommonDataBaseInteraction, IDLLmsGroupImportRejectDetails
    {
        readonly CommonInfo connection;

        public DLLmsGroupImportRejectDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<Int32> Save(LmsGroupImportRejectDetails LmsGroupImportRejectDetails)
        {
            string storeProcCommand = "LmsGroup_ImportRejectDetails";
            object? param = new { Action="Save",LmsGroupImportRejectDetails.LmsGroupImportOverviewId, LmsGroupImportRejectDetails.ContactImportOverviewId, LmsGroupImportRejectDetails.LmsGroupId, LmsGroupImportRejectDetails.EmailId, LmsGroupImportRejectDetails.PhoneNumber, LmsGroupImportRejectDetails.FileRowNumber, LmsGroupImportRejectDetails.RejectedReason, LmsGroupImportRejectDetails.RejectionType };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<LmsGroupImportRejectDetails>> GetList(LmsGroupImportRejectDetails LmsGroupImportRejectDetails, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "LmsGroup_ImportRejectDetails";
            object? param = new { Action = "GetList", LmsGroupImportRejectDetails.LmsGroupImportOverviewId, LmsGroupImportRejectDetails.ContactImportOverviewId, LmsGroupImportRejectDetails.LmsGroupId, LmsGroupImportRejectDetails.RejectionType, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<LmsGroupImportRejectDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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


