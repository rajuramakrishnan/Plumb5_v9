﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;

namespace P5GenralDL
{
    public class DLWorkFlowSMSPG : CommonDataBaseInteraction, IDLWorkFlowSMS
    {
        CommonInfo connection = null;
        public DLWorkFlowSMSPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowSMSPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> Save(WorkFlowSMS workFlowSMS)
        {
            string storeProcCommand = "select workflow_sms_save(@SmsTemplateId, @IsPromotionalOrTransactionalType, @IsTriggerEveryActivity, @SmsConfigurationNameId)";
            object? param = new { workFlowSMS.SmsTemplateId, workFlowSMS.IsPromotionalOrTransactionalType, workFlowSMS.IsTriggerEveryActivity, workFlowSMS.SmsConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Update(WorkFlowSMS workFlowSMS)
        {
            string storeProcCommand = "select workflow_sms_update(@ConfigureSmsId, @SmsTemplateId, @IsPromotionalOrTransactionalType, @IsTriggerEveryActivity, @SmsConfigurationNameId)";
            object? param = new { workFlowSMS.ConfigureSmsId, workFlowSMS.SmsTemplateId, workFlowSMS.IsPromotionalOrTransactionalType, workFlowSMS.IsTriggerEveryActivity, workFlowSMS.SmsConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param) > 0;
        }
        public async Task<WorkFlowSMS?> GetDetails(int ConfigureSmsId, DateTime? FromDate = null, DateTime? ToDate = null, byte IsSplitTested = 0, string PhoneNumber = null)
        {
            string storeProcCommand = "select *  from workflow_sms_getdetails(@ConfigureSmsId, @FromDate, @ToDate, @IsSplitTested, @PhoneNumber)";
            object? param = new { ConfigureSmsId, FromDate, ToDate, IsSplitTested, PhoneNumber };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowSMS?>(storeProcCommand, param);
        }

        public async Task<WorkFlowSMS?> GetSmsDetails(int ConfigureSmsId)
        {
            string storeProcCommand = "select *  from workflow_sms_getsmsdetails(@ConfigureSmsId)";
            object? param = new { ConfigureSmsId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowSMS?>(storeProcCommand, param);
        }

        public async void UpdateWorkflowSmsCampaignSendStatus(int workflowSmsSendingSettingId)
        {
            string storeProcCommand = "select workflow_sms_updateworkflowsmscampaignsendstatus (@Configuresmsid)";
            object? param = new { workflowSmsSendingSettingId };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<List<WorkFlowSMS>> GetRecentWorkflowSmsCampaignsForInterval()
        {
            string storeProcCommand = "select *  from workflow_sms_getrecentworkflowsmscampaignsforinterval()";
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowSMS>(storeProcCommand)).ToList();
        }

        public async Task<List<WorkFlowSMS>> GetRecentWorkflowSmsCampaignsForDailyOnce(int WorkFlowDaysLimit = 0)
        {
            string storeProcCommand = "select *  from workflow_sms_getrecentworkflowsmscampaignsfordailyonce(@WorkFlowDaysLimit)";
            object? param = new { WorkFlowDaysLimit };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowSMS>(storeProcCommand)).ToList();
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

