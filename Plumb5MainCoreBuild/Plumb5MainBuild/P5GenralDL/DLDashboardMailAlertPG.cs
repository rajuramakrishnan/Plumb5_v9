﻿using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBInteraction;
using Dapper;
using IP5GenralDL;
using System.Threading.Channels;

namespace P5GenralDL
{
    public class DLDashboardMailAlertPG : CommonDataBaseInteraction, IDLDashboardMailAlert
    {
        CommonInfo connection = null;
        public DLDashboardMailAlertPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLDashboardMailAlertPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(DashboardMailAlert mailAlert)
        {
            string storeProcCommand = "select dashboard_mailalert_save(@DashboardId, @MailAlertType, @ToEmailId, @CCFromEmailId, @IsDaily, @IsFornightly, @IsWeekly, @IsMonthly, @IsQuarterly )";
            object? param = new { mailAlert.DashboardId, mailAlert.MailAlertType, mailAlert.ToEmailId, mailAlert.CCFromEmailId, mailAlert.IsDaily, mailAlert.IsFornightly, mailAlert.IsWeekly, mailAlert.IsMonthly, mailAlert.IsQuarterly };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<DashboardMailAlert?>   GetDetail(int Id)
        {
            string storeProcCommand = "select * from dashboard_mailalert_get(@Id)"; 
            object? param = new { Id };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<DashboardMailAlert?>(storeProcCommand, param);
        }

        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "select  dashboard_mailalert_delete(@Id)"; 
            object? param = new {Id };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<IEnumerable<DashboardMailAlert>> GetAllMailAlerts()
        {
            string storeProcCommand = "select * from dashboard_mailalert_getall()"; 
            object? param = new { };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<DashboardMailAlert>(storeProcCommand, param);
        }

        public async Task<bool> UpdateLastWeeklyTriggerDate(DashboardMailAlert mailAlert)
        {
            string storeProcCommand = "select dashboard_mailalert_updateweeklytriggerdate(@DashboardId,@LastWeeklyTriggerDate)"; 
            object? param = new { mailAlert.DashboardId, mailAlert.LastWeeklyTriggerDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> UpdateLastFornightlyTriggerDate(DashboardMailAlert mailAlert)
        {
            string storeProcCommand = "select dashboard_mailalert_updatefornightlytriggerdate(@DashboardId,@LastFornightlyTriggerDate)"; 
            object? param = new { mailAlert.DashboardId, mailAlert.LastFornightlyTriggerDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateLastMonthlyTriggerDate(DashboardMailAlert mailAlert)
        {
            string storeProcCommand = "select dashboard_mailalert_updatemonthlytriggerdate(@DashboardId,@LastMonthlyTriggerDate)"; 
            object? param = new { mailAlert.DashboardId, mailAlert.LastMonthlyTriggerDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateLastQuarterlyTriggerDate(DashboardMailAlert mailAlert)
        {
            string storeProcCommand = "select dashboard_mailalert_updatequarterlytriggerdate(@DashboardId,@LastQuarterlyTriggerDate)"; 
            object? param = new { mailAlert.DashboardId, mailAlert.LastQuarterlyTriggerDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateLastDailyTriggerDate(DashboardMailAlert mailAlert)
        {
            string storeProcCommand = "select dashboard_mailalert_lastdailytriggerdate(@DashboardId,@LastDailyTriggerDate)"; 
            object? param = new {  mailAlert.DashboardId, mailAlert.LastDailyTriggerDate };
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
