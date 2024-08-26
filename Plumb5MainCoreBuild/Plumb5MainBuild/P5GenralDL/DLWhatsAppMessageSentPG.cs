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
    public class DLWhatsAppMessageSentPG : CommonDataBaseInteraction, IDLWhatsAppMessageSent
    {
        CommonInfo connection;
        public DLWhatsAppMessageSentPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWhatsAppMessageSentPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<DataSet> GetOpenAndClickedRate(string GroupIds)
        {
            string storeProcCommand = "select * from Sms_Sent(@Action,@GroupIds)";
            object? param = new { Action = "GetDeliveredAndClickedRate", GroupIds };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
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
