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
    public class DLFormDashboardPG : CommonDataBaseInteraction, IDLFormDashboard
    {
        CommonInfo connection = null;
        public DLFormDashboardPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLFormDashboardPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<List<MLFormDashboardDetails>> GetTotalFormSubmissions(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select *  from form_dashbord_totalformsubmissions(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLFormDashboardDetails>(storeProcCommand, param)).ToList();
        }
        public async Task<List<MLFormDashboardDetails>> GetTopFivePerFormingForms(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select *  from form_dashbord_topfiveperformingforms(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLFormDashboardDetails>(storeProcCommand, param)).ToList();
        }

        public async Task<MLFormDashboardDetails?> GetPlatformDistribution(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select *  from form_dashbord_platformdistribution(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLFormDashboardDetails>(storeProcCommand, param);
        }

        public async Task<MLFormDashboardDetails?> GetAggregateFormsData(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select *  from form_dashbord_aggregateformsdata(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLFormDashboardDetails>(storeProcCommand, param);
        }
        public async Task<int> GetMaxCount(DateTime FromDateTime, DateTime ToDateTime, string EmbeddedFormOrPopUpFormOrTaggedForm, int FormId)
        {
            string storeProcCommand = "select formreports_byforms_maxcount(@FromDateTime, @ToDateTime, @FormId, @EmbeddedFormOrPopUpFormOrTaggedForm)";
            object? param = new { FromDateTime, ToDateTime, FormId, EmbeddedFormOrPopUpFormOrTaggedForm };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<List<MLFormDashboard>> GetFormByReport(int OffSet, int FetchNext, DateTime FromDateTime, DateTime ToDateTime, string EmbeddedFormOrPopUpFormOrTaggedForm, int FormId)
        {
            string storeProcCommand = "select *  from formreports_byforms_getformreport(@FromDateTime, @ToDateTime, @FormId, @EmbeddedFormOrPopUpFormOrTaggedForm, @OffSet,@FetchNext)";
            object? param = new { FromDateTime, ToDateTime, FormId, EmbeddedFormOrPopUpFormOrTaggedForm, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLFormDashboard>(storeProcCommand, param)).ToList();
        }

        public async Task<List<FormDetails>> GetFormDetailsByReport(DateTime FromDateTime, DateTime ToDateTime, string EmbeddedFormOrPopUpFormOrTaggedForm)
        {
            string storeProcCommand = "select *  from formreports_byforms_getformdetails(@FromDateTime, @ToDateTime, @EmbeddedFormOrPopUpFormOrTaggedForm)";
            object? param = new { FromDateTime, ToDateTime, EmbeddedFormOrPopUpFormOrTaggedForm };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<FormDetails>(storeProcCommand, param)).ToList();
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

