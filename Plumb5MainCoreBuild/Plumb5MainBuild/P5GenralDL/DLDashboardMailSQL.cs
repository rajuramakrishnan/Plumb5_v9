﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLDashboardMailSQL : CommonDataBaseInteraction, IDLDashboardMail
    {
        CommonInfo connection = null;
        public DLDashboardMailSQL(int AdsId)
        {
            connection = GetDBConnection(AdsId);
        }

        public DLDashboardMailSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<MailDashboardCampaignEffectiveness>> GetMailDashboardCampaignEffectiveness(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "Mail_Dashboard";
            object? param = new { Action = "GetMailDashboardCampaignEffectiveness", FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MailDashboardCampaignEffectiveness>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<MailDashboadEngagement?> GetMailDashboadEngagement(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "Mail_Dashboard";
            object? param = new { Action = "GetMailDashboadEngagement", FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MailDashboadEngagement?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<MailDashboardDelivery?> GetMailDashboardDelivery(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "Mail_Dashboard";
            object? param = new { Action = "GetMailDashboardDelivery", FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MailDashboardDelivery?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<MailPerformanceOverTime>> GetMailPerformanceOverTime(DateTime FromDate, DateTime ToDate)
        {
            string storeProcCommand = "Mail_Dashboard";
            object? param = new { Action = "GetMailPerformanceOverTime", FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MailPerformanceOverTime>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
