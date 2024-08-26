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
    public class DLCampaignCalendarSQL : CommonDataBaseInteraction, IDLCampaignCalendar
    {
        CommonInfo connection;
        public DLCampaignCalendarSQL(int adsId)
        {
            connection = GetDBConnection();
        }
        public DLCampaignCalendarSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<List<MLCampaignCalendar>> GetOverallScheduledDetails(DateTime FromDateTime, DateTime ToDateTime)
        {
            string storeProcCommand = "Campaign_Calendar";
            object? param = new { Action="GetOverallScheduledDetails", FromDateTime, ToDateTime };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLCampaignCalendar>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

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
