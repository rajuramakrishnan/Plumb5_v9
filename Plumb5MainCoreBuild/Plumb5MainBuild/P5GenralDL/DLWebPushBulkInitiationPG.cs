﻿using Dapper;
using DBInteraction;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralDL
{
    public class DLWebPushBulkInitiationPG : CommonDataBaseInteraction, IDLWebPushBulkInitiation
    {
        CommonInfo connection;
        public DLWebPushBulkInitiationPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebPushBulkInitiationPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> Save(WebPushBulkInitiation BulkSentInitiation)
        {
            string storeProcCommand = "select * from webpush_bulksentinitiation_save(@SendingSettingId)";
            object? param = new { BulkSentInitiation.SendingSettingId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);

        }

        public async Task<List<WebPushBulkInitiation>> GetSentInitiation()
        {
            string storeProcCommand = "select * from webpush_bulksentinitiation_getsentinitiation()";
            object? param = new { };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebPushBulkInitiation>(storeProcCommand, param)).ToList();

        }

        public async Task<bool> UpdateSentInitiation(WebPushBulkInitiation BulkSentInitiation)
        {
            string storeProcCommand = "select * from webpush_bulksentinitiation_updatesentinitiation(@SendingSettingId,@InitiationStatus)";
            object? param = new { BulkSentInitiation.SendingSettingId, BulkSentInitiation.InitiationStatus };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param) > 0;

        }

    }
}
