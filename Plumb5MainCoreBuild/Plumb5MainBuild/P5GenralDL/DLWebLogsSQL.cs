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
    public class DLWebLogsSQL : CommonDataBaseInteraction, IDLWebLogs
    {
        CommonInfo connection;
        public DLWebLogsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLWebLogsSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<int> SaveLog(WebLogs logs)
        {
            string storeProcCommand = "Web_Logs";
            object? param = new { Action = "Save", logs.AdsId, logs.UserInfoUserId, logs.UserName, logs.UserEmailId, logs.Controller, logs.ChannelType, logs.Actions, logs.RequestContent, logs.IpAddress };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> UpdateLog(WebLogs logs)
        {
            string storeProcCommand = "Web_Logs";
            object? param = new { Action = "Update", logs.Id, logs.ResponseContent, logs.ActionDescription };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure) > 0;

        }

        public async Task<Int32> GetMaxCount(WebLogs logDetails, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "Web_Logs";
            object? param = new { Action="GetMaxCount", FromDateTime, ToDateTime, logDetails.AdsId };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<WebLogs>> GetReportData(WebLogs logDetails, DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext)
        {
            string storeProcCommand = "Web_Logs";
            object? param = new { Action = "GetList", @FromDateTime, @ToDateTime, @OffSet, @FetchNext, logDetails.AdsId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebLogs>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<List<WebLogs>> GetLogsForNotification(int AdsId)
        {
            string storeProcCommand = "Web_Logs";
            object? param = new { Action = "GetLogsForNotification", AdsId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<WebLogs>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

        }
    }
}
