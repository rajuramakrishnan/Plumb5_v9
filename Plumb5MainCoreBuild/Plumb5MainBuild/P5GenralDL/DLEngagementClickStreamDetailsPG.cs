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
    internal class DLEngagementClickStreamDetailsPG : CommonDataBaseInteraction, IDLEngagementClickStreamDetails
    {
        CommonInfo connection;
        public DLEngagementClickStreamDetailsPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLEngagementClickStreamDetailsPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }
        public async Task<List<MLSmsSentEachDetails>> EachSmsSentDetails(string PhoneNumber)
        {
            string storeProcCommand = "select * from SmsSent_EachReport(@Action,@PhoneNumber)";
            object? param = new { Action = "EachSmsSentDetails", PhoneNumber };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLSmsSentEachDetails>(storeProcCommand, param)).ToList();

        }

        public async Task<List<MLEachCallerDetails>> EachCallerDetails(string PhoneNumber)
        {
            string storeProcCommand = "select * from Lms_LeadsUpdateDetails(@Action,@PhoneNumber)";
            object? param = new { Action = "EachCallerDetails", PhoneNumber };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<MLEachCallerDetails>(storeProcCommand, param)).ToList();

        }

        public async Task<List<string>> GetVisitorPageFlow(string MachineId, string SessionId)
        {
            string storeProcCommand = "select * from MainTracker_GetHisSessionVisited(@Action,@MachineId,@SessionId)";
            object? param = new { Action = "GetVisitorPageFlow", MachineId, SessionId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<string>(storeProcCommand, param)).ToList();

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
