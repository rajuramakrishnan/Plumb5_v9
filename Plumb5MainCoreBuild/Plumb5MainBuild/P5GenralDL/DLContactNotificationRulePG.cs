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
    public class DLContactNotificationRulePG : CommonDataBaseInteraction, IDLContactNotificationRule
    {
        CommonInfo connection;
        public DLContactNotificationRulePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactNotificationRulePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> SaveContactNotificationRule(ContactNotificationRule leadnotificToSales)
        {
            try
            {
                string storeProcCommand = "select contact_notificationrule_save(@Name, @Mail, @Sms, @Conditions, @AssignUserInfoUserId, @AssignUserGroupId, @AutoMailSendingSettingId, @AutoSmsSendingSettingId, @AutoAssignToGroupId, @WhatsApp, @AutoWhatsAppSendingSettingId, @AutoAssignToLmsSourceId)";
                object? param = new { leadnotificToSales.Name, leadnotificToSales.Mail, leadnotificToSales.Sms, leadnotificToSales.Conditions, leadnotificToSales.AssignUserInfoUserId, leadnotificToSales.AssignUserGroupId, leadnotificToSales.AutoMailSendingSettingId, leadnotificToSales.AutoSmsSendingSettingId, leadnotificToSales.AutoAssignToGroupId, leadnotificToSales.WhatsApp, leadnotificToSales.AutoWhatsAppSendingSettingId, leadnotificToSales.AutoAssignToLmsSourceId };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            
        }

        public async Task<bool> UpdateContactNotificationRule(ContactNotificationRule leadnotificToSales)
        {
            string storeProcCommand = "select contact_notificationrule_update(@Id,@Name, @Mail, @Sms, @Conditions, @AssignUserInfoUserId, @AssignUserGroupId, @AutoMailSendingSettingId, @AutoSmsSendingSettingId, @AutoAssignToGroupId, @WhatsApp, @AutoWhatsAppSendingSettingId, @AutoAssignToLmsSourceId)";
            object? param = new { leadnotificToSales.Id, leadnotificToSales.Name, leadnotificToSales.Mail, leadnotificToSales.Sms, leadnotificToSales.Conditions, leadnotificToSales.AssignUserInfoUserId, leadnotificToSales.AssignUserGroupId, leadnotificToSales.AutoMailSendingSettingId, leadnotificToSales.AutoSmsSendingSettingId, leadnotificToSales.AutoAssignToGroupId, leadnotificToSales.WhatsApp, leadnotificToSales.AutoWhatsAppSendingSettingId, leadnotificToSales.AutoAssignToLmsSourceId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param) > 0;
        }

        public async Task<List<ContactNotificationRule>> GetRuleNotification(int Id, bool? Status = null)
        {
            string storeProcCommand = "select * from contact_notificationrule_get(@Id, @Status)";
            object? param = new { Id, Status };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactNotificationRule>(storeProcCommand, param)).ToList();
        }

        public async Task<bool> UpdateLastAssigUserIdNotificationToSales(int Id, int UserInfoUserId)
        {
            string storeProcCommand = "select contact_notificationrule_updatelastassigneduserid(@Id, @UserInfoUserId)";
            object? param = new { Id, UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param) > 0;
        }

        public async Task<bool> DeleteLeadNotificationToSales(int Id)
        {
            string storeProcCommand = "select contact_notificationrule_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param) > 0;
        }

        public async Task<bool> ToogleStatus(int Id, bool Status)
        {
            string storeProcCommand = "select contact_notificationrule_tooglestatus(@Id, @Status)";
            object? param = new { Id, Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param) > 0;
        }

        public async Task<bool> ChangePriority(int Id, Int16 RulePriority)
        {
            string storeProcCommand = "select contact_notificationrule_changepriority(@Id, @RulePriority)";
            object? param = new { Id, RulePriority };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param) > 0;
        }

        public async Task<List<ContactNotificationRule>> GetRules()
        {
            string storeProcCommand = "select * from contact_notificationrule_getlist()";

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactNotificationRule>(storeProcCommand)).ToList();
        }

        public async Task<int> GetMaxCount(string ruleName)
        {
            string storeProcCommand = "select contact_notificationrule_maxcount(@ruleName)";
            object? param = new { ruleName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<ContactNotificationRule>> GetRulesForBinding(int OffSet, int FetchNext, string ruleName)
        {
            try
            {
                string storeProcCommand = "select * from contact_notificationrule_getrulesforbinding(@ruleName, @OffSet, @FetchNext)";
                object? param = new { ruleName, OffSet, FetchNext };

                using var db = GetDbConnection(connection.Connection);
                return (await db.QueryAsync<ContactNotificationRule>(storeProcCommand, param)).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
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
