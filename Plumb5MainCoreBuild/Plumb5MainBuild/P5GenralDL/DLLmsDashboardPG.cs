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
    public class DLLmsDashboardPG : CommonDataBaseInteraction, IDLLmsDashboard
    {
        CommonInfo connection;
        public DLLmsDashboardPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsDashboardPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<DataSet> GetSummary(string UserIdList, List<int> UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getsummary(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime)";
            object? param = new { UserIdList, UserGroupIdList = UserGroupIdList != null ? string.Join(",", UserGroupIdList) : "", FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetSummaryDetails(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_summarydetails(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetFollowUpsData(string UserIdList, List<int> UserGroupIdList, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getfollowupsdata(@UserIdList, @UserGroupIdList)";
            object? param = new { UserIdList, UserGroupIdList = UserGroupIdList != null ? string.Join(",", UserGroupIdList) : "" };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> LeadWonLeadLost(int Duration, string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, string StageName, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_leadwonleadlost(@UserIdList, @UserGroupIdList, @FromDateTime,@ToDateTime, @Duration, @StageName, @OrderBy )";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, Duration, StageName, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> TopStages(string UserIdList, List<int> UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_topstages(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime)";
            object? param = new { UserIdList, UserGroupIdList = UserGroupIdList != null ? string.Join(",", UserGroupIdList) : "", FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> TopSources(string UserIdList, List<int> UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_topsources(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime)";
            object? param = new { UserIdList, UserGroupIdList = UserGroupIdList != null ? string.Join(",", UserGroupIdList) : "", FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> LableWiseLeadsCount(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_labelwiseleads(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetLeadCampaignDetails(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getleadcampaigndetails(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetAllStageWiseLeadsCount(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getallstagewiseleadscount(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetAllSourceWiseLeadsCount(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getallsourcewiseleadscount(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetFollowUpLeadsCount(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getfollowupleadscount(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetTopPerformerByLeadLabel(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_gettopperformerbyleadlabel(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetSTAGEWISEVSLABELWISE(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getstagewisevslabelwise(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetSOURCEWISEVSLABELWISE(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getsourcewisevslabelwise(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetSTAGEVSSOURCEWISE(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getstagevssourcewise(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetRevenueDetails(string UserIdList, string UserGroupIdList, Nullable<DateTime> FromDateTime, Nullable<DateTime> ToDateTime, int OrderBy)
        {
            string storeProcCommand = "select * from contact_lms_dashboard_getrevenuedetails(@UserIdList, @UserGroupIdList, @FromDateTime, @ToDateTime, @OrderBy)";
            object? param = new { UserIdList, UserGroupIdList, FromDateTime, ToDateTime, OrderBy };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
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
