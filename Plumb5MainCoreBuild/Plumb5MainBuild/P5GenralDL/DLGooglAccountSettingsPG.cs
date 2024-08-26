﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLGooglAccountSettingsPG : CommonDataBaseInteraction, IDLGooglAccountSettings
    {
        CommonInfo connection = null;
        public DLGooglAccountSettingsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGooglAccountSettingsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(GooglAccountSettings googlAccountsettings)
        {
            string storeProcCommand = "select * from googleaccountsettings_save(@GoogleAccountsId,@GoogleAccountName,@Status )";
            object? param = new { googlAccountsettings.GoogleAccountsId, googlAccountsettings.GoogleAccountName, googlAccountsettings.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<bool> Update(GooglAccountSettings googlAccountsettings)
        {
            string storeProcCommand = "select * from googleaccountsettings_update(@Id, @GoogleAccountsId, @GoogleAccountName, @Status)";
            object? param = new { googlAccountsettings.Id, googlAccountsettings.GoogleAccountsId, googlAccountsettings.GoogleAccountName, googlAccountsettings.Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
        public async Task<List<GooglAccountSettings>> GetDetails(int Id)
        {
            string storeProcCommand = "select * from googleaccountsettings_get(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<GooglAccountSettings>(storeProcCommand, param)).ToList();

        }

        public async Task<GooglAccountSettings?> Get(int Id)
        {
            string storeProcCommand = "select * from googleaccountsettings_get(@Id)";
            object? param = new { Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GooglAccountSettings?>(storeProcCommand, param);

        }

        public async Task<int> ChangeStatusadwords(int Id, bool Status)
        {
            string storeProcCommand = "select * from googleaccountsettings_changestatus(@Id,@Status)";
            object? param = new { Id, Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "select * from googleaccountsettings_delete(@Id)";
            object? param = new { Id };

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
