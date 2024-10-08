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
    public class DLWorkFlowSendingMailSmsPG : CommonDataBaseInteraction, IDLWorkFlowSendingMailSms
    {
        CommonInfo connection;
        public DLWorkFlowSendingMailSmsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowSendingMailSmsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<MLMailSent>> GetMailBulkDetails(MLMailSent mailSent, int MaxLimit)
        {
            string storeProcCommand = "select * from workflow_mailbulkinsert_getmailbulkdetails(@SendStatus, @MailSendingSettingId, @WorkFlowId, @MaxLimit)";
            object? param = new { mailSent.SendStatus, mailSent.MailSendingSettingId, mailSent.WorkFlowId, MaxLimit };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLMailSent>(storeProcCommand, param)).ToList();
        }

        public async Task<bool> SaveToMailSent(DataTable mailSent)
        {
            string storeProcCommand = "select * from workflow_mailbulkinsert_savetomailsent(@MailSent)";
            object? param = new { mailSent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> UpdateTotalCounts(DataTable mailSent, int MailSendingSettingId)
        {
            string storeProcCommand = "select * from workflow_mailbulkinsert_updatetotalcounts(@MailSent,@MailSendingSettingId)";
            object? param = new { mailSent, MailSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<Int32> DeleteFromWorkFlowBulkMailSent(DataTable mailSent)
        {
            string storeProcCommand = "select * from workflow_mailbulkinsert_deletefromworkflowbulkmailsent( @MailSent)";
            object? param = new { mailSent };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<SmsSent>> GetSmsBulkDetails(int SmsSendingSettingId, int WorkFlowId, Int16 SendStatus, int MaxLimit)
        {
            string storeProcCommand = "select * from workflow_smsbulkinsert_getsmsbulkdetails(@SmsSendingSettingId, @WorkFlowId, @SendStatus, @MaxLimit)";
            object? param = new { SmsSendingSettingId, WorkFlowId, SendStatus, MaxLimit };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<SmsSent>(storeProcCommand, param)).ToList();
        }
    }
}
