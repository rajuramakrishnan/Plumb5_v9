﻿using Dapper;
using DBInteraction;
using P5GenralML;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLAllConfigURLDetailsPG : CommonDataBaseInteraction, IDLAllConfigURLDetails
    {
        CommonInfo connection;

        public DLAllConfigURLDetailsPG()
        {
            connection = GetDBConnection();
        }
        public async Task<List<AllConfigURL>> Get()
        {
            string storeProcCommand = "select * from all_configurl_get()";
            object? param = new { };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<AllConfigURL>(storeProcCommand, param)).ToList();
        }

        public async Task<Int32> Save(AllConfigURL allConfigURL)
        {
            string storeProcCommand = "select all_configurl_save(@KeyName,@KeyValue)";
            object? param = new { allConfigURL.KeyName, allConfigURL.KeyValue };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
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
