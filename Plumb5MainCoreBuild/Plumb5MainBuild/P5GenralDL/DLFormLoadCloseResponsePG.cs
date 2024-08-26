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
    public class DLFormLoadCloseResponsePG : CommonDataBaseInteraction, IDLFormLoadCloseResponse
    {
        CommonInfo connection = null;
        public DLFormLoadCloseResponsePG(int AccountId)
        {
            connection = GetDBConnection(AccountId);
        }

        public DLFormLoadCloseResponsePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async void SaveUpdateForImpression(int FormId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "select form_loadcloseresponse_saveupdateforimpression(@FormId, @TrackIp, @MachineId, @SessionRefeer )";
           
            object? param = new { FormId, TrackIp, MachineId, SessionRefeer };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
            
        }

        public async void UpdateFormResponse(int FormId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "select form_loadcloseresponse_updateformresponse(@FormId, @TrackIp, @MachineId, @SessionRefeer )"; 
            object? param = new { FormId, TrackIp, MachineId, SessionRefeer };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
            var dataset = ConvertDataReaderToDataSet(list);
        }

        public async void UpdateFormClose(int FormId, string TrackIp, string MachineId, string SessionRefeer)
        {
            string storeProcCommand = "select form_loadcloseresponse_updateformclose(@FormId, @TrackIp, @MachineId, @SessionRefeer)";
            
            object? param = new { FormId, TrackIp, MachineId, SessionRefeer };
            using var db = GetDbConnection(connection.Connection);
            var list = await db.ExecuteReaderAsync(storeProcCommand, param);
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
