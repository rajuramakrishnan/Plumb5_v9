﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLWorkFlowWhatsAppPG : CommonDataBaseInteraction, IDLWorkFlowWhatsApp
    {
        CommonInfo connection;
        public DLWorkFlowWhatsAppPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<Int32> Save(MLWorkFlowWhatsApp WhatsappConfig)
        {
            string storeProcCommand = "select workflow_whatsapp_save(@WhatsAppTemplateId,@WhatsAppConfigurationNameId)"; 
            object? param = new { WhatsappConfig.WhatsAppTemplateId, WhatsappConfig.WhatsAppConfigurationNameId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<Int32> SaveAsync(MLWorkFlowWhatsApp WhatsappConfig)
        {
            string storeProcCommand = "select workflow_whatsapp_save(@WhatsAppTemplateId, @IsTriggerEveryActivity, @WhatsAppConfigurationNameId)";
             
            object? param = new { WhatsappConfig.WhatsAppTemplateId, WhatsappConfig.IsTriggerEveryActivity, WhatsappConfig.WhatsAppConfigurationNameId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> UpdateAsync(MLWorkFlowWhatsApp WhatsappConfig)
        {
            string storeProcCommand = "select workflow_whatsapp_update(@ConfigureWhatsAppId, @WhatsAppTemplateId, @IsTriggerEveryActivity, @WhatsAppConfigurationNameId)"; 
            object? param = new { WhatsappConfig.ConfigureWhatsAppId, WhatsappConfig.WhatsAppTemplateId, WhatsappConfig.IsTriggerEveryActivity, WhatsappConfig.WhatsAppConfigurationNameId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }


        public async void UpdateWorkflowWhatsAppCampaignSendStatus(int workflowwhatsappSendingSettingId)
        {
            string storeProcCommand = "select workflow_whatsapp_updateworkflowwhatsappcampaignsendstatus(@workflowwhatsappSendingSettingId)"; 
            object? param = new { workflowwhatsappSendingSettingId };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param) ;
        }

        public async Task<IEnumerable<WorkFlowWhatsApp>> GetRecentWorkflowWhatsAppCampaignsForDailyOnce()
        {
            string storeProcCommand = "select * from workflow_whatsapp_getrecentworkflowwhatsappcampaignsfordailyonce()";
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowWhatsApp>(storeProcCommand);

        }

        public async Task<SendingDatalist?> GetDetails(int ConfigureWhatsAppId)
        {
            string storeProcCommand = "select * from workflow_whatsapp_getdetails(@ConfigureWhatsAppId)"; 
            object? param = new { ConfigureWhatsAppId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<SendingDatalist?>(storeProcCommand, param);
        }

        public async Task<WorkFlowWhatsApp?> GetWhatsAppDetails(int ConfigureWhatsAppId)
        {
            string storeProcCommand = "select * from workflow_whatsapp_getwhatsappdetails(@ConfigureWhatsAppId)"; 
            object? param = new { ConfigureWhatsAppId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowWhatsApp?>(storeProcCommand, param);
        }

        public async Task<WorkFlowWhatsApp?> GetDetails(int ConfigureWhatsAppId, DateTime? FromDate, DateTime? ToDate, byte IsSplitTested = 0, string PhoneNumber = null)
        {
            string storeProcCommand = "select * from workflow_whatsapp_getdetails(@ConfigureWhatsAppId, @FromDate, @ToDate, @IsSplitTested, @PhoneNumber)"; 
            object? param = new { ConfigureWhatsAppId, FromDate, ToDate, IsSplitTested, PhoneNumber };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowWhatsApp?>(storeProcCommand, param);
        }
        public async Task<IEnumerable<WorkFlowWhatsApp>> GetRecentWorkflowWhatsAppCampaignsForInterval()
        {
            string storeProcCommand = "select * from workflow_whatsapp_getrecentworkflowwhatsappcampaignsforinterval()";
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowWhatsApp>(storeProcCommand);

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
