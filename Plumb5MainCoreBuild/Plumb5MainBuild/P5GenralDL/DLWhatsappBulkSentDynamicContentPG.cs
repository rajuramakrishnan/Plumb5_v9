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
    public class DLWhatsappBulkSentDynamicContentPG : CommonDataBaseInteraction, IDLWhatsappBulkSentDynamicContent
    {
        CommonInfo connection;

        public DLWhatsappBulkSentDynamicContentPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async  void DeleteMessageContent(DataTable AllData)
        {
            string storeProcCommand = "select whatsapp_bulksentdynamiccontent_deletemessagecontent(@AllData)";
            
            object? param = new { AllData };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> UpdateMessageContent(DataTable AllMessageContent)
        {
            string storeProcCommand = "select whatsapp_bulksentdynamiccontent_updatemessagecontent(@AllMessageContent)"; 
            object? param = new { AllMessageContent };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<IEnumerable<WhatsappBulkSent>> GetDetailsForMessageUpdate(int WhatsappSendingSettingId)
        {
            string storeProcCommand = "select * from whatsapp_bulksentdynamiccontent_getdetailsformessageupdate(@WhatsappSendingSettingId)";
            
            object? param = new { WhatsappSendingSettingId };
            List<string> fields = new List<string>() { "Id", "WhatsappSendingSettingId", "WhatsappTemplateId", "ContactId", "PhoneNumber", "MessageContent", "UserAttributes", "ButtonOneDynamicURLSuffix", "ButtonTwoDynamicURLSuffix", "MediaFileURL", "P5WhatsappUniqueID" };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WhatsappBulkSent>(storeProcCommand, param);
        }

        public async Task<IEnumerable<WhatsAppSendingSetting>> GetBulkAppSendingSettingList(int SendStatus)
        {
            string storeProcCommand = "select * from whatsapp_bulksentdynamiccontent_getbulkwhatsappsendingids(@SendStatus)";  
            object? param = new { SendStatus };
             
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WhatsAppSendingSetting>(storeProcCommand, param);
        }

        public async Task<WhatsAppSendingSetting?> GetDetail(int Id)
        {
            string name = "";
            int whatsapptemplateid = 0;
            int groupid = 0;
            string storeProcCommand = "select * from whatsapp_sendingsetting_get(@Id,@name,@whatsapptemplateid,@groupid)"; 
            object? param = new { Id, name, whatsapptemplateid, groupid };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WhatsAppSendingSetting?>(storeProcCommand, param);
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
