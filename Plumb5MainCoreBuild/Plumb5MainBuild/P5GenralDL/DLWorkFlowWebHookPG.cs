﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWorkFlowWebHookPG : CommonDataBaseInteraction, IDLWorkFlowWebHook
    {
        CommonInfo connection;
        public DLWorkFlowWebHookPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowWebHookPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WorkFlowWebHook workFlowWebHook)
        {
            string storeProcCommand = "select * from workflow_webhook_save(@RequestURL, @MethodType, @ContentType, @FieldMappingDetails, @Headers, @BasicAuthentication, @RawBody)";
            object? param = new { workFlowWebHook.RequestURL, workFlowWebHook.MethodType, workFlowWebHook.ContentType, workFlowWebHook.FieldMappingDetails, workFlowWebHook.Headers, workFlowWebHook.BasicAuthentication, workFlowWebHook.RawBody };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Update(WorkFlowWebHook workFlowWebHook)
        {
            string storeProcCommand = "select * from workflow_webhook_update(@ConfigureWebHookId, @RequestURL, @MethodType, @ContentType, @FieldMappingDetails, @Headers, @BasicAuthentication, @RawBody)";
            object? param = new { workFlowWebHook.ConfigureWebHookId, workFlowWebHook.RequestURL, workFlowWebHook.MethodType, workFlowWebHook.ContentType, workFlowWebHook.FieldMappingDetails, workFlowWebHook.Headers, workFlowWebHook.BasicAuthentication, workFlowWebHook.RawBody };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<WorkFlowWebHook?> GetWebHookDetails(int ConfigureWebHookId)
        {
            string storeProcCommand = "select * from workflow_webhook_getwebhookdetails(@ConfigureWebHookId)";
            object? param = new { ConfigureWebHookId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowWebHook?>(storeProcCommand, param);
        }

        public async Task<WorkFlowWebHook?> GetCountsData(int ConfigureWebHookId, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            string storeProcCommand = "select * from workflow_webhook_getcountsdata(@ConfigureWebHookId,@FromDate, @ToDate)";
            object? param = new { ConfigureWebHookId, FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowWebHook?>(storeProcCommand, param);
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

        #endregion End of Dispose Methods
    }
}

