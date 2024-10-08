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
    public class DLAPILogPG : CommonDataBaseInteraction, IDLAPILog
    {
        CommonInfo connection;

        public DLAPILogPG()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> Save(APILog ApiLog)
        {
            string storeProcCommand = "select api_log_save (@UniqueId, @UserHostAddress, @Headers, @StatusCode, @RequestBody, @RequestedMethod, @Useragent, @AbsoluteUri, @RequestType)";
            object? param = new { ApiLog.UniqueId, ApiLog.UserHostAddress, ApiLog.Headers, ApiLog.StatusCode, ApiLog.RequestBody, ApiLog.RequestedMethod, ApiLog.Useragent, ApiLog.AbsoluteUri, ApiLog.RequestType };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<Int32> GetMaxCount(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select api_log_maxcount( @FromDate, @ToDate)";
            object? param = new { FromDate, ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<APILog>> GetAllDetails(DateTime FromDate, DateTime ToDate, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from api_log_get(@OffSet, @FetchNext, @FromDate, @ToDatet)";
            object? param = new { OffSet, FetchNext, FromDate, ToDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<APILog>(storeProcCommand, param);
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
