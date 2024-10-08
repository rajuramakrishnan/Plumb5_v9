﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    public class DLChatAutoPingDetailsSQL : CommonDataBaseInteraction, IDLChatAutoPingDetails
    {
        CommonInfo connection = null;
        public DLChatAutoPingDetailsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLChatAutoPingDetailsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(ChatAutoPingDetails AutoPingDetails)
        {
            string storeProcCommand = "ChatAutoPing_Details";
            object? param = new { Action = "Save", AutoPingDetails.URL, AutoPingDetails.UserId, AutoPingDetails.Responsed, AutoPingDetails.PingedMessage };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);

        }

        public async Task<bool> Update(ChatAutoPingDetails AutoPingDetails)
        {
            string storeProcCommand = "ChatAutoPing_Details";
            object? param = new { Action = "Update", AutoPingDetails.URL, AutoPingDetails.UserId, AutoPingDetails.Responsed, AutoPingDetails.PingedMessage };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param) > 0;
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
