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
    public class DLLmsFilterConditionsPG : CommonDataBaseInteraction, IDLLmsFilterConditions
    {
        CommonInfo connection;
        public DLLmsFilterConditionsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsFilterConditionsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(MLLmsFilterConditions filterCondition)
        {
            string storeProcCommand = "select lms_filterconditions_save(@UserInfoUserId, @Name, @QueryField, @ShowInDashboard)";
            object? param = new { filterCondition.UserInfoUserId, filterCondition.Name, filterCondition.QueryField, filterCondition.ShowInDashboard };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<IEnumerable<MLLmsFilterConditions>>   GetFilterName(string UserIdList, bool ShowInDashboard = false)
        {
            string storeProcCommand = "select * from lms_filterconditions_getfiltername(@UserIdList, @ShowInDashboard)";
            object? param = new { UserIdList, ShowInDashboard };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLLmsFilterConditions>(storeProcCommand, param);
        }

        public async Task<MLLmsFilterConditions?>  GetFilterConditionDetails(int FilterConditionId)
        {
            string storeProcCommand = "select * from lms_filterconditions_getfilterconditiondetails(@FilterConditionId)";
            object? param = new { FilterConditionId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLLmsFilterConditions?>(storeProcCommand, param);
        }

        public async Task<MLLmsFilterConditions?>   GetRecentSavedReportDetails(string UserIdList)
        {
            string storeProcCommand = "select * from lms_filterconditions_getrecentsavedreportdetails(@UserIdList)";
            object? param = new { UserIdList };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLLmsFilterConditions?>(storeProcCommand, param);
        }

        public async Task<bool> Update(MLLmsFilterConditions filterCondition)
        {
            string storeProcCommand = "select lms_filterconditions_update(@Id, @UserInfoUserId, @Name, @QueryField, @ShowInDashboard )";
            object? param = new { filterCondition.Id, filterCondition.UserInfoUserId, filterCondition.Name, filterCondition.QueryField, filterCondition.ShowInDashboard };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<bool> DeleteSavedSearch(int Id)
        {
            string storeProcCommand = "select lms_filterconditions_delete(@Id)";
            object? param = new { Id };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
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
