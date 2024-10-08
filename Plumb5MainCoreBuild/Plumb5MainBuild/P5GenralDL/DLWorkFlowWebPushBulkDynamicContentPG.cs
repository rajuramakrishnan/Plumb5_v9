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
    public class DLWorkFlowWebPushBulkDynamicContentPG : CommonDataBaseInteraction, IDLWorkFlowWebPushBulkDynamicContent
    {
        CommonInfo connection;

        public DLWorkFlowWebPushBulkDynamicContentPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowWebPushBulkDynamicContentPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<IEnumerable<WorkFlowWebPushBulk>> GetBulkpushSendingSettingList(Int16 SendStatus)
        {
            string storeProcCommand = "select * from workflow_webpushbulkdynamiccontent_getbulkwebpushsendingids(@SendStatus)"; 
            object? param = new { SendStatus };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowWebPushBulk>(storeProcCommand, param);
        }

        public async Task<IEnumerable<WorkFlowWebPushBulk>> GetDetailsForMessageUpdate(int WebPushSendingSettingId)
        {
            string storeProcCommand = "select * from workflow_webpushbulkdynamiccontent_getdetailsformessageupdate(@WebPushSendingSettingId)";
             
            object? param = new { WebPushSendingSettingId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<WorkFlowWebPushBulk>(storeProcCommand, param);
        }

        public async void UpdateMessageContent(DataTable AllData)
        {
            string storeProcCommand = "select workflow_webpushbulkdynamiccontent_updatemessagecontent(@AllData)"; 
            object? param = new { AllData };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async void DeleteMessageContent(DataTable AllData)
        {
            string storeProcCommand = "select workflow_webpushbulkdynamiccontent_deletemessagecontent(@AllData)"; 
            object? param = new { AllData };
            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param);
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
