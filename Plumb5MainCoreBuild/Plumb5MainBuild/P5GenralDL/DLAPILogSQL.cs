﻿using P5GenralML;
using IP5GenralDL;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using DBInteraction;
using Dapper;
using Azure.Core;
using System;

namespace P5GenralDL
{
    public class DLAPILogSQL : CommonDataBaseInteraction, IDLAPILog
    {
        CommonInfo connection;

        public DLAPILogSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> Save(APILog ApiLog)
        {
            string storeProcCommand = "API_Log";
            object? param = new { Action= "Save", ApiLog.UniqueId, ApiLog.UserHostAddress, ApiLog.Headers, ApiLog.StatusCode, ApiLog.RequestBody, ApiLog.RequestedMethod, ApiLog.Useragent, ApiLog.AbsoluteUri, ApiLog.RequestType };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<Int32> GetMaxCount(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "API_Log";
            object? param = new { Action = "MaxCount", FromDate, ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<APILog>> GetAllDetails(DateTime FromDate, DateTime ToDate, int OffSet, int FetchNext)
        {
            string storeProcCommand = "API_Log";
            object? param = new { Action = "GET", OffSet, FetchNext, FromDate, ToDate }; 
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<APILog>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

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
