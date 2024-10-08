﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;

namespace P5GenralDL
{
    public class DLAccountPG : CommonDataBaseInteraction, IDLAccount
    {
        CommonInfo connection;
        public DLAccountPG()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> SaveDetails(Account account)
        {
            string storeProcCommand = "select * from account_details_save(@UserInfoUserId, @AccountName, @DomainName, @AccountDescription, @AllowSubDomain, @Timezone, @Connection, @Script, @ExpiryDate, @AccountType, @AccountStatus, @IsCustomTrackingScript)";
            object? param = new { account.UserInfoUserId, account.AccountName, account.DomainName, account.AccountDescription, account.AllowSubDomain, account.Timezone, account.Connection, account.Script, account.ExpiryDate, account.AccountType, account.AccountStatus, account.IsCustomTrackingScript };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Delete(int AccountId)
        {
            string storeProcCommand = "select * from account_details_del(@AccountId)";
            object? param = new { AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<List<Account>> GetDetails(int UserInfoUserId)
        {
            string storeProcCommand = "select * from account_details_accountbyuserid(@UserInfoUserId)";
            object? param = new { UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Account>(storeProcCommand, param)).ToList();
        }

        public async Task<Account?> GetAccountDetails(int AccountId)
        {
            string storeProcCommand = "select * from account_details_get(@AccountId)";
            object? param = new { AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Account?>(storeProcCommand, param);
        }

        public Account? GetAccountDetailsData(int AccountId)
        {
            string storeProcCommand = "select * from account_details_get(@AccountId)";
            object? param = new { AccountId };

            using var db = GetDbConnection(connection.Connection);
            return db.QueryFirstOrDefault<Account?>(storeProcCommand, param);
        }

        public async Task<List<Account>> AccountByDomain(string domainName)
        {
            string storeProcCommand = "select * from account_details_get(@accountid,@userinfouserid,@accountname,@domainName)";
            object? param = new { accountid = 0, userinfouserid = 0, accountname = "", domainName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Account>(storeProcCommand, param)).ToList();
        }
        public async Task<int> GetAllAccountCount()
        {
            string storeProcCommand = "select * from account_details_getallaccountdetails()";

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand);
        }

        public async Task<List<Account>> GetAllAccount()
        {
            string storeProcCommand = "select * from account_details_getallaccount()";

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Account>(storeProcCommand)).ToList();
        }

        public async Task<bool> AddWebPushSubDomain(int AccountId, string WebPushSubDomain)
        {
            string storeProcCommand = "select * from account_details_addwebpushsubdomain(@AccountId, @WebPushSubDomain)";
            object? param = new { AccountId, WebPushSubDomain };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> CheckWebPushSubDomain(string WebPushSubDomain)
        {
            string storeProcCommand = "select * from account_details_checkwebpushsubdomain(@WebPushSubDomain)";
            object? param = new { WebPushSubDomain };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<bool> UpdateScript(int AccountId, string Script)
        {
            string storeProcCommand = "select * from account_details_updateaccountscript(@AccountId, @Script)";
            object? param = new { AccountId, Script };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        #region Dispose Method
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connection = null;
                }
            }
            //dispose unmanaged ressources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion End of Dispose Method
    }
}
