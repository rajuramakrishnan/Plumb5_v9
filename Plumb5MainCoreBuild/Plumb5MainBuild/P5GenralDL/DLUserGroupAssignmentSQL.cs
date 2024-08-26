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
    public class DLUserGroupAssignmentSQL : CommonDataBaseInteraction, IDLUserGroupAssignment
    {
        CommonInfo connection;
        public DLUserGroupAssignmentSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLUserGroupAssignmentSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> SaveOrUpdate(int channelid, string channeltype, int usergroupid, int lastassignuserinfouserid, string userassignedvalues, int id = 0)
        {
            string storeProcCommand = "UserGroupAssignment";
            object? param = new { Action = "SaveorUpdate", channelid, channeltype, usergroupid, lastassignuserinfouserid, userassignedvalues, id };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<UserGroupAssignment?> GetDetails(int channelid = 0, string channeltype = null, int usergroupid = 0)
        {
            string storeProcCommand = "UserGroupAssignment";
            object? param = new { Action = "GET", channelid, channeltype, usergroupid };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<UserGroupAssignment?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
