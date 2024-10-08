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
    public class DLWhatsAppClickUrlPG : CommonDataBaseInteraction, IDLWhatsAppClickUrl
    {
        CommonInfo connection;
        public DLWhatsAppClickUrlPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsAppClickUrlPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> MaxCount(MLWhatsAppClickUrl sendingSettingId)
        {
            string storeProcCommand = "select * from whatsapp_clickurlcontent_clickurlmaxcount(@WhatsAppSendingSettingId)";
            object? param = new { sendingSettingId.WhatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<MLWhatsAppClickUrl>> GetResponseData(MLWhatsAppClickUrl sendingSettingId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "select * from whatsapp_clickurlcontent_clickurldata(@WhatsAppSendingSettingId,@OffSet, @FetchNext)";
            object? param = new { sendingSettingId.WhatsAppSendingSettingId, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppClickUrl>(storeProcCommand, param)).ToList();

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
