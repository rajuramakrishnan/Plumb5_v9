﻿using P5GenralML;
using IP5GenralDL;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using DBInteraction;
using Dapper;
using System.Threading.Tasks;


namespace P5GenralDL
{
    public class DLApiImportResponseSettingPG : CommonDataBaseInteraction, IDLApiImportResponseSetting
    {
        CommonInfo connection = null;
        public DLApiImportResponseSettingPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<bool> Save(ApiImportResponseSetting responseSettings)
        {
            string storeProcCommand = "select apiimport_responsesetting_save(@Id, @Name, @Status, @ReportToDetailsByMail, @ReportToDetailsBySMS, @ReportToDetailsByWhatsApp, @MailSendingSettingId, @SmsSendingSettingId, @WhatsAppSendingSettingId, @AssignLeadToUserInfoUserId, @IsOverrideAssignment, @AssignToGroupId, @WebHookId, @URLParameterResponses, @IsVerifiedEmail, @IsAutoWhatsApp, @AssignUserGroupId, @IsOverRideSource, @IsConditional, @ConditionalJson, @SourceType, @IsUserAssignmentConditional, @UserAssigmentJson, @MailSendingConditonalJson, @SmsSendingConditonalJson, @WhatsAppSendingConditonalJson, @IsAutoClickToCall, @IsMailRepeatCon, @IsSmsRepeatCon, @IsWhatsappRepeatCon, @IsCallRepeatCon, @AssignStage, @AssignStageConditonalJson, @IsIncludedLeads, @IncludedLeadsJson, @IsExcludedLeads, @ExcludedLeadsJson, @AssignToGroupConditonalJson, @IsUnConditionalGroupSticky)";
            object? param = new { responseSettings.Id, responseSettings.Name, responseSettings.Status, responseSettings.ReportToDetailsByMail, responseSettings.ReportToDetailsBySMS, responseSettings.ReportToDetailsByWhatsApp, responseSettings.MailSendingSettingId, responseSettings.SmsSendingSettingId, responseSettings.WhatsAppSendingSettingId, responseSettings.AssignLeadToUserInfoUserId, responseSettings.IsOverrideAssignment, responseSettings.AssignToGroupId, responseSettings.WebHookId, responseSettings.URLParameterResponses, responseSettings.IsVerifiedEmail, responseSettings.IsAutoWhatsApp, responseSettings.AssignUserGroupId, responseSettings.IsOverRideSource, responseSettings.IsConditional, responseSettings.ConditionalJson, responseSettings.SourceType, responseSettings.IsUserAssignmentConditional, responseSettings.UserAssigmentJson, responseSettings.MailSendingConditonalJson, responseSettings.SmsSendingConditonalJson, responseSettings.WhatsAppSendingConditonalJson, responseSettings.IsAutoClickToCall, responseSettings.IsMailRepeatCon, responseSettings.IsSmsRepeatCon, responseSettings.IsWhatsappRepeatCon, responseSettings.IsCallRepeatCon, responseSettings.AssignStage, responseSettings.AssignStageConditonalJson, responseSettings.IsIncludedLeads, responseSettings.IncludedLeadsJson, responseSettings.IsExcludedLeads, responseSettings.ExcludedLeadsJson, responseSettings.AssignToGroupConditonalJson, responseSettings.IsUnConditionalGroupSticky };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> MaxCount(string Name)
        {
            string storeProcCommand = "select * from apiimport_responsesetting_maxcount(@Name)";
            object? param = new { Name };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<ApiImportResponseSetting>> Get(int FetchNext, int OffSet, string Name)
        {
            string storeProcCommand = "select * from apiimport_responsesetting_get(@OffSet,@FetchNext,@Name )";
            object? param = new { OffSet, FetchNext, Name };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<ApiImportResponseSetting>(storeProcCommand, param);
        }

        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "select apiimport_responsesetting_delete(@Id)";
            object? param = new { Id };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> ToggleStatus(int Id, bool Status)
        {
            string storeProcCommand = "select apiimport_responsesetting_togglestatus( @Id, @Status )";
            object? param = new { Id, Status };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<ApiImportResponseSetting?> Get(string Name)
        {
            string storeProcCommand = "select * from apiimport_responsesetting_getbyname(@Name)";
            object? param = new { Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ApiImportResponseSetting?>(storeProcCommand, param);
        }
        public async Task<bool> UpdateLastAssigUserIdNotificationToSales(int Id, int UserInfoUserId)
        {
            string storeProcCommand = "select apiimport_responsesetting_updatelastuserinfouserid(@Id,@UserInfoUserId)";
            object? param = new { Id, UserInfoUserId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<IEnumerable<ApiImportResponseSetting>> GetNames()
        {
            string storeProcCommand = "select * from apiimport_responsesetting_getnames()";
            object? param = new { };
            List<string> fields = new List<string> { "Name" };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<ApiImportResponseSetting>(storeProcCommand, param);
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
