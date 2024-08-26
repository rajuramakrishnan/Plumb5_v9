﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWorkFlowMailPG : CommonDataBaseInteraction, IDLWorkFlowMail
    {
        CommonInfo connection;
        public DLWorkFlowMailPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowMailPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WorkFlowMail workFlowMail)
        {
            string storeProcCommand = "select * from workflow_mail_save( @MailTemplateId, @MailSubject, @FromName, @FromEmailId, @ReplyToEmailId, @Subscribe, @IsPromotionalOrTransactionalType, @IsTriggerEveryActivity, @MailConfigurationNameId)";
            object? param = new { workFlowMail.MailTemplateId, workFlowMail.MailSubject, workFlowMail.FromName, workFlowMail.FromEmailId, workFlowMail.ReplyToEmailId, workFlowMail.Subscribe, workFlowMail.IsPromotionalOrTransactionalType, workFlowMail.IsTriggerEveryActivity, workFlowMail.MailConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Update(WorkFlowMail workFlowMail)
        {
            string storeProcCommand = "select * from workflow_mail_update(@ConfigureMailId, @MailTemplateId, @MailSubject, @FromName, @FromEmailId, @ReplyToEmailId, @Subscribe, @IsPromotionalOrTransactionalType, @IsTriggerEveryActivity, @MailConfigurationNameId)";
            object? param = new { workFlowMail.ConfigureMailId, workFlowMail.MailTemplateId, workFlowMail.MailSubject, workFlowMail.FromName, workFlowMail.FromEmailId, workFlowMail.ReplyToEmailId, workFlowMail.Subscribe, workFlowMail.IsPromotionalOrTransactionalType, workFlowMail.IsTriggerEveryActivity, workFlowMail.MailConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }


        public async Task<WorkFlowMail?> GetDetails(int ConfigureMailId, DateTime? FromDate = null, DateTime? ToDate = null, byte IsSplitTested = 0, string EmailId = null)
        {
            string storeProcCommand = "select * from workflow_mail_getdetails(@ConfigureMailId, @FromDate, @ToDate, @IsSplitTested, @EmailId)";
            object? param = new { ConfigureMailId, FromDate, ToDate, IsSplitTested, EmailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowMail>(storeProcCommand, param);

        }

        public async Task<WorkFlowMail?> GetMailDetails(int ConfigureMailId)
        {
            string storeProcCommand = "select * from workflow_mail_getmaildetails(@ConfigureMailId)";
            object? param = new { ConfigureMailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowMail>(storeProcCommand, param);

        }

        public async Task<bool> Delete(int ConfigureMailId)
        {
            string storeProcCommand = "select * from UpdateScore(@Action,@ConfigureMailId)";
            object? param = new { Action = "Delete", ConfigureMailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

        public async Task<WorkFlowMail?> GetIsStopped(int ConfigureMailId)
        {
            string storeProcCommand = "select * from WorkFlow_Mail(@Action,@ConfigureMailId)";
            object? param = new { Action = "GetCampaignToStop", ConfigureMailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowMail>(storeProcCommand, param);

        }

        public async Task<DataSet> GetOverAllCountByUserId(int WorkFlowId, int ContactId, int ConfigureMailId, DateTime? FromDate, DateTime? ToDate)
        {
            string storeProcCommand = "select * from WorkFlow_Mail(@Action,@ WorkFlowId, @ContactId, @ConfigureMailId, @FromDate, @ToDate)";
            object? param = new { Action = "GetOverAllCountByUserId", WorkFlowId, ContactId, ConfigureMailId, FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task UpdateWorkflowMailCampaignSendStatus(int worflowMailSendingSettingId)
        {
            string storeProcCommand = "select * from workflow_mail_updateworkflowmailcampaignsendstatus(@worflowMailSendingSettingId)";
            object? param = new { worflowMailSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<WorkFlowMail>> GetRecentWorkflowMailCampaignsForInterval()
        {
            string storeProcCommand = "select * from workflow_mail_getrecentworkflowmailcampaignsforinterval()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowMail>(storeProcCommand, param)).ToList();

        }

        public async Task<List<WorkFlowMail>> GetRecentWorkflowMailCampaignsForDailyOnce(int WorkFlowDaysLimit=0)
        {
            string storeProcCommand = "select * from WorkFlow_Mail(@Action)";
            object? param = new { Action = "GetRecentWorkflowMailCampaignsForDailyOnce" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowMail>(storeProcCommand, param)).ToList();

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