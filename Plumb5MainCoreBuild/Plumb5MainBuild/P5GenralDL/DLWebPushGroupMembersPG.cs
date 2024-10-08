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
    public class DLWebPushGroupMembersPG : CommonDataBaseInteraction, IDLWebPushGroupMembers
    {
        CommonInfo connection;

        public DLWebPushGroupMembersPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushGroupMembersPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<long> GetUniqueMachineId(string ListOfGroupId)
        {
            string storeProcCommand = "select webpush_groupmembers_getuniquemachineid(@ListOfGroupId)";
            object? param = new { ListOfGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<long>(storeProcCommand, param);
        }

        public async Task<bool> MergeDistinctMachineIdIntoGroup(string ListOfGroupId, int GroupId, int UserInfoUserId)
        {
            string storeProcCommand = "select webpush_groupmembers_mergedistinctmachineidintogroup(@ListOfGroupId, @GroupId, @UserInfoUserId)";
            object? param = new { ListOfGroupId, GroupId, UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<List<WebPushUser>> GetGroupWebPushInfoList(int GroupId)
        {
            string storeProcCommand = "select * from webpush_groupmembers_get(@GroupId)";
            object? param = new { GroupId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebPushUser>(storeProcCommand, param)).ToList();
        }

        public async Task<Int64> AddToGroup(WebPushGroupMembers webPushGroupMembers)
        {
            const string storeProcCommand = "select webpush_groupmembers_addtogroup(@GroupId, @MachineId, @ContactId, @UserInfoUserId, @UserGroupId)";
            object? param = new { webPushGroupMembers.GroupId, webPushGroupMembers.MachineId, webPushGroupMembers.ContactId, webPushGroupMembers.UserInfoUserId, webPushGroupMembers.UserGroupId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<long>(storeProcCommand, param);
        }

        public async Task<bool> RemoveFromGroup(string[] machineids, int GroupId)
        {
            List<bool> TrueFalselist = new List<bool>();
            foreach (var machine in machineids)
            {
                string storeProcCommand = "select webpush_groupmembers_removefromgroup(@machine, @GroupId)";
                object? param = new { machine, GroupId };


                using var db = GetDbConnection(connection.Connection);
                TrueFalselist.Add(await db.ExecuteScalarAsync<long>(storeProcCommand, param) > 0);
            }
            return TrueFalselist.Any(x => x == true);
        }

        public async Task<List<Groups>> BelongToWhichGroup(string MachineId)
        {
            string storeProcCommand = "select * from webpush_groupmembers_belongtowhichgroup(@MachineId)";
            object? param = new { MachineId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Groups>(storeProcCommand, param)).ToList();
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
