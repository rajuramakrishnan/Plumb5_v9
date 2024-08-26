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
    public class DLLmsContactArchiveRejectedDetailsPG : CommonDataBaseInteraction, IDLLmsContactArchiveRejectedDetails
    {
        CommonInfo connection;
        public DLLmsContactArchiveRejectedDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsContactArchiveRejectedDetailsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<bool> SaveBulkLmsContactRejectedDetails(DataTable lmsreportdetails)
        {
            string storeProcCommand = "select * from lmscontactarchive_rejecteddetails_save(@lmsreportdetails)";
            object? param = new { lmsreportdetails };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<int> GetMaxCount(int LmsContactRemoveOverViewId)
        {
            string storeProcCommand = "select * from LmsContactArchive_RejectedDetails(@Action,@LmsContactRemoveOverViewId)";
            object? param = new { Action = "GetMaxCount", LmsContactRemoveOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<LmsContactArchiveRejectedDetails>> GetList(int LmsContactRemoveOverViewId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from LmsContactArchive_RejectedDetails(@Action,@OffSet, @FetchNext, @LmsContactRemoveOverViewId)";
            object? param = new { Action = "GetList", OffSet, FetchNext, LmsContactRemoveOverViewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<LmsContactArchiveRejectedDetails>(storeProcCommand, param)).ToList();

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
