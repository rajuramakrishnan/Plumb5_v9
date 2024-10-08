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
    public class DLWorkFlowWebHookBulkSentInitiationSQL : CommonDataBaseInteraction, IDLWorkFlowWebHookBulkSentInitiation
    {
        CommonInfo connection;

        public DLWorkFlowWebHookBulkSentInitiationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowWebHookBulkSentInitiationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<IEnumerable<WorkFlowWebHookBulkSentInitiation>> GetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_WebHookBulkSentInitiation";
            object? param = new { Action= "GetSentInitiation" };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowWebHookBulkSentInitiation>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> UpdateSentInitiation(WorkFlowWebHookBulkSentInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_WebHookBulkSentInitiation";
            object? param = new { Action = "UpdateSentInitiation",  BulkSentInitiation.SendingSettingId, BulkSentInitiation.InitiationStatus, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0; 

        }

        public async Task<bool> ResetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_WebHookBulkSentInitiation";
            object? param = new { Action = "ResetSentInitiation"  };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
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
