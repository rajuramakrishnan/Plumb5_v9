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
    public class DLUserHierarchySQL : CommonDataBaseInteraction, IDLUserHierarchy
    {
        CommonInfo connection;

        public DLUserHierarchySQL()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> Save(UserHierarchy userHierarchy)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "Save", userHierarchy.MainUserId, userHierarchy.AccountId, userHierarchy.UserInfoUserId, userHierarchy.SeniorUserId, userHierarchy.CreatedByUserId, userHierarchy.PermissionLevelsId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<MLUserHierarchy>> GetHisUsers(int UserInfoUserId, int AccountId, int Getallusers = 0)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "GetHisUsers", UserInfoUserId, AccountId, Getallusers };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<MLUserHierarchy>> GetHisUsers(int UserId)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "GetHisUsersWithOutAccount", UserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<MLUserHierarchy?> GetUsersSenior(int UserInfoUserId)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "GetUsersByUserInfoId", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLUserHierarchy?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<MLUserHierarchy?> GetHisDetails(int UserInfoUserId)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "GetHisDetails", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLUserHierarchy?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<UserHierarchy?> GetHisRole(int UserInfoUserId)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "GetHisRole", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<UserHierarchy?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteUserHierarchyByAccountId(int AccountId)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "DeleteUserHierarchyByAccountId", AccountId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<MLUserHierarchy>> GetUsersBySeniorIdForTree(int UserInfoUserId)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "GetUsersBySeniorIdForTree", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<List<UserHierarchy>> GetPermissionUsers(int UserId)
        {
            string storeProcCommand = "User_Hierarchy";
            object? param = new { Action = "GetHisUsersWithPermission", UserId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<UserHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
