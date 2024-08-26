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
    public class DLUserGroupSQL : CommonDataBaseInteraction, IDLUserGroup
    {
        CommonInfo connection;

        public DLUserGroupSQL()
        {
            connection = GetDBConnection();
        }

        public DLUserGroupSQL(string ConnectionStrings)
        {
            connection = new CommonInfo { Connection = ConnectionStrings };
        }

        public async Task<Int32> Save(MLUserGroup userGroup)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "Save", userGroup.Name, userGroup.UserGroupDescription, userGroup.UserGroupId, userGroup.UserInfoUserId, userGroup.CreatedByUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> SavePermissions(int[] permissionsList, int UserGroupId)
        {
            bool isDataInserted = false;
            foreach (var eachContact in permissionsList)
            {
                string storeProcCommand = "UserGroup_Permissions";
                object? param = new { Action = "Save", eachContact, UserGroupId };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
            }
            return isDataInserted;
        }

        public async Task<bool> DeleteSavedGroupPermissions(int[] permissionsList, int UserGroupId)
        {
            string storeProcCommand = "UserGroup_Permissions";
            object? param = new { Action = "Delete", PermissionsList = string.Join(",", permissionsList.ToArray()), UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> Update(MLUserGroup userGroup)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "Update", userGroup.Id, userGroup.Name, userGroup.UserGroupDescription, userGroup.UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<int> GetUserGroupPermissionsCount(string UserGroupName, int UserInfoUserId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "MaxCount", UserInfoUserId, UserGroupName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<MLUserGroupWithHierarchy>> GetUserGroupPermissionsList(string UserGroupName, int OffSet, int FetchNext, int UserInfoUserId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GET", UserInfoUserId, UserGroupName, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserGroupWithHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<MLUserGroup>> GetUserGroupPermissionsToBind(MLUserGroup userGroup)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GetUserGroupPermissionToBind", userGroup.Id };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<bool> DeleteUserGroupPermissions(int Id)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "Delete", Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<Int32> InsertUserGroupsAccount(MLUserGroup userGroup, int AccountId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "InsertUserGroupsAccount", userGroup.Id, AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<Int32> DeleteUserGroupsAccount(MLUserGroup userGroup)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "DeleteUserGroupsAccount", userGroup.Id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<UserAccounts>> GetGroupAccounts(int UserGroupId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GetGroupAccounts", UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserAccounts>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<Int32> DeleteUserFromGroup(MLUserGroup userGroup)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "DeleteUserFromGroup", userGroup.UserInfoUserId, userGroup.UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<Int32> AddMemberstoGroup(MLUserGroup userGroup)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "AddMemberstoGroup", userGroup.UserGroupId, userGroup.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<UserInfo>> GetGroupMembers(int GroupId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GetGroupMembers", GroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserInfo>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<UserGroup>> GetUserGroup(int UserInfoUserId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GetUserGroups", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<UserGroup>> GetUserGroupList()
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GetList" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<UserGroup>> GetUserGroups(int UserInfoUserId)
        {
            string storeProcCommand = "User_Group";
            object? param = new { Action = "GetUsersGroup", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
