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
    public class DLWorkFlowWebPushBulkInitiationSQL : CommonDataBaseInteraction, IDLWorkFlowWebPushBulkInitiation
    {
        CommonInfo connection;

        public DLWorkFlowWebPushBulkInitiationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowWebPushBulkInitiationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<IEnumerable<WorkFlowWebPushBulkInitiation>> GetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_WebPushBulkInitiation";
            object? param = new
            { Action = "GetSentInitiation" };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowWebPushBulkInitiation>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> UpdateSentInitiation(WorkFlowWebPushBulkInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_WebPushBulkInitiation";
            object? param = new { Action = "UpdateSentInitiation", BulkSentInitiation.SendingSettingId, BulkSentInitiation.InitiationStatus, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> ResetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_WebPushBulkInitiation";
            object? param = new
            { Action = "ResetSentInitiation" };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<Int32> Save(WorkFlowWebPushBulkInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_WebPushBulkInitiation";
            object? param = new { Action = "Save", BulkSentInitiation.SendingSettingId, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
