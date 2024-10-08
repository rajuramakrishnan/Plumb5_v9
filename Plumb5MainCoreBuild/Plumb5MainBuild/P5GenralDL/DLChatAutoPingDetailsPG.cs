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
    public class DLChatAutoPingDetailsPG : CommonDataBaseInteraction, IDLChatAutoPingDetails
    {
        CommonInfo connection = null;
        public DLChatAutoPingDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatAutoPingDetailsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(ChatAutoPingDetails AutoPingDetails)
        {
            string storeProcCommand = "select * from chatautoping_details_save(@URL, @UserId, @Responsed, @PingedMessage)";
            object? param = new { AutoPingDetails.URL, AutoPingDetails.UserId, AutoPingDetails.Responsed, AutoPingDetails.PingedMessage };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);

        }

        public async Task<bool> Update(ChatAutoPingDetails AutoPingDetails)
        {
            string storeProcCommand = "select chatautoping_details_update(@URL, @UserId, @Responsed, @PingedMessage)";
            object? param = new { AutoPingDetails.URL, AutoPingDetails.UserId, AutoPingDetails.Responsed, AutoPingDetails.PingedMessage };

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
