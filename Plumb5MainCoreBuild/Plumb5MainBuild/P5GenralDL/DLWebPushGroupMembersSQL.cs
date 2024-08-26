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
    public class DLWebPushGroupMembersSQL : CommonDataBaseInteraction, IDLWebPushGroupMembers
    {
        CommonInfo connection;

        public DLWebPushGroupMembersSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushGroupMembersSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<long> GetUniqueMachineId(string ListOfGroupId)
        {
            string storeProcCommand = "WebPush_GroupMembers";
            object? param = new { Action = "GetUniqueMachineId", ListOfGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<long>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> MergeDistinctMachineIdIntoGroup(string ListOfGroupId, int GroupId, int UserInfoUserId)
        {
            string storeProcCommand = "WebPush_GroupMembers";
            object? param = new { Action = "MergeDistinctMachineIdIntoGroup", ListOfGroupId, GroupId, UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<List<WebPushUser>> GetGroupWebPushInfoList(int GroupId)
        {
            string storeProcCommand = "WebPush_GroupMembers";
            object? param = new { Action = "GET", GroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebPushUser>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<Int64> AddToGroup(WebPushGroupMembers webPushGroupMembers)
        {
            string storeProcCommand = "WebPush_GroupMembers";
            object? param = new { Action = "AddToGroup", webPushGroupMembers.GroupId, webPushGroupMembers.MachineId, webPushGroupMembers.ContactId, webPushGroupMembers.UserInfoUserId, webPushGroupMembers.UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<long>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> RemoveFromGroup(string[] machineids, int GroupId)
        {
            List<bool> TrueFalselist = new List<bool>();
            foreach (var machine in machineids)
            {
                string storeProcCommand = "WebPush_GroupMembers";
                object? param = new { Action = "RemoveFromGroup", machine, GroupId };


                using var db = GetDbConnection(connection.Connection);
                TrueFalselist.Add(await db.ExecuteScalarAsync<long>(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0);
            }
            return TrueFalselist.Any(x => x == true);
        }

        public async Task<List<Groups>> BelongToWhichGroup(string MachineId)
        {
            string storeProcCommand = "WebPush_GroupMembers";
            object? param = new { Action = "BelongToWhichGroup", MachineId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Groups>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
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
