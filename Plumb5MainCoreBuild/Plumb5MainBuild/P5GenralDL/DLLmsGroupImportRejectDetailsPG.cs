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
    public class DLLmsGroupImportRejectDetailsPG : CommonDataBaseInteraction, IDLLmsGroupImportRejectDetails
    {
        readonly CommonInfo connection;

        public DLLmsGroupImportRejectDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<Int32> Save(LmsGroupImportRejectDetails LmsGroupImportRejectDetails)
        {
            string storeProcCommand = "select lmsgroup_importrejectdetails_save(@LmsGroupImportOverviewId, @ContactImportOverviewId, @LmsGroupId, @EmailId, @PhoneNumber, @FileRowNumber, @RejectedReason, @RejectionType)";
            object? param = new { LmsGroupImportRejectDetails.LmsGroupImportOverviewId, LmsGroupImportRejectDetails.ContactImportOverviewId, LmsGroupImportRejectDetails.LmsGroupId, LmsGroupImportRejectDetails.EmailId, LmsGroupImportRejectDetails.PhoneNumber, LmsGroupImportRejectDetails.FileRowNumber, LmsGroupImportRejectDetails.RejectedReason, LmsGroupImportRejectDetails.RejectionType };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }


        public async Task<IEnumerable<LmsGroupImportRejectDetails>>  GetList(LmsGroupImportRejectDetails LmsGroupImportRejectDetails, DateTime? FromDateTime, DateTime? ToDateTime)
        {
            string storeProcCommand = "select * from lmsgroup_importrejectdetails_getlist(@LmsGroupImportOverviewId, @ContactImportOverviewId, @LmsGroupId, @RejectionType, @FromDateTime, @ToDateTime )";
            object? param = new { LmsGroupImportRejectDetails.LmsGroupImportOverviewId, LmsGroupImportRejectDetails.ContactImportOverviewId, LmsGroupImportRejectDetails.LmsGroupId, LmsGroupImportRejectDetails.RejectionType, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<LmsGroupImportRejectDetails>(storeProcCommand, param);
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

