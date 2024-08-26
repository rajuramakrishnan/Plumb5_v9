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
    public class DLWebPushBulkInitiationSQL : CommonDataBaseInteraction, IDLWebPushBulkInitiation
    {
        CommonInfo connection;
        public DLWebPushBulkInitiationSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushBulkInitiationSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WebPushBulkInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WebPush_BulkSentInitiation";
            object? param = new { Action= "Save", BulkSentInitiation.SendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<WebPushBulkInitiation>> GetSentInitiation()
        {
            string storeProcCommand = "WebPush_BulkSentInitiation";
            object? param = new { Action = "GetSentInitiation" };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebPushBulkInitiation>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<bool> UpdateSentInitiation(WebPushBulkInitiation BulkSentInitiation)
        {
            string storeProcCommand = "WebPush_BulkSentInitiation";
            object? param = new { Action = "UpdateSentInitiation", BulkSentInitiation.SendingSettingId, BulkSentInitiation.InitiationStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }
    }
}
