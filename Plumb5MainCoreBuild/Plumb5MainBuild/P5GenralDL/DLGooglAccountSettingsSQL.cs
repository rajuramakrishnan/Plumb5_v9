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
    public class DLGooglAccountSettingsSQL : CommonDataBaseInteraction, IDLGooglAccountSettings
    {
        CommonInfo connection = null;
        public DLGooglAccountSettingsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGooglAccountSettingsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(GooglAccountSettings googlAccountsettings)
        {
            string storeProcCommand = "Google_AccountSettings";
            object? param = new { Action= "Save", googlAccountsettings.GoogleAccountsId, googlAccountsettings.GoogleAccountName, googlAccountsettings.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Update(GooglAccountSettings googlAccountsettings)
        {
            string storeProcCommand = "Google_AccountSettings";
            object? param = new { Action = "Update", googlAccountsettings.GoogleAccountsId, googlAccountsettings.GoogleAccountName, googlAccountsettings.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }
        public async Task<List<GooglAccountSettings>> GetDetails(int Id)
        {
            string storeProcCommand = "Google_AccountSettings";
            object? param = new { Action = "Get", Id };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GooglAccountSettings>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<GooglAccountSettings?> Get(int Id)
        {
            string storeProcCommand = "Google_AccountSettings";
            object? param = new { Action = "Get", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GooglAccountSettings?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<int> ChangeStatusadwords(int Id, bool Status)
        {
            string storeProcCommand = "Google_AccountSettings";
            object? param = new { Action = "ChangeStatus", Id, Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }
        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "Google_AccountSettings";
            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

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
