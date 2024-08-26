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
    public class DLDashboardMailPG : CommonDataBaseInteraction, IDLDashboardMail
    {
        CommonInfo connection = null;
        public DLDashboardMailPG(int AdsId)
        {
            connection = GetDBConnection(AdsId);
        }

        public DLDashboardMailPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<MailDashboardCampaignEffectiveness>> GetMailDashboardCampaignEffectiveness(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select * from mail_dashboard_getmaildashboardcampaigneffectiveness(@FromDate, @ToDate)";
            object? param = new { FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MailDashboardCampaignEffectiveness>(storeProcCommand, param)).ToList();
        }

        public async Task<MailDashboadEngagement?> GetMailDashboadEngagement(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select * from mail_dashboard_getmaildashboadengagement(@FromDate, @ToDate)";
            object? param = new { FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MailDashboadEngagement?>(storeProcCommand, param);
        }

        public async Task<MailDashboardDelivery?> GetMailDashboardDelivery(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select * from mail_dashboard_getmaildashboarddelivery(@FromDate, @ToDate)";
            object? param = new { FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MailDashboardDelivery?>(storeProcCommand, param);
        }

        public async Task<List<MailPerformanceOverTime>> GetMailPerformanceOverTime(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "select * from mail_dashboard_getmailperformanceovertime(@FromDate, @ToDate)";
            object? param = new { FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MailPerformanceOverTime>(storeProcCommand, param)).ToList();
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
