﻿using P5GenralML;
using IP5GenralDL;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using DBInteraction;
using Dapper;

namespace P5GenralDL
{
    public class DLAnlyticsNotificationLogPG : CommonDataBaseInteraction, IDLAnlyticsNotificationLog
    {
        CommonInfo connection;
        public DLAnlyticsNotificationLogPG()
        {
            connection = GetDBConnection();
        }

        public async Task<Int32> Save(AnlyticsNotificationLog log)
        {
            string storeProcCommand = "select anlyticsnotificationlog_save(@Accountid,@LastSentDate)"; 
            object? param = new { log.Accountid, log.LastSentDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param);
        }

        public async Task<bool>  Update(AnlyticsNotificationLog log)
        {
            string storeProcCommand = "select anlyticsnotificationlog_update(@Accountid,@LastSentDate)"; 
            object? param = new { log.Accountid, log.LastSentDate };
            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<int>(storeProcCommand, param)>0;
        }

        public async Task<IEnumerable<AnlyticsNotificationLog>>  GetDetails(int Accountid)
        {
            string storeProcCommand = "select * from anlyticsnotificationlog_get(@Accountid)"; 
            object? param = new { Accountid };
            using var db = GetDbConnection(connection.Connection);
            return await db.QueryAsync<AnlyticsNotificationLog>(storeProcCommand, param);
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
