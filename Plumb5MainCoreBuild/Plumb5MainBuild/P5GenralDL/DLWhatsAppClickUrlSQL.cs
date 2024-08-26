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
    public class DLWhatsAppClickUrlSQL : CommonDataBaseInteraction, IDLWhatsAppClickUrl
    {
        CommonInfo connection;
        public DLWhatsAppClickUrlSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsAppClickUrlSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> MaxCount(MLWhatsAppClickUrl sendingSettingId)
        {
            string storeProcCommand = "WhatsApp_ClickUrlContent";
            object? param = new { Action= "ClickUrlMaxCount", sendingSettingId.WhatsAppSendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<MLWhatsAppClickUrl>> GetResponseData(MLWhatsAppClickUrl sendingSettingId, int OffSet, int FetchNext)
        {
            string storeProcCommand = "WhatsApp_ClickUrlContent";
            object? param = new { Action = "ClickUrlData", sendingSettingId.WhatsAppSendingSettingId, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLWhatsAppClickUrl>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

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
