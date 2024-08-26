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
    public class DLLmsGroupSQL : CommonDataBaseInteraction, IDLLmsGroup
    {
        CommonInfo connection;
        public DLLmsGroupSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsGroupSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }


        public async Task<Int32> Save(LmsGroup lmsGroup)
        {
            string storeProcCommand = "Lms_Group";
            object? param = new {Action="Save", lmsGroup.UserInfoUserId, lmsGroup.Name, lmsGroup.GroupType };
            using var db = GetDbConnection(connection.Connection);

            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(LmsGroup lmsGroup)
        {
            string storeProcCommand = "Lms_Group";
            object? param = new { Action = "Update", lmsGroup.Id, lmsGroup.UserInfoUserId, lmsGroup.Name };
            using var db = GetDbConnection(connection.Connection);

            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> Delete(int lmsGroupId)
        {
            string storeProcCommand = "Lms_Group";
            object? param = new { Action = "Delete", lmsGroupId };
            using var db = GetDbConnection(connection.Connection);

            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)>0;
        }

        public async Task<Int32> GetMaxCount(string UserIdList, LmsGroup lmsgroup)
        {
            int lmsgroupid = lmsgroup != null ? lmsgroup.Id : 0;
            string Name = lmsgroup != null ? lmsgroup.Name : null;
            string storeProcCommand = "Lms_Group";
            object? param = new { Action = "GetMaxCount", UserIdList, lmsgroupid, Name };
            using var db = GetDbConnection(connection.Connection);

            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MLLmsGroup>> GetListLmsGroup(int OffSet, int FetchNext, string UserIdList, LmsGroup lmsgroup = null)
        {
            int lmsgroupid = lmsgroup != null ? lmsgroup.Id : 0;
            string Name = lmsgroup != null ? lmsgroup.Name : null;
            string storeProcCommand = "Lms_Group";
            object? param = new { Action = "GetLmsGroups", OffSet, FetchNext, UserIdList, lmsgroupid, Name };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLLmsGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure);


        }

        public async Task<IEnumerable<LmsGroup>> GetCustomisedGroupList(IEnumerable<int> ListOfId, List<string> fieldName)
        {
            string listoflmsgroupid = string.Join(",", new List<int>(ListOfId).ToArray());
            string storeProcCommand = "Lms_Group";
            object? param = new { Action = "GetLmsGroupsList", listoflmsgroupid };
            using var db = GetDbConnection(connection.Connection);

            return await db.QueryAsync<LmsGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<LmsGroup>> GetLMSGroupList()
        {
            string storeProcCommand = "Lms_Group";
            object? param = new { Action = "GetRulesLmsGroups"  };
            using var db = GetDbConnection(connection.Connection);

            return await db.QueryAsync<LmsGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<LmsGroup>> GetLMSGroup(int LmsGroupMemberId)
        {
            string storeProcCommand = "Lms_Group";
            object? param = new { Action = "Get", LmsGroupMemberId };
            using var db = GetDbConnection(connection.Connection);

            return await db.QueryAsync<LmsGroup>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
