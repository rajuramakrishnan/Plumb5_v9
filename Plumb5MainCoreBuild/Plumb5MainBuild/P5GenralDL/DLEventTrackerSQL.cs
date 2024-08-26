﻿using DBInteraction;
using IP5GenralDL;
using P5GenralML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace P5GenralDL
{
    public class DLEventTrackerSQL : CommonDataBaseInteraction, IDLEventTracker
    {
        CommonInfo connection;
        public DLEventTrackerSQL(int AccountId)
        {
            connection = GetDBConnection(AccountId);
        }

        public DLEventTrackerSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<Int32> SaveEventTrackingDetails(EventTracker mLEvent)
        {
            string storeProcCommand = "Event_Tracker";
            object param = new { mLEvent.Name, mLEvent.Events, mLEvent.PageName, mLEvent.VisitorIp, mLEvent.MachineId, mLEvent.SessionId, mLEvent.EventValue };

            using var db = GetDbConnection(connection.Connection);
            return await db.ExecuteScalarAsync<Int32>(storeProcCommand, param, commandType: CommandType.StoredProcedure);
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
