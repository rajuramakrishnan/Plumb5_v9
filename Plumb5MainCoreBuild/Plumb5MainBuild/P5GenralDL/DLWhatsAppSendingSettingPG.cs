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
    public class DLWhatsAppSendingSettingPG : CommonDataBaseInteraction, IDLWhatsAppSendingSetting
    {
        CommonInfo connection;
        public DLWhatsAppSendingSettingPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<bool> UpdateSentCount(int whatsAppSendingSettingId, int TotalSentCount, int TotalNotSentCount)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_updatesentcount(@whatsAppSendingSettingId, @TotalSentCount, @TotalNotSentCount)";
            object? param = new { whatsAppSendingSettingId, TotalSentCount, TotalNotSentCount };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<WhatsAppSendingSetting?> Get(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_get(@whatsAppSendingSettingId)";
            object? param = new { whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppSendingSetting>(storeProcCommand, param);
        }
        public async Task<List<WhatsAppSendingSetting>> GetListforapi(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_getlist(@whatsAppSendingSettingId)";
            object? param = new { whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppSendingSetting>(storeProcCommand, param)).ToList();
        }
        public async Task<bool> CheckIdentifier(string IdentifierName)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_checkidentifier(@IdentifierName)";
            object? param = new { IdentifierName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> Save(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_save(@UserInfoUserId, @UserGroupId, @Name, @WhatsAppTemplateId, @GroupId, @CampaignId, @ScheduledDate, @ScheduledStatus, @TotalContact, @IsWhatsAppOpted, @WhatsAppConfigurationNameId)";
            object? param = new { whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId, whatsappSendingSetting.CampaignId, whatsappSendingSetting.ScheduledDate, whatsappSendingSetting.ScheduledStatus, whatsappSendingSetting.TotalContact, whatsappSendingSetting.IsWhatsAppOpted, whatsappSendingSetting.WhatsAppConfigurationNameId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<WhatsAppSendingSetting>> GetRecentwhatsappCampaignsForDailyOnce(int DaysLimit)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_getrecentwhatsappcampaignsfordailyonce(@DaysLimit)";
            object? param = new { DaysLimit };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppSendingSetting>(storeProcCommand, param)).ToList();
        }

        public async Task<List<WhatsAppSendingSetting>> GetRecentWhatsappCampaignsForInterval()
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_getrecentwhatsappcampaignsforinterval()";

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppSendingSetting>(storeProcCommand)).ToList();
        }

        public async Task UpdatewhatsappCamapignSentStatus(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_updatewhatsappcampaignsendstatus(@whatsAppSendingSettingId)";
            object? param = new { whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Delete(int whatsAppSendingSettingId)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_delete(@whatsAppSendingSettingId)";
            object? param = new { whatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> UpdateScheduledCampaign(WhatsAppSendingSetting whatsappSendingSetting)
        {
            try
            {
                string storeProcCommand = "select * from whatsapp_sendingsetting_updatescheduledcampaign(@Id, @UserInfoUserId, @UserGroupId, @Name, @WhatsAppTemplateId, @GroupId, @CampaignId, @ScheduledDate, @ScheduledStatus::smallint, @TotalContact, @IsWhatsAppOpted::smallint, @WhatsAppConfigurationNameId)";
                object? param = new { whatsappSendingSetting.Id, whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId, whatsappSendingSetting.CampaignId, whatsappSendingSetting.ScheduledDate, whatsappSendingSetting.ScheduledStatus, whatsappSendingSetting.TotalContact, whatsappSendingSetting.IsWhatsAppOpted, whatsappSendingSetting.WhatsAppConfigurationNameId };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Int32> SaveForForms(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_saveforforms(@UserInfoUserId, @UserGroupId, @Name, @WhatsAppTemplateId, @GroupId)";
            object? param = new { whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }
        public async Task<bool> UpdateStoppedErrorStatus(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_updatestoppederrorstatus(@Id, @StoppedReason, @ScheduledStatus)";
            object? param = new { whatsappSendingSetting.Id, whatsappSendingSetting.StoppedReason, whatsappSendingSetting.ScheduledStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> Update(WhatsAppSendingSetting whatsappSendingSetting)
        {
            string storeProcCommand = "select * from whatsapp_sendingsetting_update(@Id, @UserInfoUserId, @UserGroupId, @Name, @WhatsAppTemplateId, @GroupId)";
            object? param = new { whatsappSendingSetting.Id, whatsappSendingSetting.UserInfoUserId, whatsappSendingSetting.UserGroupId, whatsappSendingSetting.Name, whatsappSendingSetting.WhatsAppTemplateId, whatsappSendingSetting.GroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
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
