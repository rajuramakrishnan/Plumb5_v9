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
    public class DLFormLoadCloseResponseSQL : CommonDataBaseInteraction, IDLFormLoadCloseResponse
    {
        CommonInfo connection = null;
        public DLFormLoadCloseResponseSQL(int AccountId)
        {
            connection = GetDBConnection(AccountId);
        }

        public DLFormLoadCloseResponseSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async void SaveUpdateForImpression(int FormId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "Form_LoadCloseResponse";

            object? param = new { Action= "SaveUpdateForImpression",FormId, TrackIp, MachineId, SessionRefeer }; 
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);

        }

        public async void UpdateFormResponse(int FormId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "Form_LoadCloseResponse";
            object? param = new { Action = "UpdateFormResponse", FormId, TrackIp, MachineId, SessionRefeer };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
        }

        public async void UpdateFormClose(int FormId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "Form_LoadCloseResponse";

            object? param = new { Action = "UpdateFormClose", FormId, TrackIp, MachineId, SessionRefeer };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param, commandType: CommandType.StoredProcedure);
            var dataset = ConvertDataReaderToDataSet(list);
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
