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
    public class DLLmsFollowUpReportSQL : CommonDataBaseInteraction, IDLLmsFollowUpReport
    {
        CommonInfo connection;
        public DLLmsFollowUpReportSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<Int32> GetMaxCount(string UserIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "Lms_CustomReport";
            object? param = new { Action="FollowUpMaxCount", UserIdList, FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<DataSet> GetLmsFollowUpReport(string UserIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Lms_CustomReport";
            object? param = new { Action = "GetLmsFollowUpReport", UserIdList, FromDateTime, ToDateTime, OffSet, FetchNext };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
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
