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
    public class DLContactImportMergePG : CommonDataBaseInteraction, IDLContactImportMerge
    {
        CommonInfo connection;
        public DLContactImportMergePG(int adsId)
        {
            connection = GetDBConnection(adsId);
        }

        public DLContactImportMergePG(string connectionString)
        {
            connection = new CommonInfo() { Connection = connectionString };
        }

        public async Task<List<ContactImportMerge>> GetList(int ContactImportOverviewId)
        {
            string storeProcCommand = "ContactImport_Merge";
            object? param = new { Action= "GetList", ContactImportOverviewId };

            using var db = GetDbConnection(connection.Connection);
            return (await db.QueryAsync<ContactImportMerge>(storeProcCommand, param, commandType: CommandType.StoredProcedure)).ToList();

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

