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
    public class DLAudiencePG : CommonDataBaseInteraction, IDLAudience
    {
        CommonInfo connection;
        public DLAudiencePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLAudiencePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<object> GetVisitorReportCount(_Plumb5MLGetVisitors mlObj)
        {
            try
            {

                string storeProcCommand = "select * from selectvisitor_maxcount(@Duration, @FromDate, @ToDate, @drpSearchBy, @SearchTextValue )";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate, mlObj.drpSearchBy, mlObj.SearchTextValue };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Visitors
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_GetVisitors(_Plumb5MLGetVisitors mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectvisitor_getlist(@Duration, @FromDate, @ToDate, @Start, @End, @drpSearchBy, @SearchTextValue)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End, mlObj.drpSearchBy, mlObj.SearchTextValue };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<object> BindFilterValues(_Plumb5MLGetVisitors mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectvisitor_filter(@Duration, @FromDate, @ToDate,@drpSearchBy, @SearchTextValue)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate, mlObj.drpSearchBy, mlObj.SearchTextValue };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<object>(storeProcCommand, param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<object> Select_CityDetails(_Plumb5MLCity mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectcity_getlist(@Duration, @FromDate, @ToDate,@Start, @End)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<object>(storeProcCommand, param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<object> Select_CityMapDetails(_Plumb5MLCity mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectcity_getmapdata(@Duration, @FromDate, @ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<object>(storeProcCommand, param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<object> Select_CityDetails_MaxCount(_Plumb5MLCity mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectcity_maxcount(@Duration, @FromDate, @ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<object>(storeProcCommand, param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Frequency
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_Frequency(_Plumb5MLFrequency mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectfrequency(@FromDate, @ToDate)";
                object? param = new { mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_Recency(_Plumb5MLRecency mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectrecency(@Start, @End)";
                object? param = new { mlObj.Start, mlObj.End };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Recency Returning
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_RecencyReturn(_Plumb5MLRecencyReturn mlObj)
        {
            try
            {
                string storeProcCommand = "select * from SelectRecencyReturn(@FromDate, @ToDate,@Start, @End)";
                object? param = new { mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Time Spend and Page Depth
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_TimeSpend(_Plumb5MLTimeSpend mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selecttimespend(@Duration, @FromDate,@ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_PageDepth(_Plumb5MLPageDepth mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectpagedepth(@Duration, @FromDate,@ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Network
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_NetworkDetails(_Plumb5MLGetNetwork mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectnetwork_getlist(@Duration, @FromDate,@ToDate,@Start,@End)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Network
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_NetworkDetailsCount(_Plumb5MLGetNetwork mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectnetwork_maxcount(@Duration, @FromDate,@ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Devices
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_DeviceDetails(_Plumb5MLGetDevices mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectdevicedetails_getlist(@FromDate,@ToDate,@startcount,@endcount)";
                object? param = new { mlObj.FromDate, mlObj.ToDate, mlObj.startcount, mlObj.endcount };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<object> Select_BrowserReportCount(_Plumb5MLBrowsersDetails mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectbrowser_maxcount(@Duration,@FromDate,@ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Devices
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_DeviceDetailsCount(_Plumb5MLGetDevices mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectdevicedetails_maxcount(@FromDate,@ToDate)";
                object? param = new { mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Browser
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_BrowserDetailsWithDateRange(_Plumb5MLBrowsersDetails mlObj)
        {
            try
            {
                string storeProcCommand = "select * from selectbrowser_getlist(@Duration,@FromDate,@ToDate,@startcount,@endcount)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate, mlObj.startcount, mlObj.endcount };

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Auto Suggestion
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<DataSet> Select_SearchBy_AutoSuggest(_Plumb5MLAutosuggest mlObj)
        {
            try
            {
                string storeProcCommand = "select * from SelectVisitorAutoSuggest(@Type,@SearchText)";
                object? param = new { mlObj.Type, mlObj.SearchText };
                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand, param);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Search By OnClick
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_SearchByOnclick(_Plumb5MLSearchBy mlObj)
        {
            try
            {
                string storeProcCommand = "select * from SelectVisitor(@FromDate,@ToDate,@Start,@End,@Type,@SearchBy,@VisitorSummary)";
                object? param = new { mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End, mlObj.Type, mlObj.SearchBy, mlObj.VisitorSummary };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryAsync<object>(storeProcCommand, param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Select_GroupNames()
        {
            try
            {
                string storeProcCommand = "select * from selectgroupname()";

                using var db = GetDbConnection(connection.Connection);
                var list = await db.ExecuteReaderAsync(storeProcCommand);
                var dataset = ConvertDataReaderToDataSet(list);
                return dataset;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Insert_AddToGroup(_Plumb5MLGroupName mlObj)
        {
            try
            {

                string storeProcCommand = "select * from insertvisitortogroup(@ListData)";
                object? param = new { mlObj.ListData };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Update_Score(_Plumb5MLUpdateScore mlObj)
        {
            try
            {
                string storeProcCommand = "select * from UpdateScore(@MachineId,@Key)";
                object? param = new { mlObj.MachineId, mlObj.Key };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlObj"></param>
        /// <returns></returns>
        public async Task<object> Transaction(_Plumb5MLUpdateScore mlObj)
        {
            try
            {
                string storeProcCommand = "select * from TransactionCheck()";
                object? param = new { };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

            }
            catch
            {
                return 0;
            }
        }

        public async Task<object> Select_Location_CityDetails(_Plumb5MLCity mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectcity_getlist(@Duration, @FromDate, @ToDate, @Start, @End)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate, mlObj.Start, mlObj.End };

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

        public async Task<object> Select_Location_CityMapDetails(_Plumb5MLCity mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectcity_getmapdata(@Duration, @FromDate, @ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

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

        public async Task<object> Select_Location_CityDetails_MaxCount(_Plumb5MLCity mlObj)
        {
            try
            {
                var storeProcCommand = "select * from selectcity_maxcount(@Duration, @FromDate, @ToDate)";
                object? param = new { mlObj.Duration, mlObj.FromDate, mlObj.ToDate };

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
