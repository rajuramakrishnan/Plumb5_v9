﻿using P5GenralML;
using IP5GenralDL;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using DBInteraction;
using Dapper;
using Azure.Core;
using System;

namespace P5GenralDL
{
    public class DLAppMessengerTrackForIndividualSQL : CommonDataBaseInteraction, IDLAppMessengerTrackForIndividual
    {
        CommonInfo connection;
        public DLAppMessengerTrackForIndividualSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLAppMessengerTrackForIndividualSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> Save(AppMessengerTrackForIndividual messengerTrack)
        {
            string storeProcCommand = "AppMessengerTrack_ForIndividual";
            object? param = new {Action= "Save", messengerTrack.PhoneNumber, messengerTrack.AppMessengerContent, messengerTrack.ContactId, messengerTrack.AppMessengerTemplateId, messengerTrack.IsFormIsChatIsLmsIsMail };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<AppMessengerTrackForIndividual>> GetSmsTrackForIndividual(string PhoneNumber)
        {
            string storeProcCommand = "AppMessengerTrack_ForIndividual";
            object? param = new { Action = "Get", PhoneNumber };
            using var db = GetDbConnection(connection.Connection); 
            return await db.QueryAsync<AppMessengerTrackForIndividual>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
                    connection = null;
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
