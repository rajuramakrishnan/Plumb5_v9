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
    public class DLContactLookedUpSQL : CommonDataBaseInteraction, IDLContactLookedUp
    {
        CommonInfo connection;

        public DLContactLookedUpSQL(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactLookedUpSQL(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<Contact>> GET()
        {
            string storeProcCommand = "Contact_CustomDetails";
            object? param = new { Action = "GetContactsForLookUpScheduler" };
            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Contact>(storeProcCommand, commandType: CommandType.StoredProcedure)).ToList();
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
