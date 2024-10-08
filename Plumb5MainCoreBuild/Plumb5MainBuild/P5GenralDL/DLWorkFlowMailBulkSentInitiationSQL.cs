﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWorkFlowMailBulkSentInitiationSQL : CommonDataBaseInteraction, IDLWorkFlowMailBulkSentInitiation
    {
        CommonInfo connection;
        public DLWorkFlowMailBulkSentInitiationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWorkFlowMailBulkSentInitiationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<WorkFlowMailBulkSentInitiation>> GetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_MailBulkSentInitiation";
            object? param = new { Action = "GetSentInitiation"};

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WorkFlowMailBulkSentInitiation>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<bool> UpdateSentInitiation(WorkFlowMailBulkSentInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_MailBulkSentInitiation";
            object? param = new { Action = "UpdateSentInitiation", BulkSentInitiation.SendingSettingId, BulkSentInitiation.InitiationStatus, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<bool> ResetSentInitiation()
        {
            string storeProcCommand = "WorkFlow_MailBulkSentInitiation";
            object? param = new { Action = "ResetSentInitiation"};

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<int> Save(WorkFlowMailBulkSentInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WorkFlow_MailBulkSentInitiation";
            object? param = new { Action= "Save", BulkSentInitiation.SendingSettingId, BulkSentInitiation.WorkFlowId, BulkSentInitiation.WorkFlowDataId, BulkSentInitiation.IsPromotionalOrTransactionalType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

    }
}
