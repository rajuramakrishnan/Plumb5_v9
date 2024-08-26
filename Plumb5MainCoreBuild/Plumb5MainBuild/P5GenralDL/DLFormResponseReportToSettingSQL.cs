using Dapper;
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
    public class DLFormResponseReportToSettingSQL : CommonDataBaseInteraction, IDLFormResponseReportToSetting
    {
        CommonInfo connection = null;
        public DLFormResponseReportToSettingSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFormResponseReportToSettingSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<bool> Save(FormResponseReportToSetting responseSettings)
        {
            string storeProcCommand = "Form_ResponseReportToSetting";
            object? param = new { Action = "Save", responseSettings.FormId, responseSettings.ReportToFormsMailFieldId, responseSettings.ReportToDetailsByMail, responseSettings.ReportToFormsSMSFieldId, responseSettings.ReportToDetailsBySMS, responseSettings.MailOutDependencyFieldId, responseSettings.MailIdList, responseSettings.RedirectUrl, responseSettings.AccesLeadToUserId, responseSettings.ReportToAssignLeadToUserIdFieldId, responseSettings.ReportToDetailsByPhoneCall, responseSettings.GroupId, responseSettings.SmsSendingSettingIdList, responseSettings.WebHooks, responseSettings.WebHooksFinalUrl, responseSettings.SmsOutDependencyFieldId, responseSettings.GroupIdBasedOnOptin, responseSettings.WebHookId, responseSettings.URLParameterResponses, responseSettings.IsRedirectUrlNewWindow, responseSettings.IsOverrideAssignment, responseSettings.ReportToDetailsByWhatsApp, responseSettings.WhatsAppSendingSettingIdList, responseSettings.WhatsAppOutDependencyFieldId, responseSettings.ReportToFormsWhatsAppFieldId, responseSettings.IsOverRideSource, responseSettings.SourceType };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<FormResponseReportToSetting?> Get(int FormId)
        {
            string storeProcCommand = "Form_ResponseReportToSetting";
            object? param = new { Action = "GET", FormId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<FormResponseReportToSetting?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public FormResponseReportToSetting? Gets(int FormId)
        {
            string storeProcCommand = "Form_ResponseReportToSetting";
            object? param = new { Action = "GET", FormId };
            using var db = GetDbConnection(connection.Connection);
            return db.QueryFirstOrDefault<FormResponseReportToSetting?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Delete(Int32 FormId)
        {
            string storeProcCommand = "Form_ResponseReportToSetting";
            object? param = new { Action = "Delete", FormId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
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

