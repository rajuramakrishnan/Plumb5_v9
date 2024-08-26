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
    public class DLWorkFlowSQL : CommonDataBaseInteraction, IDLWorkFlow
    {
        CommonInfo connection;
        public DLWorkFlowSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WorkFlow workFlow)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "Save", workFlow.Title, workFlow.Flowchart, workFlow.ArrayConfig, workFlow.Status, workFlow.UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(WorkFlow workFlow)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "Update", workFlow.WorkFlowId, workFlow.Title, workFlow.Flowchart, workFlow.ArrayConfig, workFlow.UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<bool> UpdateStatus(int WorkFlowId, int Status, string UserName)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "UpdateStatus", WorkFlowId, Status, UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<bool> UpdateLastupdateddata(WorkFlow workFlow)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "UpdateLastupdateddate", workFlow.WorkFlowId, workFlow.UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }
        public async Task<WorkFlow?> GetDetails(WorkFlow workFlow)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "GetDetails", workFlow.WorkFlowId, Offset = 0, FetchNext = 0, workFlow.Title };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlow>(storeProcCommand, param);
        }

        public async Task<int> CheckWorkflowTitle(WorkFlow workFlow)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "CheckTitle", workFlow.WorkFlowId, workFlow.Title };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int WorkFlowId)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "Delete", WorkFlowId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> GetMaxCount(string WorkflowName = null)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "GetMaxCount", WorkflowName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<WorkFlow>> GetListDetails(int OffSet, int FetchNext, string WorkflowName = null)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "GetDetails", WorkFlowId = 0, OffSet, FetchNext, WorkflowName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlow>(storeProcCommand, param)).ToList();
        }

        public async Task<bool> UpdateErrorStatus(int WorkFlowId, int Status, string StoppedReason)
        {
            string storeProcCommand = "Work_Flow";
            object? param = new { Action = "UpdateErrorStatus", WorkFlowId, Status, StoppedReason };

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
