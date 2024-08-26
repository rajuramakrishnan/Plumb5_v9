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
    public class DLWebPushDashboardPG : CommonDataBaseInteraction, IDLWebPushDashboard
    {
        CommonInfo connection;

        public DLWebPushDashboardPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushDashboardPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<MLWebPushDashboard?> GetSubcribersDetails(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from webpush_dashboard_getsubcribersdetails(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLWebPushDashboard?>(storeProcCommand, param);
        }

        public async Task<MLWebPushDashboard?> GetCampaignDetails(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from webpush_dashboard_getcampaigndetails(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLWebPushDashboard?>(storeProcCommand, param);

        }

        public async Task<List<MLWebPushDashboard>> GetNotificationDetails(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "select * from webpush_dashboard_getnotificationdetails(@FromDateTime, @ToDateTime)";
            object? param = new { FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWebPushDashboard>(storeProcCommand, param)).ToList();

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
