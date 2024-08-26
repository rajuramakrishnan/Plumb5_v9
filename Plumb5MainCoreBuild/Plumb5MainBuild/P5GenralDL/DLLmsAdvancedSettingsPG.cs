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
    public class DLLmsAdvancedSettingsPG : CommonDataBaseInteraction, IDLLmsAdvancedSettings
    {
        CommonInfo connection;
        public DLLmsAdvancedSettingsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsAdvancedSettingsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> saveupdate(MLLmsAdvancedSettings MLLmsAdvancedSettings)
        {
            string storeProcCommand = "select lmsadvancesetting_saveupdate(@Key,@Value)";
            object? param = new { MLLmsAdvancedSettings.Key, MLLmsAdvancedSettings.Value };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param);
        }

        public async Task<List<MLLmsAdvancedSettings>> GetDetailsAdvancedSettings(string key)
        {
            string storeProcCommand = "select * from lmsadvancesetting_getdetails(@key)";
            object? param = new { key };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLLmsAdvancedSettings>(storeProcCommand, param)).ToList();
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
