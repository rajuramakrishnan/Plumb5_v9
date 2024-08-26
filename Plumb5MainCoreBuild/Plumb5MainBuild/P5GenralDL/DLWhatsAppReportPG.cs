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
    public class DLWhatsAppReportPG : CommonDataBaseInteraction, IDLWhatsAppReport
    {
        readonly CommonInfo connection;
        public DLWhatsAppReportPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public async Task<int> MaxCount(DateTime? FromDateTime, DateTime? ToDateTime, string CampaignName, string TemplateName, int WhatsAppSendingSettingId)
        {
            string storeProcCommand = "select whatsapp_reportdetails_getmaxcount()";
            object? param = new { FromDateTime, ToDateTime, CampaignName, TemplateName, WhatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<MLWhatsAppReport>> GetReportData(DateTime? FromDateTime, DateTime? ToDateTime, int OffSet, int FetchNext, string CampaignName = null, string TemplateName = null, int WhatsAppSendingSettingId = 0)
        {
            string storeProcCommand = "select * from whatsapp_reportdetails_getreportdata(@FromDateTime, @ToDateTime, @OffSet, @FetchNext, @CampaignName, @TemplateName, @WhatsAppSendingSettingId)";
            object? param = new { FromDateTime, ToDateTime, OffSet, FetchNext, CampaignName, TemplateName, WhatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppReport>(storeProcCommand, param)).ToList();
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
