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
    public class DLWorkFlowWhatsAppBulkSentInitiationSQL : CommonDataBaseInteraction, IDLWorkFlowWhatsAppBulkSentInitiation
    {
        CommonInfo connection;

        public DLWorkFlowWhatsAppBulkSentInitiationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<IEnumerable<WorkFlowWhatsAppBulkSentInitiation>> GetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_WhatsAppBulkSentInitiation";
            object? param = new
            { Action = "GetSentInitiation" };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowWhatsAppBulkSentInitiation>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> UpdateSentInitiation(WorkFlowWhatsAppBulkSentInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_WhatsAppBulkSentInitiation";
            object? param = new { Action = "UpdateSentInitiation", BulkSentInitiation.SendingSettingId, BulkSentInitiation.InitiationStatus, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }
        public async Task<Int32> Save(WorkFlowWhatsAppBulkSentInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_WhatsAppBulkSentInitiation";
            object? param = new { Action = "Save", BulkSentInitiation.SendingSettingId, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);


        }
        public async Task<bool> ResetInitiation()
        {
            string storeProcCommand = "WorkFlow_WhatsAppBulkSentInitiation";
            object? param = new
            { Action = "ResetInitiation" };
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
