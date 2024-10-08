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
    public class DLWhatsAppCampaignPG : CommonDataBaseInteraction, IDLWhatsAppCampaign
    {
        CommonInfo connection;
        public DLWhatsAppCampaignPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsAppCampaignPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(WhatsAppCampaign WhatsAppCampaign)
        {
            string storeProcCommand = "select * from whatsapp_campaign_save(@.ame, @UserInfoUserId, @UserGroupId)";
            object? param = new { WhatsAppCampaign.Name, WhatsAppCampaign.UserInfoUserId, WhatsAppCampaign.UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Update(WhatsAppCampaign WhatsAppCampaign)
        {
            string storeProcCommand = "select * from whatsapp_campaign_update(@Name,@Id)";
            object? param = new { WhatsAppCampaign.Name, WhatsAppCampaign.Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "select * from whatsapp_campaign_delete(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
        public async Task<List<WhatsAppCampaign>> GetCustomisedCampaignDetails(IEnumerable<int> ListOfId, List<string> fieldName)
        {
            string storeProcCommand = "select * from whatsapp_campaign_get(@ListOfId,@fieldName)";
            object? param = new { ListOfId = string.Join(",", new List<int>(ListOfId).ToArray()), fieldName = fieldName != null ? string.Join(",", fieldName.ToArray()) : null };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WhatsAppCampaign>(storeProcCommand, param)).ToList();

        }
        public async Task<int> MaxCount(WhatsAppCampaign WhatsAppCampaign)
        {
            string storeProcCommand = "select * from whatsapp_campaign_maxcount(@Name)";
            object? param = new { WhatsAppCampaign.Name };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

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
