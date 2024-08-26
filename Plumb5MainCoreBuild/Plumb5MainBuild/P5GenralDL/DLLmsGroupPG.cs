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
    public class DLLmsGroupPG : CommonDataBaseInteraction, IDLLmsGroup
    {
        CommonInfo connection;
        public DLLmsGroupPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsGroupPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }


        public async Task<Int32> Save(LmsGroup lmsGroup)
        {
            string storeProcCommand = "select * from lms_group_save(@UserInfoUserId, @Name, @GroupType)";
            object? param = new { lmsGroup.UserInfoUserId, lmsGroup.Name, lmsGroup.GroupType };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Update(LmsGroup lmsGroup)
        {
            string storeProcCommand = "select * from lms_group_update(@Id,@UserInfoUserId, @Name )";
            object? param = new { lmsGroup.Id, lmsGroup.UserInfoUserId, lmsGroup.Name };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<bool> Delete(int lmsGroupId)
        {
            try
            {
                string storeProcCommand = "select * from lms_group_delete(@lmsGroupId)";
                object? param = new { lmsGroupId };
                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

        public async Task<Int32> GetMaxCount(string UserIdList, LmsGroup lmsgroup)
        {
            int lmsgroupid = lmsgroup != null ? lmsgroup.Id : 0;
            string Name = lmsgroup != null ? lmsgroup.Name : null;
            string storeProcCommand = "select * from  lms_group_getmaxcount(@UserIdList,@lmsgroupid,@Name)";
            object? param = new { UserIdList, lmsgroupid, Name };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<MLLmsGroup>> GetListLmsGroup(int OffSet, int FetchNext, string UserIdList, LmsGroup lmsgroup = null)
        {
            int lmsgroupid = lmsgroup != null ? lmsgroup.Id : 0;
            string Name = lmsgroup != null ? lmsgroup.Name : null;
            string storeProcCommand = "select * from  lms_group_getlmsgroups(@OffSet, @FetchNext,@UserIdList,@lmsgroupid,@Name)";
            object? param = new { OffSet, FetchNext, UserIdList, lmsgroupid, Name };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLLmsGroup>(storeProcCommand, param);

        }

        public async Task<IEnumerable<LmsGroup>> GetCustomisedGroupList(IEnumerable<int> ListOfId, List<string> fieldName)
        {
            string listoflmsgroupid = string.Join(",", new List<int>(ListOfId).ToArray());
            string storeProcCommand = "select * from   lms_group_getlmsgroupslist(@listoflmsgroupid)";
            object? param = new { listoflmsgroupid };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<LmsGroup>(storeProcCommand, param);
        }
        public async Task<IEnumerable<LmsGroup>> GetLMSGroupList()
        {
            string storeProcCommand = "select * from   lms_group_getruleslmsgroups()";
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<LmsGroup>(storeProcCommand);
        }
        public async Task<IEnumerable<LmsGroup>> GetLMSGroup(int LmsGroupMemberId)
        {
            string storeProcCommand = "select * from  lms_group_get(@LmsGroupMemberId)";
            object? param = new { LmsGroupMemberId };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<LmsGroup>(storeProcCommand, param);
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
