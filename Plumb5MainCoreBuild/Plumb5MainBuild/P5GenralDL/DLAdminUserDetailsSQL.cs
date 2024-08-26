﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;

namespace P5GenralDL
{
    public class DLAdminUserDetailsSQL : CommonDataBaseInteraction, IDLAdminUserDetails
    {
        CommonInfo connection;
        public DLAdminUserDetailsSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<IEnumerable<MLAdminUserInfo>> GetAllUser(string UserIdList=null)
        {
            string storeProcCommand = "Admin_UserDetails";
            object? param = new { Action = "GetAllUser", UserIdList };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLAdminUserInfo>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MLAdminUserInfo>> SelectAllUsers()
        {
            string storeProcCommand = "Admin_UserDetails";
            object? param = new { Action = "SelectAllUsers" };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLAdminUserInfo>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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

