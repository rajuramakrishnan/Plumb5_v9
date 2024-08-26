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
    public class DLGroupByFilterSQL : CommonDataBaseInteraction, IDLGroupByFilter
    {
        CommonInfo connection;
        public DLGroupByFilterSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLGroupByFilterSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> SaveUpdate(GroupByFilter groupByFilter)
        {
            string storeProcCommand = "Group_ByFilter";
            object? param = new { Action = "SaveUpdate", groupByFilter.GroupId, groupByFilter.IsOtherGroup, groupByFilter.TimeInterval, groupByFilter.FilterContent, groupByFilter.FilterQuery };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<GroupByFilter?> Get(GroupByFilter groupByFilter)
        {
            string storeProcCommand = "Group_ByFilter";
            object? param = new { Action = "Get", groupByFilter.GroupId, groupByFilter.IsOtherGroup, groupByFilter.TimeInterval };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<GroupByFilter?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
