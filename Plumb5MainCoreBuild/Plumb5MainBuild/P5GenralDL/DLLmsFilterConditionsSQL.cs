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
    public class DLLmsFilterConditionsSQL : CommonDataBaseInteraction, IDLLmsFilterConditions
    {
        CommonInfo connection;
        public DLLmsFilterConditionsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLLmsFilterConditionsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(MLLmsFilterConditions filterCondition)
        {
            string storeProcCommand = "Lms_FilterConditions";
            object? param = new { Action="Save",filterCondition.UserInfoUserId, filterCondition.Name, filterCondition.QueryField, filterCondition.ShowInDashboard };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MLLmsFilterConditions>> GetFilterName(string UserIdList, bool ShowInDashboard = false)
        {
            string storeProcCommand = "Lms_FilterConditions";
            object? param = new {  Action= "GetFilterName", UserIdList, ShowInDashboard };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<MLLmsFilterConditions>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<MLLmsFilterConditions?> GetFilterConditionDetails(int FilterConditionId)
        {
            string storeProcCommand = "Lms_FilterConditions";
            object? param = new { Action = "GetFilterConditionDetails", FilterConditionId };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLLmsFilterConditions?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<MLLmsFilterConditions?> GetRecentSavedReportDetails(string UserIdList)
        {
            string storeProcCommand = "Lms_FilterConditions";
            object? param = new { Action = "GetRecentSavedReportDetails", UserIdList };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<MLLmsFilterConditions?>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(MLLmsFilterConditions filterCondition)
        {
            string storeProcCommand = "Lms_FilterConditions";
            object? param = new { Action = "Update", filterCondition.Id, filterCondition.UserInfoUserId, filterCondition.Name, filterCondition.QueryField, filterCondition.ShowInDashboard };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure)> 0;
        }

        public async Task<bool> DeleteSavedSearch(int Id)
        {
            string storeProcCommand = "Lms_FilterConditions";
            object? param = new { Action = "Delete", Id };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
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
