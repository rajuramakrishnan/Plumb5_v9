﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;
using Dapper;

namespace P5GenralDL
{
    public class DLIpligenceStateListSQL : CommonDataBaseInteraction, IDLIpligenceStateList
    {
        CommonInfo connection;
        public DLIpligenceStateListSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLIpligenceStateListSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<List<IpligenceDAS>> GetStateList(string StateName)
        {
            string storeProcCommand = "Ipligence_StateList";
            object? param = new { @Action = "GetStateList", StateName };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<IpligenceDAS>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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