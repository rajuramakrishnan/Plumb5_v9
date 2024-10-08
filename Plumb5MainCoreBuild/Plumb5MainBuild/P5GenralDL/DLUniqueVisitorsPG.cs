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
    public class _Plumb5DLUniqueVisitorsPG : CommonDataBaseInteraction, IDLUniqueVisitors
    {
        CommonInfo connection;
        private bool _disposed = false;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AccountId"></param>
        public _Plumb5DLUniqueVisitorsPG(int AccountId)
        {
            connection = GetDBConnection(AccountId);
        }

        public _Plumb5DLUniqueVisitorsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<object> Select_UniqueVisitsMaxCount(_Plumb5MLUniqueVisits mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectuniquevisits_maxcount(@FromDate, @ToDate, @Key, @Data, @Data2, @Data3)";
                object? param = new { mlObj.FromDate, mlObj.ToDate, mlObj.Key, mlObj.Data, mlObj.Data2, mlObj.Data3 };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_UniqueVisits(_Plumb5MLUniqueVisits mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectuniquevisits(@FromDate, @ToDate, @Start, @End, @Key,@Location,@Data, @Data2, @Data3)";
                object? param = new { mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End, mlObj.Key, mlObj.Location, mlObj.Data, mlObj.Data2, mlObj.Data3 };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        public async Task<DataSet> GetCachedUniqueVisitsMaxCount(_Plumb5MLUniqueVisits mlObj)
        {
            try
            {
                var storeProcCommand = "select * from uniquevisits_cached_maxcount(@Key, @Data, @FromDate, @ToDate)";
                object? param = new { mlObj.Key, mlObj.Data, mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }

        public async Task<DataSet> GetCachedUniqueVisits(_Plumb5MLUniqueVisits mlObj)
        {
            try
            {
                var storeProcCommand = "select * from uniquevisits_cached_getreport(@Key, @Data, @FromDate, @ToDate, @Start, @End)";
                object? param = new { mlObj.Key, mlObj.Data, mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }

        public async Task<DataSet> SelectUnique_DeviceDetailsCount(_Plumb5MLGetDevices mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectuniquedevicedetails_maxcount(@FromDate, @ToDate)";
                object? param = new { mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }
        public async Task<DataSet> SelectDeviceUniqueVisits(_Plumb5MLUniqueVisits mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectuniquevisitordevicedetails_getlist(@FromDate, @ToDate, @Start, @End)";
                object? param = new { mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception ex)
            {
                AddDbError(ex.ToString(), DateTime.Now.ToString(), "plumb5-db", ex.StackTrace.ToString());
                return null;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {

                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
