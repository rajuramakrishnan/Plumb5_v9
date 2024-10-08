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
    public class DLWorkFlowPG : CommonDataBaseInteraction, IDLWorkFlow
    {
        CommonInfo connection;
        public DLWorkFlowPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WorkFlow workFlow)
        {
            string storeProcCommand = "select * from work_flow_save(@Title, @Flowchart, @ArrayConfig, @Status, @UserName)";
            object? param = new { workFlow.Title, workFlow.Flowchart, workFlow.ArrayConfig, workFlow.Status, workFlow.UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Update(WorkFlow workFlow)
        {
            string storeProcCommand = "select * from work_flow_update(@WorkFlowId, @Title, @Flowchart, @ArrayConfig, @UserName)";
            object? param = new { workFlow.WorkFlowId, workFlow.Title, workFlow.Flowchart, workFlow.ArrayConfig, workFlow.UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateStatus(int WorkFlowId, int Status, string UserName)
        {
            string storeProcCommand = "select * from work_flow_updatestatus(@WorkFlowId, @Status, @UserName)";
            object? param = new { WorkFlowId, Status, UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<bool> UpdateLastupdateddata(WorkFlow workFlow)
        {
            string storeProcCommand = "select * from work_flow_updatelastupdateddate(@WorkFlowId,@UserName)";
            object? param = new { workFlow.WorkFlowId, workFlow.UserName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }
        public async Task<WorkFlow?> GetDetails(WorkFlow workFlow)
        {
            string storeProcCommand = "select * from work_flow_getdetails(@WorkFlowId,@Offset,@FetchNext,@Title)";
            object? param = new { workFlow.WorkFlowId, Offset = 0, FetchNext = 0, workFlow.Title };

            using var db = GetDbConnection(connection.Connection);
            return await db.QueryFirstOrDefaultAsync<WorkFlow>(storeProcCommand, param);
        }

        public async Task<int> CheckWorkflowTitle(WorkFlow workFlow)
        {
            string storeProcCommand = "select * from work_flow_checktitle(@WorkFlowId,@Title)";
            object? param = new { workFlow.WorkFlowId, workFlow.Title };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool> Delete(int WorkFlowId)
        {
            string storeProcCommand = "select * from work_flow_delete(@WorkFlowId)";
            object? param = new { WorkFlowId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
        }

        public async Task<int> GetMaxCount(string WorkflowName = null)
        {
            string storeProcCommand = "select * from work_flow_getmaxcount(@WorkflowName)";
            object? param = new { WorkflowName };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<List<WorkFlow>> GetListDetails(int OffSet, int FetchNext, string WorkflowName = null)
        {
            string storeProcCommand = "select * from work_flow_getdetails(@WorkFlowId,@OffSet,@FetchNext,@WorkflowName)";
            object? param = new { WorkFlowId = 0, OffSet, FetchNext, WorkflowName };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlow>(storeProcCommand, param)).ToList();
        }

        public async Task<bool> UpdateErrorStatus(int WorkFlowId, int Status, string StoppedReason)
        {
            string storeProcCommand = "select * from work_flow_updateerrorstatus(@WorkFlowId, @Status, @StoppedReason)";
            object? param = new { WorkFlowId, Status, StoppedReason };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;
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
