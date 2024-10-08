﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System.Data;
using Dapper;

namespace P5GenralDL
{
    public class DLConversionsSQL : CommonDataBaseInteraction, IDLConversions
    {
        CommonInfo connection;
        public DLConversionsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLConversionsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<object?> GoalsMaxCount(_Plumb5MLGoal mlObj)
        {
            try
            {
                var storeProcCommand = "Goal_Setting";
                object? param = new { @Action = "MaxCount", mlObj.FromDate, mlObj.ToDate };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryFirstOrDefaultAsync<object?>(storeProcCommand, param);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<object?> GetGoalsReport(_Plumb5MLGoal mlObj)
        {
            try
            {
                var storeProcCommand = "Goal_Setting";
                object? param = new { @Action = "GetList", mlObj.Start, mlObj.End };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryFirstOrDefaultAsync<object?>(storeProcCommand, param);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<object?> Select_Goal(_Plumb5MLGoal mlObj)
        {
            try
            {
                var storeProcCommand = "SelectGoalList";

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryFirstOrDefaultAsync<object?>(storeProcCommand);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<Int32> Insert_GoalSetting(_Plumb5MLGoal mlObj)
        {
            try
            {
                var storeProcCommand = "Goal_Setting";
                object? param = new
                {
                    @Action = "Save",
                    mlObj.GoalName,
                    mlObj.GoalId,
                    mlObj.Channel,
                    mlObj.PageName1,
                    mlObj.PageName2,
                    mlObj.PageName3,
                    mlObj.PageName4,
                    mlObj.PageName5,
                    mlObj.PageName6,
                    mlObj.PageName7,
                    mlObj.PageName8,
                    mlObj.PageName9,
                    mlObj.PageName10
                };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

            }
            catch
            {
                return 0;
            }
        }

        public async Task<Int32> Delete_GoalSetting(_Plumb5MLGoal mlobj)
        {
            try
            {
                var storeProcCommand = "Goal_Setting";
                object? param = new
                {
                    @Action= mlobj.Key,
                    mlobj.GoalId
                };

                using var db = GetDbConnection(connection.Connection);
                return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

            }
            catch
            {
                return 0;
            }
        }

        public async Task<object?> Select_ForwardGoal(_Plumb5MLForwardGoal mlobj)
        {
            try
            {
                var storeProcCommand = "ForwardGoal";
                object? param = new
                {
                    mlobj.GoalId,
                    mlobj.FromDate,
                    mlobj.ToDate
                };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryFirstOrDefaultAsync<object?>(storeProcCommand, param);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<object?> Select_ReverseGoal(_Plumb5MLReverseGoal mlobj)
        {
            try
            {
                var storeProcCommand = "ReverseGoal";
                object? param = new
                {
                    mlobj.GoalId,
                    mlobj.FromDate,
                    mlobj.ToDate,
                    mlobj.Domain
                };

                using var db = GetDbConnection(connection.Connection);
                return await db.QueryFirstOrDefaultAsync<object?>(storeProcCommand, param);
            }
            catch
            {
                return 0;
            }
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

