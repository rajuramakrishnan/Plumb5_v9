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
    public class DLWorkFlowBulkWhatsAppSentPG : CommonDataBaseInteraction, IDLWorkFlowBulkWhatsAppSent
    {
        CommonInfo connection;
        public DLWorkFlowBulkWhatsAppSentPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<bool> DeleteAllTheDataWhichAreInQuque(int WorkFlowId)
        {
            string storeProcCommand = "select  workflow_whatsappbulkinsert_deleteallthedatawhichareinquque(@WorkFlowId)";
             
            object? param = new {  WorkFlowId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<long> GetTotalBulkWhatsApp(int WhatsAppSendingSettingId, int WorkFlowId)
        {
            string storeProcCommand = "select workflow_whatsappbulkinsert_gettotalbulkwhatsapp( @WhatsAppSendingSettingId, @WorkFlowId)";
             
            object? param = new { WhatsAppSendingSettingId, WorkFlowId };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<long>(storeProcCommand, param)  ;
        }
        public async Task<IEnumerable<WorkFlowBulkWhatsAppSent>> GetDetailsForMessageUpdate(WorkFlowBulkWhatsAppSent whatsAppSent)
        {
            string storeProcCommand = "select * from workflow_whatsappsent_getdetailsformessageupdate(@WhatsappSendingSettingId, @WorkFlowId, @WorkFlowDataId)";
             
            object? param = new { whatsAppSent.WhatsappSendingSettingId, whatsAppSent.WorkFlowId, whatsAppSent.WorkFlowDataId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowBulkWhatsAppSent>(storeProcCommand, param);
        }
        public async Task<IEnumerable<WorkFlowBulkWhatsAppSent>> GetSendingSettingIds(WorkFlowBulkWhatsAppSent whatsAppSent)
        {
            string storeProcCommand = "select * from workflow_whatsappsent_getsendingsettingids(@SendStatus)"; 
            object? param = new { whatsAppSent.SendStatus };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowBulkWhatsAppSent>(storeProcCommand, param);
        }
        public async void UpdateMessageContent(DataTable AllData)
        {
            string storeProcCommand = "select workflow_whatsappsent_updatemessagecontent(@AllData)"; 
            object? param = new { AllData };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);


        }
        public async void DeleteMessageContent(DataTable AllData)
        {
            string storeProcCommand = "workflow_whatsappsent_deletemessagecontent(@AllData)"; 
            object? param = new { AllData };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
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
