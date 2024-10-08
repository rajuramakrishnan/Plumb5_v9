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
    public class DLWebPushReportPG : CommonDataBaseInteraction, IDLWebPushReport
    {
        readonly CommonInfo connection;
        public DLWebPushReportPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushReportPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> MaxCount(DateTime? FromDateTime, DateTime? ToDateTime, string CampaignName)
        {
            string storeProcCommand = "select webpush_reportdetails_getmaxcount(@FromDateTime, @ToDateTime, @CampaignName)";
            object? param = new { FromDateTime, ToDateTime, CampaignName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<MLWebPushReport>> GetReportData(DateTime? FromDateTime, DateTime? ToDateTime, int OffSet, int FetchNext, string CampaignName = null)
        {
            string storeProcCommand = "select * from webpush_reportdetails_getreportdata(@FromDateTime, @ToDateTime, @OffSet, @FetchNext, @CampaignName)";
            object? param = new { FromDateTime, ToDateTime, OffSet, FetchNext, CampaignName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWebPushReport>(storeProcCommand, param)).ToList();
        }

        public async Task<List<MLWebPushCampaign>> GetWebPushCampaignResponseData(int webpushSendingSettingId)
        {
            string storeProcCommand = "select * from webpush_sent_getwebpushcampaignresponsedata(@webpushSendingSettingId)";
            object? param = new { webpushSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWebPushCampaign>(storeProcCommand, param)).ToList();
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
