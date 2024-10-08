﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLCacheReportDetailsSQL : CommonDataBaseInteraction, IDLCacheReportDetails
    {
        CommonInfo connection;
        public DLCacheReportDetailsSQL(int adsId)
        {
            connection = GetDBConnection();
        }
        public DLCacheReportDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<object> GetCacheReportDetails(MLCacheReportDetails cachereport)
        {
            try
            {
                string storeProcCommand = "CachedVisits";
                object? param = new { cachereport.Action, cachereport.Duration, cachereport.Start, cachereport.End, cachereport.FromDate, cachereport.ToDate };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<DataSet>(storeProcCommand, param, commandType: CommandType.StoredProcedure);               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<object> GetCachedMobileReportDetails(MLCacheReportDetails cachereport)
        {
            try
            {
                string storeProcCommand = "CachedMobile_Data";
                object? param = new { cachereport.Action, cachereport.FromDate, cachereport.ToDate, cachereport.Duration, cachereport.Start, cachereport.End };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<DataSet>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
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
