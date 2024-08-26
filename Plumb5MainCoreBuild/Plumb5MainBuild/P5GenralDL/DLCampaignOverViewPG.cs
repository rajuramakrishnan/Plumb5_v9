﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P5GenralML;
using DBInteraction;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P5GenralDL
{
    internal class DLCampaignOverViewPG : CommonDataBaseInteraction, IDLCampaignOverView
    {
        CommonInfo connection;
        public DLCampaignOverViewPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }
        public DLCampaignOverViewPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<int> CampaignMaxCount(DateTime FromDateTime, DateTime ToDateTime, string CampaignName, string TemplateName, string ChannelType)
        {
            string storeProcCommand = "select * from campaign_overview_getmaxcount(@FromDateTime,@ToDateTime,@CampaignName,@TemplateName,@ChannelType)";
            object? param = new { FromDateTime, ToDateTime, CampaignName, TemplateName, ChannelType };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<DataSet> GetCampaignReportDetails(DateTime fromDateTime, DateTime ToDateTime, int OffSet, int FetchNext, string CampaignName, string TemplateName, string ChannelType)
        {
            string storeProcCommand = "select * from campaign_overview_getreportdetails(@FromDateTime,@ToDateTime,@OffSet,@FetchNext,@CampaignName,@TemplateName,@ChannelType)";
            object? param = new { fromDateTime, ToDateTime, OffSet, FetchNext, CampaignName, TemplateName, ChannelType };

            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetTemplateDetails(string ChannelType)
        {
            string storeProcCommand = "select * from campaign_overview_gettemplatedetails(@ChannelType)";
            object? param = new { ChannelType };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

        }

        public async Task<DataSet> GetCampaignDetails(string ChannelType)
        {
            string storeProcCommand = "select * from campaign_overview_getcampaigndetails(@ChannelType)";
            object? param = new { ChannelType };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            return dataset;

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
