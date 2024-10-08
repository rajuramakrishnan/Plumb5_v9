﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P5GenralDL
{
    public class DLEmailVerifyProviderSettingPG : CommonDataBaseInteraction, IDLEmailVerifyProviderSetting
    {
        CommonInfo connection;
        public DLEmailVerifyProviderSettingPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLEmailVerifyProviderSettingPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(EmailVerifyProviderSetting setting)
        {
            string storeProcCommand = "select * from email_verifyprovidersetting_save(@UserInfoUserId, @ProviderName, @IsDefaultProvider, @IsActive, @ApiKey, @Password, @UserName, @APIUrl)";
            object? param = new { setting.UserInfoUserId, setting.ProviderName, setting.IsDefaultProvider, setting.IsActive, setting.ApiKey, setting.Password, setting.UserName, setting.APIUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }
        public async Task<bool> Update(EmailVerifyProviderSetting setting)
        {
            string storeProcCommand = "select * from email_verifyprovidersetting_update(@Id, @UserInfoUserId, @ProviderName, @IsDefaultProvider, @IsActive, @ApiKey, @Password, @UserName, @APIUrl)";
            object? param = new { setting.Id, setting.UserInfoUserId, setting.ProviderName, setting.IsDefaultProvider, setting.IsActive, setting.ApiKey, setting.Password, setting.UserName, setting.APIUrl };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }
        public async Task<List<EmailVerifyProviderSetting>> GetList(string ProviderName = null)
        {
            string storeProcCommand = "select * from email_verifyprovidersetting_get(@ProviderName)";
            object? param = new { ProviderName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<EmailVerifyProviderSetting>(storeProcCommand, param)).ToList();

        }
        public async Task<EmailVerifyProviderSetting?> GetActiveprovider()
        {
            string storeProcCommand = "select * from email_verifyprovidersetting_get()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<EmailVerifyProviderSetting?>(storeProcCommand, param);

        }
        public async Task<bool> Delete(int Id)
        {
            string storeProcCommand = "select * from email_verifyprovidersetting_delete(@Id)";
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
