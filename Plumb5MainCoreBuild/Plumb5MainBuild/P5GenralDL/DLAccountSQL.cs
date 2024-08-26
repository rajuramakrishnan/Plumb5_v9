﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;

namespace P5GenralDL
{
    public class DLAccountSQL : CommonDataBaseInteraction, IDLAccount
    {
        CommonInfo connection;
        public DLAccountSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> SaveDetails(Account account)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "Save", account.UserInfoUserId, account.AccountName, account.DomainName, account.AccountDescription, account.AllowSubDomain, account.Timezone, account.Connection, account.Script, account.ExpiryDate, account.AccountType, account.AccountStatus, account.IsCustomTrackingScript };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int AccountId)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "DEL", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<Account>> GetDetails(int UserInfoUserId)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "ACCOUNTBYUSERID", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Account>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<Account?> GetAccountDetails(int AccountId)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "GET", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<Account?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public Account? GetAccountDetailsData(int AccountId)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "GET", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return db.QueryFirstOrDefault<Account?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Account>> AccountByDomain(string domainName)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "GET", domainName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Account>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<Account>> GetAllAccount()
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "GetAllAccount" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Account>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<bool> AddWebPushSubDomain(int AccountId, string WebPushSubDomain)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "AddWebPushSubDomain", AccountId, WebPushSubDomain };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> CheckWebPushSubDomain(string WebPushSubDomain)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "CheckWebPushSubDomain", WebPushSubDomain };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> UpdateScript(int AccountId, string Script)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "UpdateAccountScript", AccountId, Script };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> GetAllAccountCount()
        {
            string storeProcCommand = "select * from account_details_getallaccountdetails()";

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand);
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
