﻿using Dapper;
using DBInteraction;
using IP5GenralDL;
using Microsoft.Identity.Client;
using P5GenralML;
using System.Data;
using System.Text.RegularExpressions;

namespace P5GenralDL
{
    public class DLAdminUserHierarchySQL : CommonDataBaseInteraction, IDLAdminUserHierarchy
    {
        CommonInfo connection;
        public DLAdminUserHierarchySQL()
        {
            connection = GetDBConnection();
        }
        public async Task<IEnumerable<MLAdminUserHierarchy>> GetHisUsers(int UserId)
        {
            string storeProcCommand = "Admin_User_Hierarchy";
            object? param = new { Action = "GetHisUsers", UserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLAdminUserHierarchy>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<MLAdminUserHierarchy?> GetHisDetails(int UserInfoUserId)
        {
            string storeProcCommand = "Admin_User_Hierarchy";
            object? param = new { Action = "GetHisDetails", UserInfoUserId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLAdminUserHierarchy?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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

