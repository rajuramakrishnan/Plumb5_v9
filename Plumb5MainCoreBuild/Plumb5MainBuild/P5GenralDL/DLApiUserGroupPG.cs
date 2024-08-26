﻿using P5GenralML;
using IP5GenralDL;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using DBInteraction;
using Dapper;
using Azure.Core;
using System;


namespace P5GenralDL
{
    public class DLApiUserGroupPG : CommonDataBaseInteraction, IDLApiUserGroup
    {
        CommonInfo connection = null;
        public DLApiUserGroupPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLApiUserGroupPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int16>  Save(int lastassignuserinfouserid, int usergroupid)
        {
            string storeProcCommand = "select apiusergroup_save(@lastassignuserinfouserid,@usergroupid)"; 
            object? param = new { lastassignuserinfouserid, usergroupid };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int16>(storeProcCommand, param);
        }

        public async Task<ApiUserGroup?>  GET(int usergroupid)
        {
            string storeProcCommand = "select * from apiusergroup_get(@usergroupid)"; 
            object? param = new { usergroupid }; 
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<ApiUserGroup?>(storeProcCommand, param);
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
