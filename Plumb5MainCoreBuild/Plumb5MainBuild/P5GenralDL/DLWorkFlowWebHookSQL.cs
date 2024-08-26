﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;

namespace P5GenralDL
{
    public class DLWorkFlowWebHookSQL : CommonDataBaseInteraction, IDLWorkFlowWebHook
    {
        CommonInfo connection;
        public DLWorkFlowWebHookSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowWebHookSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WorkFlowWebHook workFlowWebHook)
        {
            string storeProcCommand = "WorkFlow_WebHook";
            object? param = new { @Action = "Save", workFlowWebHook.RequestURL, workFlowWebHook.MethodType, workFlowWebHook.ContentType, workFlowWebHook.FieldMappingDetails, workFlowWebHook.Headers, workFlowWebHook.BasicAuthentication, workFlowWebHook.RawBody };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(WorkFlowWebHook workFlowWebHook)
        {
            string storeProcCommand = "WorkFlow_WebHook";
            object? param = new { @Action = "Update", workFlowWebHook.ConfigureWebHookId, workFlowWebHook.RequestURL, workFlowWebHook.MethodType, workFlowWebHook.ContentType, workFlowWebHook.FieldMappingDetails, workFlowWebHook.Headers, workFlowWebHook.BasicAuthentication, workFlowWebHook.RawBody };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<WorkFlowWebHook?> GetWebHookDetails(int ConfigureWebHookId)
        {
            string storeProcCommand = "WorkFlow_WebHook";
            object? param = new { @Action = "GetWebHookDetails", ConfigureWebHookId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowWebHook?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<WorkFlowWebHook?> GetCountsData(int ConfigureWebHookId, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            string storeProcCommand = "WorkFlow_WebHook";
            object? param = new { @Action = "GetCountsData", ConfigureWebHookId, FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowWebHook?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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

