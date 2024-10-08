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
    public class DLUserDetailsSQL : CommonDataBaseInteraction, IDLUserDetails
    {
        //private bool _disposed = false;
        //Get UserDetails By Email and Pwd ....
        CommonInfo connection;
        public DLUserDetailsSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<UserInfo?> UserById(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUserbyId", MlObj.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<UserInfo?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<UserDetail>> UsersbyMainUserId(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsers", MlObj.MainUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserDetail>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<UserDetailswithSenior>> UserDetailswithSenior(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsersWithSenior", MlObj.MainUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserDetailswithSenior>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task SaveCreatedUserInfo(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "Insert", MlObj.MainUserId, MlObj.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteUser(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "DeleteUserbyId", MlObj.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<MLUserHierarchy>> GetUsersByHierarchy(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsersByHierarchy", MlObj.MainUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<MLUserHierarchy>> GetUsersByGroups(UserDetails MlObj)
        {
            string UserGroupIdList = "";
            if (MlObj.GroupId != null && MlObj.GroupId.Count > 0)
                UserGroupIdList = string.Join(",", MlObj.GroupId.ToArray());

            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsersbyGroups", MlObj.MainUserId, UserGroupIdList };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<UserAccounts>> GetUsersAccount(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetAccounts", MlObj.MainUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserAccounts>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<UserDetails?> Get(int UserInfoUserId)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "Get", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<UserDetails?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }


        public async Task<int> InsertHierarchy(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "Insert_UserHierarchy", MlObj.MainUserId, MlObj.UserInfoUserId, MlObj.SeniorUserId, MlObj.AccountId, MlObj.CreatedByUserId, MlObj.PermissionLevelsId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<MLUserHierarchy?> GetUsersHierarchyByUserId(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsersByUserInfoId", MlObj.MainUserId, MlObj.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLUserHierarchy?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<UserAccounts>> GetUsersAccountbyUserId(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsersAccountByUserId", MlObj.MainUserId, MlObj.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserAccounts>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<int> DeleteHierarchy(UserDetails MlObj)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "Delete_UserHierarchy", MlObj.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<MLUserGroup>> GetUsersGroup(int UserId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GetUsersGroup", UserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<UserDetails>> GetUsersByCreatedUserId(int CreatedByUserId)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsersByCreatedUserId", CreatedByUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserDetails>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<P5GenralML.MLUserHierarchyWithPermissions>> GetUserDetails(int UserId, int UserGroupId, UserDetailsHierarchyWithPermissions userDetailsWithPermissions)
        {
            string storeProcCommand = "UserDetails";
            object? param = new { Action = "GetUsersDetails", UserId, userDetailsWithPermissions.EmailId, UserGroupId = UserGroupId > 0 ? UserGroupId.ToString() : "", userDetailsWithPermissions.ActiveStatus, userDetailsWithPermissions.PermissionLevelsId, userDetailsWithPermissions.SeniorUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserHierarchyWithPermissions>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
