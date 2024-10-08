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
    public class DLFacebookTokenSQL : CommonDataBaseInteraction, IDLFacebookToken
    {
        CommonInfo connection = null;
        public DLFacebookTokenSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLFacebookTokenSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(FacebookToken fbToken)
        {
            string storeProcCommand = "Facebook_Token";
            object? param = new { Action = "Save", fbToken.Token };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<FacebookToken?> Get()
        {
            string storeProcCommand = "Facebook_Token";
            object? param = new { Action = "Get" };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<FacebookToken?>(storeProcCommand, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteToken()
        {
            string storeProcCommand = "Facebook_Token";
            object? param = new { Action = "Delete" };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand) > 0;
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
