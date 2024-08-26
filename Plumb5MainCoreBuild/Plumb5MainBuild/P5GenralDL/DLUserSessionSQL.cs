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
    public class DLUserSessionSQL : CommonDataBaseInteraction, IDLUserSession
    {
        CommonInfo connection;

        public DLUserSessionSQL()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> Save(UserSession UserSession)
        {
            string storeProcCommand = "User_Session";
            object? param = new { Action = "Save", UserSession.UserInfoUserId, UserSession.UserGroupId, UserSession.SessionId, UserSession.AuthValue, UserSession.SecureKey, UserSession.IsLocked };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<UserSession?> Get(UserSession UserSession)
        {
            string storeProcCommand = "User_Session";
            object? param = new { Action = "Update", UserSession.UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<UserSession?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
