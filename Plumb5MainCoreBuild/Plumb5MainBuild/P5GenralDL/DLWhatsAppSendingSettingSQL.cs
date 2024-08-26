﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLWhatsAppSendingSettingSQL : CommonDataBaseInteraction, IDLWhatsAppSendingSetting
    {
        CommonInfo connection;
        public DLWhatsAppSendingSettingSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<bool> UpdateSentCount(int whatsAppSendingSettingId, int TotalSentCount, int TotalNotSentCount)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "UpdateSentCount", whatsAppSendingSettingId, TotalSentCount, TotalNotSentCount };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<WhatsAppSendingSetting?> Get(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "Get", whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppSendingSetting>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<WhatsAppSendingSetting>> GetListforapi(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "GetList", whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppSendingSetting>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<bool> CheckIdentifier(string IdentifierName)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "CheckIdentifier", IdentifierName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> Save(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "Save", whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId, whatsappSendingSetting.CampaignId, whatsappSendingSetting.ScheduledDate, whatsappSendingSetting.ScheduledStatus, whatsappSendingSetting.TotalContact, whatsappSendingSetting.IsWhatsAppOpted, whatsappSendingSetting.WhatsAppConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<WhatsAppSendingSetting>> GetRecentwhatsappCampaignsForDailyOnce(int DaysLimit)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "", DaysLimit };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppSendingSetting>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<WhatsAppSendingSetting>> GetRecentWhatsappCampaignsForInterval()
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "GetRecentwhatsAppCampaignsForDailyOnce" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppSendingSetting>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task UpdatewhatsappCamapignSentStatus(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "UpdateWhatsAppCampaignSendStatus", whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "Delete", whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> UpdateScheduledCampaign(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "UpdateScheduledCampaign", whatsappSendingSetting.Id, whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId, whatsappSendingSetting.CampaignId, whatsappSendingSetting.ScheduledDate, whatsappSendingSetting.ScheduledStatus, whatsappSendingSetting.TotalContact, whatsappSendingSetting.IsWhatsAppOpted, whatsappSendingSetting.WhatsAppConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<Int32> SaveForForms(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "SaveForForms", whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> UpdateStoppedErrorStatus(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "UpdateStoppedErrorStatus", whatsappSendingSetting.Id, whatsappSendingSetting.StoppedReason, whatsappSendingSetting.ScheduledStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> Update(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "WhatsApp_SendingSetting";
            object? param = new { Action = "Update", whatsappSendingSetting.Id, whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
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
