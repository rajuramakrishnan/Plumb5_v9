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
    public class DLCustomDashboardMailAlertPG : CommonDataBaseInteraction, IDLCustomDashboardMailAlert
    {
        CommonInfo connection = null;
        public DLCustomDashboardMailAlertPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLCustomDashboardMailAlertPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<MLCustomDashboardMailAlert?> GetAllMailAlerts(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select *  from custom_dashboardmailalert_get(@FromDate, @ToDate)";
            object? param = new { FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLCustomDashboardMailAlert?>(storeProcCommand, param);
        }

        public async Task<MLCustomDashboardMailAlertNew?> GetAllMailAlertsNew(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select *  from custom_dashboardmailalert_new_get(@FromDate, @ToDate)";
            object? param = new { FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLCustomDashboardMailAlertNew?>(storeProcCommand, param);
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

