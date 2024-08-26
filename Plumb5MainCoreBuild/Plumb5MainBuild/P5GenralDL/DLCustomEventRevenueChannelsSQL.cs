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
    public class DLCustomEventRevenueChannelsSQL : CommonDataBaseInteraction, IDLCustomEventRevenueChannels
    {
        CommonInfo connection;
        public DLCustomEventRevenueChannelsSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public async Task<DataSet> GetDayWiseRevenue(string EventName, string EventPriceColumn, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "CustomEvents_RevenueChannels";

            object? param = new { Action= "GetDayWiseRevenue",EventName, EventPriceColumn, FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<DataSet> GetChannelCount(string EventName, string EventPriceColumn, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "CustomEvents_RevenueChannels";
            
            object? param = new { Action = "GetChannelCount", EventName, EventPriceColumn, FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<Int32> GetRevenueMaxCount(string Channel, string EventName, string EventPriceColumn, DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "CustomEvents_RevenueChannels";
            object? param = new { Action = "GetMaxCount", Channel, EventName, EventPriceColumn, FromDateTime, ToDateTime };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<DataSet> GetRevenueData(string Channel, string EventName, string EventPriceColumn, DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext)
        {
            string storeProcCommand = "CustomEvents_RevenueChannels";
            object? param = new { Action = "GetRevenueData", Channel, EventName, EventPriceColumn, FromDateTime, ToDateTime, OffSet, FetchNext };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;
        }

        public async Task<Int32> GetIndividualRevenueCount(string Channel, int CampaignId, string EventName, DateTime FromDateTime, DateTime ToDateTime, Int16 CampignType)
        {
            string storeProcCommand = "CustomEvents_RevenueChannels";

            object? param = new { Action = "GetIndividualRevenueCount", Channel, CampaignId, EventName, FromDateTime, ToDateTime, CampignType };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Customevents>> GetIndividualRevenueData(string Channel, int CampaignId, string EventName, DateTime FromDateTime, DateTime ToDateTime, int OffSet, int FetchNext, Int16 CampignType)
        {
            string storeProcCommand = "CustomEvents_RevenueChannels";
            object? param = new { Action = "GetIndividualRevenueData", Channel, CampaignId, EventName, FromDateTime, ToDateTime, OffSet, FetchNext, CampignType };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<Customevents>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
