﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLCustomDashboardMailAlertSQL : CommonDataBaseInteraction, IDLCustomDashboardMailAlert
    {
        CommonInfo connection = null;
        public DLCustomDashboardMailAlertSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLCustomDashboardMailAlertSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<MLCustomDashboardMailAlert?> GetAllMailAlerts(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "Custom_DashboardMailAlert";
            object? param = new { @Action = "GET", FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLCustomDashboardMailAlert?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<MLCustomDashboardMailAlertNew?> GetAllMailAlertsNew(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "Custom_DashboardMailAlert_New";
            object? param = new { @Action = "GET", FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLCustomDashboardMailAlertNew?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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


