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
    public class DLUserGroupMembersPG : CommonDataBaseInteraction, IDLUserGroupMembers
    {
        CommonInfo connection;

        public DLUserGroupMembersPG()
        {
            connection = GetDBConnection();
        }

        public async Task<List<MLUserHierarchy>> GetHisUsers(int UserGroupId)
        {
            string storeProcCommand = "select * from usergroup_members_gethisusers(@UserGroupId)";
            object? param = new { UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLUserHierarchy>(storeProcCommand, param)).ToList();
        }

        public async Task<List<Members>> GetUserGroupMembers(List<int> ListofUserGroupMember)
        {
            string storeProcCommand = "select * from usergroup_members_getusergroupmembers()";
            object? param = new { ListofUserGroupMembers = string.Join(",", ListofUserGroupMember) };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Members>(storeProcCommand, param)).ToList();
        }

        public async Task<int> AddToGroup(int UserInfoUserId, int UserGroupId)
        {
            const string storeProcCommand = "select usergroup_members_addtogroup(@UserInfoUserId, @UserGroupId)";
            object? param = new { UserInfoUserId, UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> RemoveFromGroup(int UserInfoUserId, int UserGroupId)
        {
            const string storeProcCommand = "select usergroup_members_removefromgroup(@UserInfoUserId, @UserGroupId)";
            object? param = new { UserInfoUserId, UserGroupId };

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
