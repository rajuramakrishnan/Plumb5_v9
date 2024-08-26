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
    public class DLMailScheduledReportSQL : CommonDataBaseInteraction, IDLMailScheduledReport
    {
        CommonInfo connection;
        public DLMailScheduledReportSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLMailScheduledReportSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> GetMaxCount(DateTime FromDateTime, DateTime ToDateTime, string CampignName = null)
        {
            string storeProcCommand = "Mail_ScheduledReport";
            object? param = new { Action = "GetScheduledCount", FromDateTime, ToDateTime, CampignName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<MLMailScheduled>> GetScheduled(int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime, List<string> fieldsName = null, string CampignName = null)
        {
            string storeProcCommand = "Mail_ScheduledReport";
            object? param = new { Action = "GetScheduled", OffSet, FetchNext, FromDateTime, ToDateTime, CampignName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLMailScheduled>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }


        public async Task<bool> Delete(Int32 Id)
        {
            string storeProcCommand = "Mail_ScheduledReport";
            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        #region Dispose Method
        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    connection = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion End of Dispose Method
    }
}
