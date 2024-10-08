﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLWorkFlowSMSSQL : CommonDataBaseInteraction, IDLWorkFlowSMS
    {
        CommonInfo connection = null;
        public DLWorkFlowSMSSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowSMSSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> Save(WorkFlowSMS workFlowSMS)
        {
            string storeProcCommand = "WorkFlow_SMS";
            object? param = new { @Action = "Save", workFlowSMS.SmsTemplateId, workFlowSMS.IsPromotionalOrTransactionalType, workFlowSMS.IsTriggerEveryActivity, workFlowSMS.SmsConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(WorkFlowSMS workFlowSMS)
        {
            string storeProcCommand = "WorkFlow_SMS";
            object? param = new { @Action = "Update", workFlowSMS.ConfigureSmsId, workFlowSMS.SmsTemplateId, workFlowSMS.IsPromotionalOrTransactionalType, workFlowSMS.IsTriggerEveryActivity, workFlowSMS.SmsConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<WorkFlowSMS?> GetDetails(int ConfigureSmsId, DateTime? FromDate = null, DateTime? ToDate = null, byte IsSplitTested = 0, string PhoneNumber = null)
        {
            string storeProcCommand = "WorkFlow_SMS";
            object? param = new { @Action = "GetDetails", ConfigureSmsId, FromDate, ToDate, IsSplitTested, PhoneNumber };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowSMS?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<WorkFlowSMS?> GetSmsDetails(int ConfigureSmsId)
        {
            string storeProcCommand = "WorkFlow_SMS";
            object? param = new { @Action = "GetSmsDetails", ConfigureSmsId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowSMS?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async void UpdateWorkflowSmsCampaignSendStatus(int workflowSmsSendingSettingId)
        {
            string storeProcCommand = "WorkFlow_SMS";
            object? param = new { @Action = "UpdateWorkflowSmsCampaignSendStatus", workflowSmsSendingSettingId };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<WorkFlowSMS>> GetRecentWorkflowSmsCampaignsForInterval()
        {
            string storeProcCommand = "WorkFlow_SMS";
            object? param = new { @Action = "GetRecentWorkflowSmsCampaignsForInterval" };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowSMS>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<WorkFlowSMS>> GetRecentWorkflowSmsCampaignsForDailyOnce(int WorkFlowDaysLimit = 0)
        {
            string storeProcCommand = "WorkFlow_SMS";
            object? param = new { @Action = "GetRecentWorkflowSmsCampaignsForDailyOnce", WorkFlowDaysLimit };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowSMS>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
