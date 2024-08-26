﻿using DBInteraction;
using P5GenralML;
using System.Data;
using Dapper;
using IP5GenralDL;

namespace P5GenralDL
{
    public class DLWorkFlowSmsBulkSentInitiationSQL : CommonDataBaseInteraction, IDLWorkFlowSmsBulkSentInitiation
    {
        CommonInfo connection;
        public DLWorkFlowSmsBulkSentInitiationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowSmsBulkSentInitiationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<WorkFlowSmsBulkSentInitiation>> GetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_SmsBulkSentInitiation";
            object? param = new { Action = "GetSentInitiation" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowSmsBulkSentInitiation>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<bool> UpdateSentInitiation(WorkFlowSmsBulkSentInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_SmsBulkSentInitiation";
            object? param = new { Action = "UpdateSentInitiation", BulkSentInitiation.SendingSettingId, BulkSentInitiation.InitiationStatus, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> ResetInitiation()
        {
            string storeProcCommand = "WorkFlow_SmsBulkSentInitiation";
            object? param = new { Action = "ResetInitiation" };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<int> Save(WorkFlowSmsBulkSentInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_SmsBulkSentInitiation";
            object? param = new { Action = "Save", BulkSentInitiation.SendingSettingId, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId, BulkSentInitiation.IsPromotionalOrTransactionalType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }
    }
}

