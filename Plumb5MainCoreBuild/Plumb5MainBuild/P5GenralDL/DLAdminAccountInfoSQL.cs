﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLAdminAccountInfoSQL : CommonDataBaseInteraction, IDLAdminAccountInfo
    {
        CommonInfo connection;
        public DLAdminAccountInfoSQL(int key = 0)
        {
            connection = GetDBConnection();
        }

        public async Task<MLAdminUserInfo?> GetSupportManagerByAccountId(int AccountId)
        {
            string storeProcCommand = "Admin_GetUserDetails";
            object? param = new { Action = "GetSupportManagerByAccountId", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLAdminUserInfo?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<MLAdminManagersName?> GetManagersNameByAccountId(int AccountId)
        {
            string storeProcCommand = "Admin_GetUserDetails";
            object? param = new { Action = "GetAccountManagersByAccountId", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLAdminManagersName?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> StopStartAccount(int AccountId, int Status)
        {
            string storeProcCommand = "AdminAccount_Details";
            object? param = new { Action = "StopStartAccount", AccountId, Status };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<MLAdminUserInfo?> GetManagerByAccountId(int AccountId)
        {
            string storeProcCommand = "Admin_GetUserDetails";
            object? param = new { Action = "GetManagerByAccountId", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLAdminUserInfo?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateAccountManager(int AccountId, int UserId, int SupportUserId)
        {
            string storeProcCommand = "Admin_GetUserDetails";
            object? param = new { Action = "UpdateAccountManager", AccountId, UserId, SupportUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<long> SaveSupportAccountManagers(int AccountId, int accountmanager)
        {
            string storeProcCommand = "Admin_GetUserDetails";
            object? param = new { Action = "SaveSupportAccountManagers", AccountId, accountmanager };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> AdminAccountsDelete(int AccountId)
        {
            string storeProcCommand = "AdminAccount_Details";
            object? param = new { Action = "UserAccountDeletion", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> SaveManagerDetails(MLAdminUserInfo addmanager)
        {
            string storeProcCommand = "ManagerDetails";
            object? param = new { Action = "Save", addmanager.Role, addmanager.UserId, addmanager.FirstName, addmanager.LastName, addmanager.EmailId, addmanager.MobilePhone, addmanager.Password };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        public async Task<int> DeleteManagerDetails(int Userid, int Role, int UpdateManagerRole)
        {
            string storeProcCommand = "selectManagerDetails";
            object? param = new { Action = "Delete", Userid, Role, UpdateManagerRole };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<MLAdminUserInfo>> GetManagerdetailsForDropdown()
        {
            string storeProcCommand = "ManagerDetails";
            object? param = new { Action = "GetManagerForDropDown" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLAdminUserInfo>(storeProcCommand, param)).ToList();
        }
        public async Task<int> UpdateMergeAccountSuportManager(int UserId)
        {
            string storeProcCommand = "ManagerDetails";
            object? param = new { Action = "MergeManager", UserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> ChangeManagerStatus(int userid, bool ActiveStatus)
        {
            string storeProcCommand = "ManagerDetails";
            object? param = new { Action = "ChangeStatus", userid, ActiveStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<MLAdminUserInfo>> GetManagerDetails(object Role)
        {
            string storeProcCommand = "ManagerDetails";
            object? param = new { Action = "Get", Role };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLAdminUserInfo>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<MLAdminManagersDetails?> GetManagersDetailsByAccountId(int AccountId)
        {
            string storeProcCommand = "Admin_GetUserDetails";
            object? param = new { Action = "GetAccountManagersDetailsByAccountId", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLAdminManagersDetails?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        public async Task<bool> UpdateAccountType(int AccountId, short AccountType)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "UpdateAccountType", AccountId, AccountType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> UpdateExpiryDate(int AccountId, DateTime ExpiryDate)
        {
            string storeProcCommand = "Account_Details";
            object? param = new { Action = "UpdateExpiry", AccountId, ExpiryDate };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<DataSet> GetManagerDetails(int userid, int supportuserid)
        {
            string storeProcCommand = "select * from admin_getuserdetailslistall(@Userid,@Supportuserid)";
            object? param = new { userid, supportuserid };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
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
