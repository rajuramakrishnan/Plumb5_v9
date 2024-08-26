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
    public class DLAdminCreditAllocationReportSQL : CommonDataBaseInteraction, IDLAdminCreditAllocationReport
    {
        CommonInfo connection;

        public DLAdminCreditAllocationReportSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<int> Save(AdminCreditAllocationReport credit)
        {
            string storeProcCommand = "Credit_AllocationReport";
            object? param = new { Action = "Save", credit.UserInfoUserId, credit.AccountId, credit.FeatureId, credit.ProviderName, credit.TotalCredit, credit.Remarks };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetCount(AdminCreditAllocationReport obj, DateTime fromDateTime, DateTime toDateTime)
        {
            string storeProcCommand = "Credit_AllocationReport";
            object? param = new { Action = "COUNT", obj.FeatureId, obj.ProviderName, fromDateTime, toDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<MLAdminCreditAllocationReport>> GetDetails(AdminCreditAllocationReport obj, DateTime fromDateTime, DateTime toDateTime, int Offset, int FetchNext)
        {
            string storeProcCommand = "Credit_AllocationReport";
            object? param = new { Action = "COUNT", obj.FeatureId, obj.ProviderName, fromDateTime, toDateTime, Offset, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLAdminCreditAllocationReport>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
