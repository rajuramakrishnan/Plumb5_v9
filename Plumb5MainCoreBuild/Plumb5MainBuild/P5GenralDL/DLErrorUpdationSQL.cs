﻿using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace P5GenralDL
{
    internal class DLErrorUpdationSQL : CommonDataBaseInteraction, IDLErrorUpdation
    {
        CommonInfo connection;
        public DLErrorUpdationSQL()
        {
            connection = GetDBConnection();
        }

        public DLErrorUpdationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> SaveLog(MLErrorUpdation mLErrorUpdation)
        {
            string storeProcCommand = "Error_Updation";
            object? param = new { Action = "SaveLog", mLErrorUpdation.LogName, mLErrorUpdation.Error, mLErrorUpdation.ErrorDescription, mLErrorUpdation.ErrorDateTime, mLErrorUpdation.PageName, mLErrorUpdation.StackTrace };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }


        public async Task<int> GetErrorMaxCount(string ErrorName, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "Error_Updation";
            object? param = new { Action = "GetMaxCount", ErrorName, FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<MLErrorUpdation>> GetErrorLog(string errorName, DateTime fromDateTime, DateTime toDateTime, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Error_Updation";
            object? param = new { Action = "GetLog", errorName, fromDateTime, toDateTime, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLErrorUpdation>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<MLErrorUpdation>> GetLog(MLErrorUpdation mLErrorUpdation, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Error_Updation";
            object? param = new { Action = "GetLog", mLErrorUpdation.Id, mLErrorUpdation.LogName, OffSet, FetchNext };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLErrorUpdation>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

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
