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
    public class DLWorkFlowMailSQL : CommonDataBaseInteraction, IDLWorkFlowMail
    {
        CommonInfo connection;
        public DLWorkFlowMailSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowMailSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> Save(WorkFlowMail workFlowMail)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "Save", workFlowMail.MailTemplateId, workFlowMail.MailSubject, workFlowMail.FromName, workFlowMail.FromEmailId, workFlowMail.ReplyToEmailId, workFlowMail.Subscribe, workFlowMail.IsPromotionalOrTransactionalType, workFlowMail.IsTriggerEveryActivity, workFlowMail.MailConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Update(WorkFlowMail workFlowMail)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "Update", workFlowMail.ConfigureMailId, workFlowMail.MailTemplateId, workFlowMail.MailSubject, workFlowMail.FromName, workFlowMail.FromEmailId, workFlowMail.ReplyToEmailId, workFlowMail.Subscribe, workFlowMail.IsPromotionalOrTransactionalType, workFlowMail.IsTriggerEveryActivity, workFlowMail.MailConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }


        public async Task<WorkFlowMail?> GetDetails(int ConfigureMailId, DateTime? FromDate, DateTime? ToDate, byte IsSplitTested, string EmailId)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "GetDetails", ConfigureMailId, FromDate, ToDate, IsSplitTested, EmailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowMail>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<WorkFlowMail?> GetMailDetails(int ConfigureMailId)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "GetMailDetails", ConfigureMailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowMail>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Delete(int ConfigureMailId)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "Delete", ConfigureMailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<WorkFlowMail?> GetIsStopped(int ConfigureMailId)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "GetCampaignToStop", ConfigureMailId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlowMail>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<DataSet> GetOverAllCountByUserId(int WorkFlowId, int ContactId, int ConfigureMailId, DateTime? FromDate, DateTime? ToDate)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "GetOverAllCountByUserId", WorkFlowId, ContactId, ConfigureMailId, FromDate, ToDate };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task UpdateWorkflowMailCampaignSendStatus(int worflowMailSendingSettingId)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "UpdateWorkflowMailCampaignSendStatus", worflowMailSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);


        }

        public async Task<List<WorkFlowMail>> GetRecentWorkflowMailCampaignsForInterval()
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "GetRecentWorkflowMailCampaignsForInterval" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowMail>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<WorkFlowMail>> GetRecentWorkflowMailCampaignsForDailyOnce(int WorkFlowDaysLimit)
        {
            string storeProcCommand = "WorkFlow_Mail";
            object? param = new { Action = "GetRecentWorkflowMailCampaignsForDailyOnce", WorkFlowDaysLimit };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowMail>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

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
