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
    public class DLContactLookedUpPG : CommonDataBaseInteraction, IDLContactLookedUp
    {
        CommonInfo connection;

        public DLContactLookedUpPG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactLookedUpPG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<Contact>> GET()
        {
            string storeProcCommand = "select * from contact_customdetails()";

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<Contact>(storeProcCommand)).ToList();
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
