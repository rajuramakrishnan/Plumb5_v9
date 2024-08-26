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
    public class DLIpAddressDetailsPG : CommonDataBaseInteraction, IDLIpAddressDetails
    {
        CommonInfo connection;
        public DLIpAddressDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLIpAddressDetailsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<IpligenceDAS?> IpAddressBelongsToINRorUSD(string IpAddress)
        {
            string storeProcCommand = "select * from lead_notification_get(@IpAddress)";
            List<string> paramName = new List<string> { IpAddress };
            object? param = new { };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<IpligenceDAS?>(storeProcCommand, param);
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


