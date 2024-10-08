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
    public class DLUserGroupAssignmentPG : CommonDataBaseInteraction, IDLUserGroupAssignment
    {
        CommonInfo connection;
        public DLUserGroupAssignmentPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLUserGroupAssignmentPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> SaveOrUpdate(int channelid, string channeltype, int usergroupid, int lastassignuserinfouserid, string userassignedvalues, int id = 0)
        {
            string storeProcCommand = "select usergroup_assignmentsaveorupdate(@channelid, @channeltype, @usergroupid, @lastassignuserinfouserid, @userassignedvalues, @id)";
            object? param = new { channelid, channeltype, usergroupid, lastassignuserinfouserid, userassignedvalues, id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<UserGroupAssignment?> GetDetails(int channelid = 0, string channeltype = null, int usergroupid = 0)
        {
            string storeProcCommand = "select * from usergroupassignment_get(@channelid, @channeltype, @usergroupid)";
            object? param = new { channelid, channeltype, usergroupid };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<UserGroupAssignment?>(storeProcCommand, param);
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
