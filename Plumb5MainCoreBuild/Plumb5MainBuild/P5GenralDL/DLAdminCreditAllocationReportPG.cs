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
    public class DLAdminCreditAllocationReportPG : CommonDataBaseInteraction, IDLAdminCreditAllocationReport
    {
        CommonInfo connection;

        public DLAdminCreditAllocationReportPG()
        {
            connection = GetDBConnection();
        }

        public async Task<int> Save(AdminCreditAllocationReport credit)
        {
            string storeProcCommand = "select credit_allocationreport_save(@UserInfoUserId,@AccountId,@FeatureId,@ProviderName,@TotalCredit,@Remarks)";
            object? param = new { credit.UserInfoUserId, credit.AccountId, credit.FeatureId, credit.ProviderName, credit.TotalCredit, credit.Remarks };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<int> GetCount(AdminCreditAllocationReport obj, DateTime fromDateTime, DateTime toDateTime)
        {
            string storeProcCommand = "select credit_allocationreport_count(@FeatureId,@ProviderName,@fromDateTime,@toDateTime)";
            object? param = new { obj.FeatureId, obj.ProviderName, fromDateTime, toDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<MLAdminCreditAllocationReport>> GetDetails(AdminCreditAllocationReport obj, DateTime fromDateTime, DateTime toDateTime, int Offset, int FetchNext)
        {
            string storeProcCommand = "select * from credit_allocationreport_get(@FeatureId,@ProviderName,@fromDateTime,@toDateTime,@Offset, @FetchNext)";
            object? param = new { obj.FeatureId, obj.ProviderName, fromDateTime, toDateTime, Offset, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLAdminCreditAllocationReport>(storeProcCommand, param)).ToList();
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
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }
}
